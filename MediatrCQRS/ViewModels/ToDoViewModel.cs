using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediatrCQRS.ViewModels
{
    public class ToDoViewModel : IValidatableObject
    {
        public int Id { get; set; }

        //Enable validation using annotations
        [Required]
        public string Name { get; set; }

        public bool Completed { get; set; }

        public ToDoViewModel()
        {

        }

        //Seconds method: enable internal validation agains itself
        public bool Validate_NoteIsNew()
        {
            if (this.Id == 0 && this.Name != null)
                return true;

            return false;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Validate_NoteIsNew())
                yield return new ValidationResult("Object addded to ModelState Validation");
        }
    }
}
