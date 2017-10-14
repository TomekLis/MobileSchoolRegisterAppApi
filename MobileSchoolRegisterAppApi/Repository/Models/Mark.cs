namespace Repository.Models
{
    public class Mark : StudentActivity
    {
        //TODO: check whether parent constructor gets called without declaration in child class
        public Mark() : base()
        {
            
        }
        public MarkValue MarkValue { get; set; }
        public Importance Importance { get; set; }
    }
    public enum MarkValue
    {
        A, B, C, D, E, F
    }

    public enum Importance
    {
        Exam, ClassExam, Test, Quiz
    }
}