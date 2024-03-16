using Application.DTO;
using Application.Interface;
using Application.Wrapper;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.NewAssessment.Querie.GetById
{
    public class GetByIdQuerie:IRequest<Response<AssessmentDto>>
    {
        public int Id { get; set; }
    }
    public class GetByIdQuerieHandler : IRequestHandler<GetByIdQuerie, Response<AssessmentDto>>
    {
        private readonly IAssessmentRespository _assessmentRespository;
        private readonly IMapper _mapper;
        public GetByIdQuerieHandler(IAssessmentRespository assessmentRespository,IMapper mapper)
        {
             _assessmentRespository = assessmentRespository;
            _mapper = mapper;
        }
        public async Task<Response<AssessmentDto>> Handle(GetByIdQuerie request, CancellationToken cancellationToken)
        {
            var assessment = await _assessmentRespository.GetByIdAsync(request.Id);
            if (assessment == null)
            {
                return new Response<AssessmentDto>
                {
                    Succeeded = false,
                    Message = "Assessment not found",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            var questions = await _assessmentRespository.GetQuestionsByAssessmentIdAsync(request.Id);
            var assessmentDto = _mapper.Map<AssessmentDto>(assessment);
            assessmentDto.questionsDtos = _mapper.Map<List<QuestionsDto>>(questions);
            return new Response<AssessmentDto>
            {
                Data = assessmentDto,
                Succeeded = true,
                StatusCode= (int)HttpStatusCode.OK
            };
        }
    }
}
