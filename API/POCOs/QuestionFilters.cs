namespace QuestionBank.POCOs
{
    public class QuestionFilters
    {
        public required List<string> grades { get; set; }
        public required List<string> subjects { get; set; }
        public required List<string> exam_ids { get; set; }
        public required List<string> question_types { get; set; }
    }
}
