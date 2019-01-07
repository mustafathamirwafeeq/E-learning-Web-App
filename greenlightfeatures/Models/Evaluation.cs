//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ELearning.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Evaluation
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public string Grade { get; set; }
        public System.DateTime ExamDate { get; set; }
        public int EvaluationTypeId { get; set; }
        public bool IsStudentEvaluation { get; set; }
        public Nullable<int> Efforts { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> A1 { get; set; }
        public Nullable<bool> A2 { get; set; }
        public Nullable<bool> B1 { get; set; }
        public Nullable<bool> B2 { get; set; }
        public Nullable<bool> C1 { get; set; }
        public Nullable<bool> C2 { get; set; }
    
        public virtual Exam Exam { get; set; }
        public virtual EvaluationType EvaluationType { get; set; }
    }
}