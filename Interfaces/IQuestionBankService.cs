﻿using QuestionBank.DataEntities;
using QuestionBank.POCOs;

namespace QuestionBank.Interfaces
{
    public interface IQuestionBankService
    {
        bool CreateQuestion(InesrtQuestionParameter question);
        List<Question> GetQuestionList();
        Question? GetQuestion(int question_id);
        bool UpdateQuestion(UpdateQuestionParameter updateQuestionParameter);
        bool DeleteQuestion(int question_id);
    }
}
