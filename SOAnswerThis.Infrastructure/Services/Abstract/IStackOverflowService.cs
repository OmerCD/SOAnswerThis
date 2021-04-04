using System.Threading;
using System.Threading.Tasks;
using SOAnswerThis.DTO;

namespace SOAnswerThis.Infrastructure.Services.Abstract
{
    public interface IStackOverflowService
    {
        Task<StackoverflowQuestionDTO> GetRandomStackoverflowQuestion(CancellationToken cancellationToken = default);
    }
}