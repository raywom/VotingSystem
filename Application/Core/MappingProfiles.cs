using Application.VotingSystem;
using Application.Comments;
using Application.Polls;
using Application.Profiles;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;
            CreateMap<Poll, Poll>();
            CreateMap<Poll, PollDto>()
                .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Choices, o => o.MapFrom(s => s.Choices))
                .ForMember(d => d.Voters, o => o.MapFrom(s => s.Voters))
                .ForMember(d => d.Comments, o => o.MapFrom(s => s.Comments));
            CreateMap<Choice, ChoiceDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title));
            CreateMap<Vote, VoterDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Poll.Id))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.AppUser.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.AppUser.Followings.Count))
                .ForMember(d => d.Following,
                    o => o.MapFrom(s => s.AppUser.Followers.Any(x => x.Observer.UserName == currentUsername)))
                .ForMember(d => d.ChoiceId, o => o.MapFrom(s => s.Choice.Id));
            CreateMap<AppUser, Profile>()
                .ForMember(d => d.Image, s => s.MapFrom(o => o.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.Followings.Count))
                .ForMember(d => d.Following,
                    o => o.MapFrom(s => s.Followers.Any(x => x.Observer.UserName == currentUsername)));
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Vote, UserPollDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Poll.Id))
                .ForMember(d => d.Date, o => o.MapFrom(s => s.Poll.CloseDate))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Poll.Title))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Poll.Category))
                .ForMember(d => d.HostUsername, o => o.MapFrom(s =>
                    s.Poll.AppUser.UserName));
        }
    }
}