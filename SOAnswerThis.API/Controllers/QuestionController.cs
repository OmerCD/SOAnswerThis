using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HtmlAgilityPack;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using SOAnswerThis.Contract;
using SOAnswerThis.Domain.Queries;
using SOAnswerThis.Infrastructure.Services.Abstract;

namespace SOAnswerThis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public QuestionController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var randomStackoverflowQuestion =
                await _mediator.Send(new GetRandomStackoverflowQuestionCommand(), cancellationToken);
            return Ok(_mapper.Map<StackoverflowQuestionResponseModel>(randomStackoverflowQuestion));
        }
    }
}