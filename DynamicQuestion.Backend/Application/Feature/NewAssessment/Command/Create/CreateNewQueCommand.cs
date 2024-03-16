using Application.DTO;
using Application.Interface;
using Application.Wrapper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.NewAssessment.Command.Create
{
    public class CreateNewQueCommand : IRequest<Response<int>>
    {
        public AssessmentDto Assessment { get; set; }
        public List<QuestionsDto> QuestionList { get; set; }
    }

    public class CreateNewQueCommandHandler : IRequestHandler<CreateNewQueCommand, Response<int>>
    {
        private readonly IAssessmentRespository _assessmentRespository;
        private readonly IAssessmentQueRepository _questionsRepository;

        public CreateNewQueCommandHandler(IAssessmentRespository assessmentRespository, IAssessmentQueRepository assessmentQueRepository)
        {
            _assessmentRespository = assessmentRespository;
            _questionsRepository = assessmentQueRepository;
        }

        public async Task<Response<int>> Handle(CreateNewQueCommand request, CancellationToken cancellationToken)
        {
            if (request.Assessment == null || request.QuestionList == null || !request.QuestionList.Any())
            {
                return new Response<int>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Assessment and Question list must be provided."
                };
            }

            var anyAssessment = await _assessmentRespository.GetByIdAsync(request.Assessment.Id);

            if (anyAssessment != null)
            {
                anyAssessment.name = request.Assessment.name;
                anyAssessment.isScorable = request.Assessment.isScorable;
                anyAssessment.assessmentsQuestions = new List<AssessmentQuestons>();

                foreach (var questionDto in request.QuestionList)
                {
                    anyAssessment.assessmentsQuestions.Add(new AssessmentQuestons
                    {
                        questions = questionDto.questions,
                        response_Type = questionDto.response_Type,
                        isRequired = questionDto.isRequired,
                        assessmentId = anyAssessment.Id
                    });
                }
            }
            else
            {
                var newAssessment = new Assessment
                {
                    name = request.Assessment.name,
                    isScorable = request.Assessment.isScorable,
                    assessmentsQuestions = new List<AssessmentQuestons>(),
                };

                foreach (var questionDto in request.QuestionList)
                {
                    newAssessment.assessmentsQuestions.Add(new AssessmentQuestons
                    {
                        questions = questionDto.questions,
                        response_Type = questionDto.response_Type,
                        isRequired = questionDto.isRequired,
                        assessmentId = newAssessment.Id
                    });
                }

                await _assessmentRespository.AddAsync(newAssessment);

                return new Response<int>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Succeeded = true,
                    Message = "Successfully Added!"
                };
            }

            return new Response<int>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Something went wrong."
            };
        }
    }
}
