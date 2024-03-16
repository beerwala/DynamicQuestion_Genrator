using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.NewAssessment.Queries.GetAll
{
    public class GetAllAssessmentQuery : IRequest<IEnumerable<AssessmentDto>>
    {

    }

    public class GetAllAssessmentQueryHandler : IRequestHandler<GetAllAssessmentQuery, IEnumerable<AssessmentDto>>
    {
        private readonly IAssessmentRespository _assessmentRepository;
        private readonly IMapper _mapper;

        public GetAllAssessmentQueryHandler(IAssessmentRespository assessmentRepository, IMapper mapper)
        {
            _assessmentRepository = assessmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssessmentDto>> Handle(GetAllAssessmentQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<AssessmentDto>>(await _assessmentRepository.GetAllAsync()).ToList();
        }
    }
}
