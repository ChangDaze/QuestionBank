using QuestionBank.ParameterObjects;

namespace QuestionBank.DataEntities
{
    public class Question
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
        public int? parent_question_id { get; set; }
        public int question_volume { get; set; }
        public DateTime update_datetime { get; set; }
        public string update_user { get; set; }
        public DateTime create_datetime { get; set; }
        public string create_user { get; set; }

        public Question(InesrtQuestionPara _parameter)
        {
            exam_id = _parameter.exam_id;
            exam_question_number = _parameter.exam_question_number;
            grade = _parameter.grade;
            subject = _parameter.subject;
            type = _parameter.type;
            content = _parameter.content;
            option = _parameter.option;
            answer = _parameter.answer;
        }

        public Question(UpdateQuestionPara _parameter)
        {
            question_id = _parameter.question_id;
            exam_id = _parameter.exam_id;
            exam_question_number = _parameter.exam_question_number;
            grade = _parameter.grade;
            subject = _parameter.subject;
            type = _parameter.type;
            content = _parameter.content;
            option = _parameter.option;
            answer = _parameter.answer;
        }
    }
}
