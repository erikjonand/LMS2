﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LMS2.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [StringLength(200, ErrorMessage = "The description can at most be 200 characters long")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DefaultDateTimeValue("Now")]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DefaultDateTimeValue("Now")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "Urgent information")]
        public string UrgentInfo { get; set; }
        protected bool historic;
        public bool Historic
        {
            get
            {
                return historic;
            }
            set
            {
                if (EndDate < DateTime.Now.Date)
                    historic = true;
                else historic = false;
            }
        }


        //Appendices/Documents
        public virtual ICollection<File> Files { get; set; }

        [Display(Name = "students")]
        public virtual ICollection<ApplicationUser> AttendingStudents { get; set; }
        [Display(Name = "modules")]
        public virtual ICollection<Module> Modules { get; set; }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class DefaultDateTimeValueAttribute : ValidationAttribute
        {
            public string DefaultValue { get; set; }

            public DefaultDateTimeValueAttribute(string defaultValue)
            {
                DefaultValue = defaultValue;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                PropertyInfo property = validationContext.ObjectType.GetProperty(validationContext.MemberName);

                // Set default value only if no value is already specified 
                if (value == null)
                {
                    DateTime defaultValue = DateTime.Now.Date;
                    property.SetValue(validationContext.ObjectInstance, defaultValue);
                }

                return ValidationResult.Success;
            }
        }




    }
}