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
        public bool CreateQuestion(InesrtQuestionParameter inesrtQuestionParameter)
        {
            int parent_question_id = IDGeneraterService.GetNewQuestionID();
            int parent_question_volume = 1;
            if (inesrtQuestionParameter.sub_questions != null)
            {
                //更新母題題量
                parent_question_volume = inesrtQuestionParameter.sub_questions.Count;
                //新增子題
                foreach (InesrtQuestionParameter sub_question in inesrtQuestionParameter.sub_questions)
                {
                    Question subQuestion = new Question()
                    {
                        question_id = IDGeneraterService.GetNewQuestionID(), //產生新ID
                        exam_id = sub_question.exam_id,
                        exam_question_number = sub_question.exam_question_number,
                        grade = sub_question.grade,
                        subject = sub_question.subject,
                        type = sub_question.type,
                        content = sub_question.content,
                        option = sub_question.option,
                        answer = sub_question.answer,
                        parent_question_id = parent_question_id, //子題需有母題號
                        question_volume = 1, //子題固定為1
                        update_datetime = DateTime.Now,
                        update_user = sub_question.user,
                        create_datetime = DateTime.Now,
                        create_user = sub_question.user,
                    };
                    _questionsBankRepository.CreateQuestion(subQuestion);
                }
            }
            //新增母題
            Question parentQuestion = new Question()
            {
                question_id = parent_question_id,
                exam_id = inesrtQuestionParameter.exam_id,
                exam_question_number = inesrtQuestionParameter.exam_question_number,
                grade = inesrtQuestionParameter.grade,
                subject = inesrtQuestionParameter.subject,
                type = inesrtQuestionParameter.type,
                content = inesrtQuestionParameter.content,
                option = inesrtQuestionParameter.option,
                answer = inesrtQuestionParameter.answer,
                question_volume = parent_question_volume,
                update_datetime = DateTime.Now,
                update_user = inesrtQuestionParameter.user,
                create_datetime = DateTime.Now,
                create_user = inesrtQuestionParameter.user,
            };
            _questionsBankRepository.CreateQuestion(parentQuestion);                     
            return true;
        }
        public List<Question> GetQuestionList()
        {
            var questionList = _questionsBankRepository.GetQuestionList();
            return questionList;
        }
        public Question? GetQuestion(int question_id)
        {
            var question = _questionsBankRepository.GetQuestion(question_id);
            return question;
        }
        public bool UpdateQuestion(UpdateQuestionParameter updateQuestionParameter)
        {
            var updateEmployee =
                _questionsBankRepository.UpdateQuestion(updateQuestionParameter);
            return true;
        }
        public bool DeleteQuestion(int question_id)
        {
            var deleteQuestion = _questionsBankRepository.DeleteQuestion(question_id);
            return true;
        }        
    }
}
