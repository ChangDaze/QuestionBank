using QuestionBank.DataEntities;
using QuestionBank.POCOs;

namespace QuestionBank.Services
{
    public interface IQuestionsService
    {
        public QuestionFilters GetQuestionFilters();
        public List<Question> GetQuestions();
        public Question? GetQuestion(int question_id);
        public List<DisplayQuestion> PickQuestions(PickQuestionsParameter parameter);
        public bool CreateQuestion(InesrtQuestionParameter parameter);
        public bool UpdateQuestion(UpdateQuestionParameter parameter);
        public bool DeleteQuestion(int question_id);
    }
}
