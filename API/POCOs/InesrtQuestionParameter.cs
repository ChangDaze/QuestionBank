namespace QuestionBank.POCOs
{
    public class InesrtQuestionParameter
    {
        public string? exam_id { get; set; }
        public string? exam_question_number { get; set; }
        public string? grade { get; set; }
        public string? subject { get; set; }
        public string? question_type { get; set; }
        public string? content { get; set; }
        public string? option { get; set; }
        public string? answer { get; set; }
        public int? parent_question_id { get; set; }
        public int? question_volume { get; set; }
        public required string user { get; set; }
        public List<InesrtQuestionParameter>? sub_questions { get; set; }
    }
}
