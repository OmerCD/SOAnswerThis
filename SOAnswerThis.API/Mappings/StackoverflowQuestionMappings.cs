using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SOAnswerThis.Contract;
using SOAnswerThis.DTO;

namespace SOAnswerThis.API.Mappings
{
    public class StackoverflowQuestionMappings : Profile
    {
        public StackoverflowQuestionMappings()
        {
            CreateMap<StackoverflowQuestionDTO, StackoverflowQuestionResponseModel>()
                .ForMember(model => model.Question, expression => expression.MapFrom(dto => dto.QuestionHtml));
        }
    }
}