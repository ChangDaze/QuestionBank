using Dapper;
using Npgsql;
using System.Data;
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
        //取得目前question_id並設置
        public static bool SetQuestionID(string connectionString)
        {
            IDbConnection db = new NpgsqlConnection(connectionString);
            int? now_question_id = db.Query<int?>("SELECT max(question_id) new_question_id FROM public.questions ", new { }).FirstOrDefault();
            db.Close();
            db.Dispose();

            if (now_question_id == null)
                question_id = 0;
            else
                question_id = (int)now_question_id;   
            
            return true;
        }
    }
}
