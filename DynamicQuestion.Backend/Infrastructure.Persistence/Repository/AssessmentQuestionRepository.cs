using Application.Interface;
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
    public class AssessmentQuestionRepository:GenericRepository<AssessmentQuestons>,IAssessmentQueRepository
    {
        private readonly DbSet<AssessmentQuestons> _dbSet;
        private readonly ApplicationContext _context;
        public AssessmentQuestionRepository(ApplicationContext application):base(application)
        {
            _dbSet = application.Set<AssessmentQuestons>();
            _context = application;
        }
        public async Task AddRangeAsync(IEnumerable<AssessmentQuestons> entities)
        {
            await _context.AssessmentQuestons.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
