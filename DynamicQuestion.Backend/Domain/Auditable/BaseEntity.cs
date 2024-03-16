using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auditable
{
    public abstract class BaseEntity<T>:IEntity
    {
        [Required]
        public T Id { get; set; }

        object IEntity.Id
        {
            get { return Id; }
            set { }
        }

        [Required]
        public string? CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOnUtc { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime LastModifiedOnUtc { get; set; }

        public bool IsDeleted { get; set; }
    }
}
