using QuestionBank.Models;

namespace QuestionBank.Services
{
    public class QuestionBankService:IQuestionBankService
    {
        private readonly IDbService _dbService;
        public QuestionBankService(IDbService dbService)
        {
            _dbService = dbService;
        }
        public bool CreateQuestion(Question question)
        {
            var result =
                _dbService.EditData(
                    "INSERT INTO public.employee (id,name, age, address, mobile_number) VALUES (@Id, @Name, @Age, @Address, @MobileNumber)",
                    question);
            return true;
        }

        public List<Question> GetQuestionList()
        {
            var employeeList = _dbService.GetAll<Question>("SELECT * FROM public.employee", new { });
            return employeeList;
        }

        public Question GetQuestion(int id)
        {
            var employeeList = _dbService.Get<Question>("SELECT * FROM public.employee where id=@id", new { id });
            return employeeList;
        }

        public Question UpdateQuestion(Question employee)
        {
            var updateEmployee =
                _dbService.EditData(
                    "Update public.employee SET name=@Name, age=@Age, address=@Address, mobile_number=@MobileNumber WHERE id=@Id",
                    employee);
            return employee;
        }

        public bool DeleteQuestion(int id)
        {
            var deleteEmployee = _dbService.EditData("DELETE FROM public.employee WHERE id=@Id", new { id });
            return true;
        }
    }
}
