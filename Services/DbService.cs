using Dapper;
using Npgsql;
using System.Data;

namespace QuestionBank.Services
{
    public class DbService:IDbService
    {
        private readonly IDbConnection _db;
        private readonly IConfiguration _configuration;
        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new NpgsqlConnection(_configuration["ConnectionStrings:QuestionBankDB"]);
        }
        public T Get<T>(string command, object parms)
        {
            T result;
            result = _db.Query<T>(command, parms).FirstOrDefault();
            return result;
        }
        public List<T> GetAll<T>(string command, object parms)
        {
            List<T> result = new List<T>();
            result = _db.Query<T>(command, parms).ToList();
            return result;
        }
        public int EditData(string command, object parms)
        {
            int result;
            result = _db.Execute(command, parms);
            return result;
        }
        public static int? GetNowQuestionID(string connectionString)
        {
            IDbConnection db = new NpgsqlConnection(connectionString);
            int? now_question_id = db.Query<int?>("SELECT max(question_id) new_question_id FROM public.questions ", new { }).FirstOrDefault();
            db.Close();
            db.Dispose();
            return now_question_id;
        }
    }
}
