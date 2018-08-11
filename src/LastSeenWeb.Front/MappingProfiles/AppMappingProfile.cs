using AutoMapper;
using LastSeenWeb.Data.Models;
using LastSeenWeb.Domain.Models;

namespace LastSeenWeb.Front.MappingProfiles
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<LastSeenItem, LastSeenItemEntity>()
				.ReverseMap();
		}
	}
}
