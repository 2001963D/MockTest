using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MockTest.Shared.Domain
{
    public class Test : BaseDomainModel, IValidatableObject
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Task Name does not meet length requirements")]
        public string TaskName { get; set; }
        [Required]
        public DateTime? DueDate { get; set; }
        public Boolean IsCompleted { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {   
            if (DueDate != null)
            {
                if (DueDate <= CreatedDate)
                {
                    yield return new ValidationResult("Due Date must be greater than Today", new[] { "Due Date" });
                }
            }
        }
    }
}
