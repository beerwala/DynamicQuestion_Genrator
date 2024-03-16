using Domain.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Assessment:BaseEntity<int>,IEntity
    {
        public string name { get; set; }

        public bool? isScorable { get; set; } = false;

        public IList<AssessmentQuestons> assessmentsQuestions { get; set; }
    }
}
