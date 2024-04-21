using System.Runtime.CompilerServices;

namespace QuestionBank.Services
{
    public static class IDGeneraterService
    {
        private static int question_id;
        public static int GetNewQuestionID() 
        {
            question_id += 1;
            return question_id;
        }
        public static bool SetQuestionID(int? now_question_id)
        {
            if (now_question_id == null)
                question_id = 0;
            else
                question_id = (int)now_question_id;   
            
            return true;
        }
    }
}
