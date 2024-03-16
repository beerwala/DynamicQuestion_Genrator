using Application.Feature.AssessmentQuestion.Command;
using Application.Feature.NewAssessment.Command.Create;
using Application.Feature.NewAssessment.Command.Update;
using Application.Feature.NewAssessment.Querie.GetById;
using Application.Feature.NewAssessment.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicQuestion.WebApi.Controllers.v1
{
    public class AssessmentController : BaseController
    {
        [HttpPost("NewAssessment")]
        public async Task<IActionResult> CreateAssessment([FromBody] CreateAssessmentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("CreateQuestions")]
        public async Task<IActionResult> CreateQuestions([FromBody] CreateQuestionCommand command)
        {
            if (command == null || command.Questions == null || command.Questions.Count == 0)
            {
                return BadRequest("Invalid or Empty Input");
            }

            var response = await Mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("CreateQuestions")]
        public async Task<IActionResult> CreateNewQue([FromBody] CreateNewQueCommand command)
        {
            if (command == null || command.QuestionList == null || command.QuestionList.Count == 0)
            {
                return BadRequest("Invalid or Empty Input");
            }

            var response = await Mediator.Send(command);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateAssessmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            command.Id = id;

            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetByIdQuerie { Id = id }));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {

            return Ok(await Mediator.Send(new GetAllAssessmentQuery()));
        }
    }
}
