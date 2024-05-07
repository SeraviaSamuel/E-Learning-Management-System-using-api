namespace E_Learning_Management_System.DTO
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public double Mark { get; set; }
        public int? LearnerId { get; set; }
        public int? FeedbackId { get; set; }
        public int? TheQuizzesId { get; set; }
    }
}
