using QuestionBank.Models;

namespace QuestionBank.Services
{
    public interface IQuestionBankService
    {
        bool CreateQuestion(Question question);
        List<Question> GetQuestionList();
        Question GetQuestion(int key);
        Question UpdateQuestion(Question question);
        bool DeleteQuestion(int key);
    }
}
