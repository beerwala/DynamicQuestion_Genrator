using Domain.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class AssessmentQuestons:BaseEntity<int>,IEntity
    {
        public string questions { get; set; }

        public string response_Type { get; set; }

        public bool? isRequired { get; set; }=false;

        public int assessmentId { get; set; }

        public Assessment Assessment { get; set; }
    }
}
