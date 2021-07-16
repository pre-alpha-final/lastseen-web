using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LastSeenWeb.Data.Models;
using LastSeenWeb.Domain.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LastSeenWeb.Data.Services.Implementation
{
	public class LastSeenRepository : ILastSeenRepository
	{
		private const string DatabaseName = "mo10097_lastseen";
		private const string ItemsCollectionName = "LastSeenItems";
		private const string NotesCollectionName = "LastSeenNotes";

		private readonly IMapper _mapper;
		private readonly IMongoClient _mongoClient;
		private readonly ILogger<LastSeenRepository> _logger;

		public LastSeenRepository(IMapper mapper, IMongoClient mongoClient, ILogger<LastSeenRepository> logger)
		{
			_mapper = mapper;
			_mongoClient = mongoClient;
			_logger = logger;
		}

		public async Task<List<LastSeenItem>> GetAll(string ownerName)
		{
			try
			{
				var db = _mongoClient.GetDatabase(DatabaseName);
				var lastSeenItems = db.GetCollection<LastSeenItemEntity>(ItemsCollectionName);
				var result = await lastSeenItems
					.Find(e => e.OwnerName == ownerName && e.Deleted == false)
					.SortByDescending(e => e.Modified)
					.ToListAsync();

				return _mapper.Map<List<LastSeenItem>>(result);
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
			}

			return new List<LastSeenItem>();
		}

		public async Task<LastSeenItem> Get(string id, string ownerName)
		{
			try
			{
				var db = _mongoClient.GetDatabase(DatabaseName);
				var lastSeenItems = db.GetCollection<LastSeenItemEntity>(ItemsCollectionName);
				var result = await lastSeenItems
					.Find(e => e.Id == id && e.OwnerName == ownerName && e.Deleted == false)
					.FirstOrDefaultAsync();

				return _mapper.Map<LastSeenItem>(result);
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
			}

			return null;
		}

		public async Task Upsert(LastSeenItem lastSeenItem, string ownerName)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(lastSeenItem.Id) || lastSeenItem.Id == "undefined")
				{
					lastSeenItem.Id = null;
				}

				var db = _mongoClient.GetDatabase(DatabaseName);
				var lastSeenItems = db.GetCollection<LastSeenItemEntity>(ItemsCollectionName);
				var oldEntity = await lastSeenItems
					.Find(e => e.OwnerName == ownerName && e.Id == lastSeenItem.Id)
					.FirstOrDefaultAsync();
				var lastModified = oldEntity?.Modified ?? DateTime.UtcNow;

				var entity = _mapper.Map<LastSeenItemEntity>(lastSeenItem);
				entity.OwnerName = ownerName;
				entity.Modified = lastSeenItem.MoveToTop ? DateTime.UtcNow : lastModified;

				if (entity.Id == null)
				{
					await lastSeenItems.InsertOneAsync(entity);
				}
				else
				{
					await lastSeenItems.ReplaceOneAsync(e => e.Id == entity.Id, entity);
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
			}
		}

		public async Task Delete(string id, string ownerName)
		{
			try
			{
				var db = _mongoClient.GetDatabase(DatabaseName);
				var lastSeenItems = db.GetCollection<LastSeenItemEntity>(ItemsCollectionName);
				var entity = await lastSeenItems
					.Find(e => e.OwnerName == ownerName && e.Id == id)
					.FirstOrDefaultAsync();
				entity.Deleted = true;
				await lastSeenItems.ReplaceOneAsync(e => e.Id == entity.Id, entity);
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
			}
		}

		public async Task UpsertNotes(string notes, string ownerName)
		{
			try
			{
				var db = _mongoClient.GetDatabase(DatabaseName);
				var notesEntities = db.GetCollection<NotesEntity>(NotesCollectionName);
				var oldEntity = await notesEntities
					.Find(e => e.OwnerName == ownerName)
					.FirstOrDefaultAsync();

				var entity = new NotesEntity
				{
					Id = oldEntity?.Id,
					OwnerName = ownerName,
					Notes = notes,
				};

				if (entity.Id == null)
				{
					await notesEntities.InsertOneAsync(entity);
				}
				else
				{
					await notesEntities.ReplaceOneAsync(e => e.Id == entity.Id, entity);
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
			}
		}

		public async Task<string> GetNotes(string ownerName)
		{
			try
			{
				var db = _mongoClient.GetDatabase(DatabaseName);
				var notesEntities = db.GetCollection<NotesEntity>(NotesCollectionName);
				var result = await notesEntities
					.Find(e => e.OwnerName == ownerName)
					.FirstOrDefaultAsync();

				return result?.Notes;
			}
			catch (Exception e)
			{
				_logger.LogError(e.Message);
			}

			return null;
		}
	}
}
