﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LMS2.Models
{
    public class Module
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be between {1} and {2} characters long", MinimumLength = 1)]
        [Display(Name = "Module name")]
        [Required]
        public string ModuleName
        {
            get
            { return moduleName; }
            set
            {
                moduleName = InitialCapital(value);
            }
        }
        protected string moduleName { get; set; }

        [StringLength(200, ErrorMessage = "The {0} must be between {1} and {2} characters long", MinimumLength = 1)]
        [Required]
        public string Description { get; set; }

        [DefaultDateTimeValue("Now")]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }

        [DefaultDateTimeValue("Now")]
        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(5000, ErrorMessage = "The {0} must be between {1} and {2} characters long", MinimumLength = 1)]
        [Display(Name = "Module info")]
        public string ModuleInfo { get; set; }

        public int CourseId { get; set; }
        public IEnumerable<Course> Courses { get; set; }


        //navigational property
        public virtual ICollection<File> Files { get; set; }
        public virtual Course Course { get; set; }
        [Display(Name = "activities")]
        public virtual ICollection<Activity> Activities { get; set; }

        /*        Appendices*/


        public string InitialCapital(string value)
        {
            if (value == null | value.Trim().Length == 0) value = "";
            if (value.Trim().Length > 1)
                value = value.Trim().Substring(0, 1).ToUpper() + value.Trim().Substring(1, value.Length - 1).ToLower();
            else
                value = value.Trim().ToUpper();
            return value;
        }
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