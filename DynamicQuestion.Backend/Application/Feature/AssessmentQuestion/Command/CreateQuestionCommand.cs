using Application.DTO;
using Application.Interface;
using Application.Wrapper;
using Domain.Entity;
using MediatR;
using System.Net;
namespace Application.Feature.AssessmentQuestion.Command
{
    public class CreateQuestionCommand : IRequest<Response<int>>
    {
        public List<QuestionsDto> Questions { get; set; }
    }

    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Response<int>>
    {
        private readonly IAssessmentQueRepository _assessmentQueRepository;

        public CreateQuestionCommandHandler(IAssessmentQueRepository assessmentQueRepository)
        {
            _assessmentQueRepository = assessmentQueRepository;
        }

        // CreateQuestionCommandHandler
        public async Task<Response<int>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request?.Questions?.Any() ?? false)
            {
                var newQuestions = request.Questions.Select(questionDTO => new AssessmentQuestons
                {
                    questions = questionDTO.questions, // Changed property name to 'Question'
                    isRequired = questionDTO.isRequired, // Changed property name to 'IsRequired'
                    assessmentId = questionDTO.assessmentId // Changed property name to 'AssessmentId'
                }).ToList();

                await _assessmentQueRepository.AddRangeAsync(newQuestions);

                return new Response<int>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Questions Added Successfully!",
                    Succeeded = true
                };
            }
            else
            {
                return new Response<int>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid or Empty Input",
                };
            }
        }

    }
}
