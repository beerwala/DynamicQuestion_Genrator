using Application.Interface;
using Application.Wrapper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.NewAssessment.Command.Create
{
    public class CreateAssessmentCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public bool isScorable { get; set; }
    }
    public class CreateAssessmentCommandHandler : IRequestHandler<CreateAssessmentCommand, Response<int>>
    {
        private readonly IAssessmentRespository _assessmentRespository;
        public CreateAssessmentCommandHandler(IAssessmentRespository assessmentRespository)
        {
            _assessmentRespository = assessmentRespository;
        }
        public async Task<Response<int>> Handle(CreateAssessmentCommand request, CancellationToken cancellationToken)
        {
            if (request != null)
            {
                var newAssessment = new Assessment
                {
                    name = request.Name,
                    isScorable = request.isScorable,
                };
                await _assessmentRespository.AddAsync(newAssessment);
                return new Response<int>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "New Assessment Added Successfully!",
                    Succeeded = true,
                    Data = newAssessment.Id
                };
            }
            else
            {
                return new Response<int>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invaild or Empty Input",
                };
            }
        }
    }
}
