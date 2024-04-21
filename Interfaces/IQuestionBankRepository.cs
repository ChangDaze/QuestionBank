using QuestionBank.DataEntities;
using QuestionBank.POCOs;

namespace QuestionBank.Interfaces
{
    public interface IQuestionBankRepository
    {
        bool CreateQuestion(Question question);
        List<Question> GetQuestionList();
        Question? GetQuestion(int question_id);
        bool UpdateQuestion(UpdateQuestionParameter updateQuestionParameter);
        bool DeleteQuestion(int question_id);
    }
}
