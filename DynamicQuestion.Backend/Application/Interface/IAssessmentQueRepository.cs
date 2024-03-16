using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IAssessmentQueRepository:IGenericRepositoryAsync<AssessmentQuestons>
    {
        Task AddRangeAsync(IEnumerable<AssessmentQuestons> entities);
    }
}
