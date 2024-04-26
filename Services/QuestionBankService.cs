using QuestionBank.DataEntities;
using QuestionBank.Interfaces;
using QuestionBank.POCOs;
using System.Reflection.Metadata;

namespace QuestionBank.Services
{
    public class QuestionBankService:IQuestionBankService
    {
        private readonly IQuestionBankRepository _questionsBankRepository;
        public QuestionBankService(IQuestionBankRepository questionsBankRepository)
        {
            _questionsBankRepository = questionsBankRepository;
        }
        public List<Question> GetQuestions()
        {
            var questionList = _questionsBankRepository.GetQuestions();
            return questionList;
        }
        public Question? GetQuestion(int question_id)
        {
            var question = _questionsBankRepository.GetQuestion(question_id);
            return question;
        }
        public List<Question> PickQuestions(PickQuestionsParameter parameter)
        {
            var questionList = _questionsBankRepository.PickQuestions(parameter);
            return questionList;
        }
        public bool CreateQuestion(InesrtQuestionParameter parameter)
        {
            int parent_question_id = IDGeneraterService.GetNewQuestionID();
            bool sub_question_flag = parameter.sub_questions != null ? true : false; //起取出來的flag，true時下面才敢用!
            int parent_question_volume = sub_question_flag ? parameter.sub_questions!.Count : 1;

            //新增母題
            InesrtQuestionParameterCreateQuestion(parameter, null, parent_question_volume);//母題本身沒有母題id

            if (sub_question_flag)
            {                
                //新增子題
                foreach (InesrtQuestionParameter sub_question in parameter.sub_questions!)
                {
                    InesrtQuestionParameterCreateQuestion(sub_question, parent_question_id, 1); //子題單獨題量固定為1
                }
            }
                               
            return true;
        }
        public bool UpdateQuestion(UpdateQuestionParameter parameter)
        {
            var updateEmployee =
                _questionsBankRepository.UpdateQuestion(parameter);
            return true;
        }
        public bool DeleteQuestion(int question_id)
        {
            var deleteQuestion = _questionsBankRepository.DeleteQuestion(question_id);
            return true;
        }   
        
        private void InesrtQuestionParameterCreateQuestion(InesrtQuestionParameter inesrtQuestionParameter, int? parent_question_id, int question_volume)
        {
            Question subQuestion = new Question()
            {
                question_id = IDGeneraterService.GetNewQuestionID(), //產生新ID
                exam_id = inesrtQuestionParameter.exam_id,
                exam_question_number = inesrtQuestionParameter.exam_question_number,
                grade = inesrtQuestionParameter.grade,
                subject = inesrtQuestionParameter.subject,
                type = inesrtQuestionParameter.type,
                content = inesrtQuestionParameter.content,
                option = inesrtQuestionParameter.option,
                answer = inesrtQuestionParameter.answer,
                parent_question_id = parent_question_id, //子題需有母題號
                question_volume = question_volume, //子題固定為1
                update_datetime = DateTime.Now,
                update_user = inesrtQuestionParameter.user,
                create_datetime = DateTime.Now,
                create_user = inesrtQuestionParameter.user,
            };
            _questionsBankRepository.CreateQuestion(subQuestion);
        }
    }
}
