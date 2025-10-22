using AutoMapper;
using comicTracker.DTOs;
using comicTracker.Models;

namespace comicTracker.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.ComicCount, opt => opt.MapFrom(src => src.Comics.Count))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
                .ForMember(dest => dest.RoleDisplayName, opt => opt.Ignore())
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());
            
            CreateMap<ApplicationUser, UserWithComicsDto>()
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
                .ForMember(dest => dest.RoleDisplayName, opt => opt.Ignore());
            
            CreateMap<UpdateUserProfileRequest, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.IsAdmin, opt => opt.Ignore());

            CreateMap<AdminUserUpdateRequest, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore());

            // Comic mappings
            CreateMap<Comic, ComicDto>()
                .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition.ToString()));
            
            CreateMap<CreateComicRequest, Comic>()
                .ConstructUsing(_ => new Comic())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.DateAdded, opt => opt.Ignore())
                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
            
            CreateMap<UpdateComicRequest, Comic>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.DateAdded, opt => opt.Ignore())
                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            // Wishlist mappings
            CreateMap<WishlistItem, WishlistItemDto>()
                .ForMember(dest => dest.DesiredCondition, opt => opt.MapFrom(src => src.DesiredCondition.ToString()));
            
            CreateMap<CreateWishlistItemRequest, WishlistItem>()
                .ConstructUsing(_ => new WishlistItem())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.DateAdded, opt => opt.Ignore())
                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
            
            CreateMap<UpdateWishlistItemRequest, WishlistItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.DateAdded, opt => opt.Ignore())
                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}