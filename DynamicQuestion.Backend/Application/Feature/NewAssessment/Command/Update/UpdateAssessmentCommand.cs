using Application.DTO;
using Application.Interface;
using Application.Wrapper;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.NewAssessment.Command.Update
{
    public class UpdateAssessmentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; } // Add assessment ID property
        public AssessmentDto AssessmentDto { get; set; }
        public IList<QuestionsDto> QuestionsDtos { get; set; }
    }

    public class UpdateAssessmentCommandHandler : IRequestHandler<UpdateAssessmentCommand, Response<int>>
    {
        private readonly IAssessmentRespository _assessmentRespository;
        private readonly IAssessmentQueRepository _questionRepository;
        private readonly IMapper _mapper;

        public UpdateAssessmentCommandHandler(IAssessmentRespository assessmentRespository, IAssessmentQueRepository questionRepository, IMapper mapper)
        {
            _assessmentRespository = assessmentRespository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateAssessmentCommand request, CancellationToken cancellationToken)
        {
            // Get the assessment from the repository
            var assessment = await _assessmentRespository.GetByIdAsync(request.Id);

            // Check if the assessment exists
            if (assessment != null)
            {
                // Update assessment properties
                _mapper.Map(request.AssessmentDto, assessment);
                await _assessmentRespository.UpdateAsync(assessment);

                // Update associated questions
                foreach (var questionDto in request.QuestionsDtos)
                {
                    // Map DTO to entity
                    var question = _mapper.Map<AssessmentQuestons>(questionDto);
                    question.assessmentId = assessment.Id; // Ensure the foreign key is set

                    // Update or add the question
                    if (question.Id > 0)
                    {
                        // Update existing question
                        await _questionRepository.UpdateAsync(question);
                    }
                    else
                    {
                        // Add new question
                        await _questionRepository.AddAsync(question);
                    }
                }

                // Return success response
                return new Response<int>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Updated Assessment Successfully",
                    Succeeded = true
                };
            }
            else
            {
                // Return error response if assessment not found
                return new Response<int>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Assessment not found"
                };
            }
        }


    }
}
