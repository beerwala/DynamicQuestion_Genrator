using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AssessmentDto
    {
        public int Id {  get; set; }
        public string name { get; set; }
        public bool isScorable { get; set; } = false;
        public IList<QuestionsDto> questionsDtos {  get; set; }
    }
}
