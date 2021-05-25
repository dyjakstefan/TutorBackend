using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TutorBackend.Core.Dto;
using TutorBackend.Core.Entities;
using TutorBackend.Core.Requests;

namespace TutorBackend.Infrastructure.MappingProfile
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<CreateMessageRequest, Message>();

            CreateMap<Conversation, ConversationDto>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.User.Username))
                .ForMember(x => x.TutorUsername, opt => opt.MapFrom(x => x.Tutor.Username))
                .ForMember(x => x.Files, opt => opt.MapFrom(s => s.SharedFiles));

            CreateMap<Message, MessageDto>();
        }
    }
}
