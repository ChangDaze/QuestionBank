using QuestionBank.DataEntities;
using QuestionBank.POCOs;

namespace QuestionBank.Interfaces
{
    public interface IQuestionBankRepository
    {
        public List<string> GetGradeFilters();
        public List<string> GetSubjectFilters();
        public List<string> GetExamIDFilters();
        public List<string> GetQuestionTypeFilters();
        public List<Question> GetQuestions();
        public Question? GetQuestion(int question_id);
        public List<BaseSignQuestion> PickQuestions(PickQuestionsParameter parameter, IPickQuestionsFilter filter);
        public bool CreateQuestion(Question question);
        public bool UpdateQuestion(UpdateQuestionParameter parameter);
        public bool DeleteQuestion(int question_id);
    }
}
