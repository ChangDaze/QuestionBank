namespace QuestionBank.POCOs
{
    public class PickQuestionsParameter
    {
        public string? exam_id { get; set; }
        public string? grade { get; set; }
        public string? subject { get; set; }
        public string? type { get; set; }
        public int count { get; set; }
    }
}
