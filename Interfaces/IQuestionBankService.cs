using QuestionBank.DataEntities;

namespace QuestionBank.Interfaces
{
    public interface IQuestionBankService
    {
        bool CreateQuestion(Question question);
        List<Question> GetQuestionList();
        Question GetQuestion(long key);
        bool UpdateQuestion(Question question);
        bool DeleteQuestion(long key);
    }
}
