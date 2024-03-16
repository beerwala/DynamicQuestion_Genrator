using Application.DTO;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            CreateMap<Assessment, AssessmentDto>().ReverseMap();
            CreateMap<AssessmentQuestons, QuestionsDto>().ReverseMap();
            CreateMap<AssessmentDto, Assessment>();
            CreateMap<QuestionsDto, AssessmentQuestons>();
        }
    }
}
