using QuestionBank.DataEntities;
using QuestionBank.POCOs;

namespace QuestionBank.Interfaces
{
    public interface IQuestionBankRepository
    {        
        List<Question> GetQuestions();
        Question? GetQuestion(int question_id);
        List<Question> PickQuestions(PickQuestionsParameter parameter);
        bool CreateQuestion(Question question);
        bool UpdateQuestion(UpdateQuestionParameter parameter);
        bool DeleteQuestion(int question_id);
    }
}
