using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LastSeenWeb.Data.Models;
using LastSeenWeb.Domain.Models;

namespace LastSeenWeb.Data.Services.Implementation
{
	public class LastSeenRepository : ILastSeenRepository
	{
		private readonly IMapper _mapper;

		public LastSeenRepository(IMapper mapper)
		{
			_mapper = mapper;
		}

		public Task<List<LastSeenItem>> Get()
		{
			var list = new List<LastSeenItemEntity>
			{
				new LastSeenItemEntity { Id="2", VisitUrl="link",Name="Large", ImageUrl="https://images.unsplash.com/photo-1494249465471-5655b7878482?ixlib=rb-0.3.5&ixid=eyJhcHBfaWQiOjEyMDd9&s=191559dc1cae3f8967d568dfd8a77093&auto=format&fit=crop&w=1050&q=80" },
				new LastSeenItemEntity { Id="3", VisitUrl="link",Name="Tall", ImageUrl="https://ae01.alicdn.com/kf/HTB1vZZCLFXXXXbAXXXXq6xXFXXX2/S0127-BEELZEBUB-DEMON-SWORD-OF-VAELEN-DARKNESS-SATIN-FINISH-TWO-SIDE-BLADES-38.jpg" },
				new LastSeenItemEntity { Id="4", VisitUrl="link",Name="Wide", ImageUrl="https://cdn.reliks.com/products/841/750x280/1.jpg" },
				new LastSeenItemEntity { Id="5", VisitUrl="link", Name="Small", ImageUrl="https://i.ytimg.com/vi/FK_wM-nhdkE/hqdefault.jpg?sqp=-oaymwEXCPYBEIoBSFryq4qpAwkIARUAAIhCGAE=&rs=AOn4CLDuMzxvXYZuM0aP8g6hmsciVf3AgQ" },
				new LastSeenItemEntity { Id="6", VisitUrl="link",Name="Large", ImageUrl="https://images.unsplash.com/photo-1494249465471-5655b7878482?ixlib=rb-0.3.5&ixid=eyJhcHBfaWQiOjEyMDd9&s=191559dc1cae3f8967d568dfd8a77093&auto=format&fit=crop&w=1050&q=80" },
				new LastSeenItemEntity { Id="7", VisitUrl="link",Name="Tall", ImageUrl="https://ae01.alicdn.com/kf/HTB1vZZCLFXXXXbAXXXXq6xXFXXX2/S0127-BEELZEBUB-DEMON-SWORD-OF-VAELEN-DARKNESS-SATIN-FINISH-TWO-SIDE-BLADES-38.jpg" },
				new LastSeenItemEntity { Id="8", VisitUrl="link",Name="Wide", ImageUrl="https://cdn.reliks.com/products/841/750x280/1.jpg" },
				new LastSeenItemEntity { Id="9", VisitUrl="link", Name="Small", ImageUrl="https://i.ytimg.com/vi/FK_wM-nhdkE/hqdefault.jpg?sqp=-oaymwEXCPYBEIoBSFryq4qpAwkIARUAAIhCGAE=&rs=AOn4CLDuMzxvXYZuM0aP8g6hmsciVf3AgQ" },
				new LastSeenItemEntity { Id="10", VisitUrl="link",Name="Large", ImageUrl="https://images.unsplash.com/photo-1494249465471-5655b7878482?ixlib=rb-0.3.5&ixid=eyJhcHBfaWQiOjEyMDd9&s=191559dc1cae3f8967d568dfd8a77093&auto=format&fit=crop&w=1050&q=80" },
				new LastSeenItemEntity { Id="11", VisitUrl="link",Name="Tall", ImageUrl="https://ae01.alicdn.com/kf/HTB1vZZCLFXXXXbAXXXXq6xXFXXX2/S0127-BEELZEBUB-DEMON-SWORD-OF-VAELEN-DARKNESS-SATIN-FINISH-TWO-SIDE-BLADES-38.jpg" },
				new LastSeenItemEntity { Id="12", VisitUrl="link",Name="Wide", ImageUrl="https://cdn.reliks.com/products/841/750x280/1.jpg" },
			};

			return Task.FromResult(_mapper.Map<List<LastSeenItem>>(list));
		}
	}
}
