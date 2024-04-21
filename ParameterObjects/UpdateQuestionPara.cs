namespace QuestionBank.ParameterObjects
{
    public class UpdateQuestionPara
    {
        public int question_id { get; set; }
        public string exam_id { get; set; }
        public string exam_question_number { get; set; }
        public string grade { get; set; }
        public string subject { get; set; }
        public string type { get; set; }
        public string content { get; set; }
        public string option { get; set; }
        public string answer { get; set; }
    }
}
