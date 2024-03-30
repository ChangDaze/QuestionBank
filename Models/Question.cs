namespace QuestionBank.Models
{
    public class Question
    {
        public int? pick_no { get; set; }
        public int question_id { get; set; }
        public string? question { get; set; }
        public List<Question>? sub_question { get; set; }
        public string? option { get; set; }
        public string? answer { get; set; }
    }
}
