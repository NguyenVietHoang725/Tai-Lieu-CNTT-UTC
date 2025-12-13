namespace MyWebApp.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int LearnerID { get; set; }
        public float Grade { get; set; }
        public virtual Course? Course { get; set; }
        public virtual Learner? Learner { get; set; }
    }
}
