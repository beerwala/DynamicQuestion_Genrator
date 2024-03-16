using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class QuestionsDto
    {
        public string questions { get; set; }

        public string response_Type { get; set; }

        public bool isRequired { get; set; } = false;

        public int assessmentId { get; set; }
    }
}
