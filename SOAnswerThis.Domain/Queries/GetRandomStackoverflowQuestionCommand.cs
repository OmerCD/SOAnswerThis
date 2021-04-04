using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SOAnswerThis.DTO;
using SOAnswerThis.Infrastructure.Services.Abstract;

namespace SOAnswerThis.Domain.Queries
{
    public class GetRandomStackoverflowQuestionCommand : IRequest<StackoverflowQuestionDTO>
    {
        
    }
    public class GetRandomStackoverflowQuestionCommandHandler:IRequestHandler<GetRandomStackoverflowQuestionCommand, StackoverflowQuestionDTO>
    {
        private readonly IStackOverflowService _stackOverflowService;

        public GetRandomStackoverflowQuestionCommandHandler(IStackOverflowService stackOverflowService)
        {
            _stackOverflowService = stackOverflowService;
        }

        public Task<StackoverflowQuestionDTO> Handle(GetRandomStackoverflowQuestionCommand request, CancellationToken cancellationToken)
        {
            return _stackOverflowService.GetRandomStackoverflowQuestion(cancellationToken);
        }
    }
}