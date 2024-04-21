namespace QuestionBank.POCOs
{
    public class UpdateQuestionParameter
    {
        public int question_id { get; set; }
        public string? exam_id { get; set; }
        public string? exam_question_number { get; set; }
        public string? grade { get; set; }
        public string? subject { get; set; }
        public string? type { get; set; }
        public string? content { get; set; }
        public string? option { get; set; }
        public string? answer { get; set; }
        public required string update_user { get; set; }
        public DateTime? update_datetime { get; set; }
    }
}
