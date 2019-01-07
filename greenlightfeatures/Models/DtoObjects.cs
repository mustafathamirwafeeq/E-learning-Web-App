using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELearning.Models.Enum
{
    public enum UserCourseEnum
    {
        Started = 1,
        Finished = 2
    }

    public enum CMSPageEnum
    {
        Welcome = 1,
        AboutUs = 2,
        ContactUs = 3
    }

    public enum NewsAudience
    {
        All = 1,
        Instructors = 2,
        Students = 3,
        Class = 4
    }
    public enum WeeklyHoliday
    {
        Friday = 1,
        Saturday = 2
    }
    
}
namespace ELearning.Models.Dto
{
    //
    //[DataType(DataType.DateTime)]
    //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy hh:mm:ss tt}")]
    public class ClassDate
    {
        public DateTime Date { get; set; }
        public bool IsClass { get; set; }
        public string HolidayType { get; set; }
    }
    public class ExamDto
    {
        public string ExamTitle { get; set; }
        public string ExamType { get; set; }
        public string CoursTitle { get; set; }

        public List<EvalutionDto> Evalutions { get; set; } 
    }

    public class EvalutionDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Grade { get; set; }
        public DateTime ExamDate { get; set; }
        public Exam Exam { get; set; }
        public List<AspNetUsers> Students { get; set; }
        public List<EvaluationType> EvalTypes { get; set; }
    }
    public class ExamFullView
    {
        public Exam Exam { get; set; }
        public List<Evaluation> Evals { get; set; }
    }
    public class ForumReplyDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Answer { get; set; }
        public DateTime PubDate { get; set; }
        public string Question { get; set; }

        public int Id { get; set; }
    }

    public class UserCoursesDto
    {
        public int CourseId { get; set; }

        public string Course { get; set; }

        public string Department { get; set; }

        public string CourseDescription { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public bool Status { get; set; }

        public System.DateTime AddedDate { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }

        public int TotalExams { get; set; }

        public int ExamTaken { get; set; }
        public string StudentIds { get; set; }
    }
    public class AttendanceDTO
    {
        public Attendance Attendance { get; set; }
        public DateTime Date { get; set; }
        public int TotalStudents { get; set; }
        public int PresentStudents { get; set; }
        public bool IsPresent { get; set; }
    }

    public class BaseDetail
    {
        public TrainingBase Base { get; set; }
        public List<CourseClass> ClassesInitiated { get; set; }
        public List<CourseClass> ClassesCompleted { get; set; }
        public List<CourseClass> ClassesLive { get; set; }
        public int TotalInstructors { get; set; }
        public int TotalStudents { get; set; }
        public int LiveStudents { get; set; }
    }
    public class YearMonthClass
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthString { get; set; }
        public int ClassesInitiated { get; set; }
        public int ClassesCompleted { get; set; }
        public int StudentEnrolled { get; set; }
    }
    public class StudentClassDetail
    {
        public AspNetUsers Student { get; set; }
        public CourseClass Class { get; set; }
        public int Total { get; set; }
        public int Presents { get; set; }
        public int Absents { get; set; }
        public double OverAllEvaluationMarks { get; set; }
    }
}