﻿using API.Models;

namespace API.View_Models.Educations
{
    public class EducationVM
    {
        public Guid? Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }
    }
}
