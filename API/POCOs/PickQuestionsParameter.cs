namespace QuestionBank.POCOs
{
    public class PickQuestionsParameter : IPickQuestionsFilter
    {
        public string? exam_id { get; set; }
        public string? grade { get; set; }
        public string? subject { get; set; }
        public string? question_type { get; set; }
        public int data_count { get; set; }
    }
}
