using Application.DTO;
using Application.Interface;
using Azure;
using Domain.Entity;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class AssessmentRepository : GenericRepository<Assessment>, IAssessmentRespository
    {
        private readonly DbSet<Assessment> _assessments;
        private readonly ApplicationContext _context;
        public AssessmentRepository(ApplicationContext application) : base(application)
        {
            _assessments = application.Set<Assessment>();
            _context = application;
        }

        public async Task<IEnumerable<AssessmentQuestons>> GetQuestionsByAssessmentIdAsync(int assessmentrqId)
        {
            return await _context.AssessmentQuestons
                .Where(q => q.assessmentId == assessmentrqId)
                .ToListAsync();
        }


    }
}
