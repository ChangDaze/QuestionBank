using QuestionBank.DataEntities;
using QuestionBank.Interfaces;
using QuestionBank.POCOs;
using System.Reflection.Metadata;

namespace QuestionBank.Services
{
    public class QuestionsService:IQuestionsService
    {
        private readonly IQuestionBankRepository _questionsBankRepository;
        public QuestionsService(IQuestionBankRepository questionsBankRepository)
        {
            _questionsBankRepository = questionsBankRepository;
        }
        public QuestionFilters GetQuestionFilters()
        {
            QuestionFilters questionFilters = new QuestionFilters()
            {
                grades = _questionsBankRepository.GetGradeFilters(),
                subjects = _questionsBankRepository.GetSubjectFilters(),
                exam_ids = _questionsBankRepository.GetExamIDFilters(),
                question_types = _questionsBankRepository.GetQuestionTypeFilters(),
            };
            return questionFilters;
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
        public List<DisplayQuestion> PickQuestions(PickQuestionsParameter parameter)
        {
            //缺Pick all (nolimit)
            //缺減少至約略數量
            //取出題數標準
            int data_count = parameter.data_count;
            //建立filter
            IPickQuestionsFilter filter = new PickQuestionsFilter()
            {
                exam_id = parameter.exam_id,
                grade = parameter.grade,
                subject = parameter.subject,
                question_type = parameter.question_type,
            };
            //取得BaseSignQuestion
            List<BaseSignQuestion> questionList = _questionsBankRepository.PickQuestions(parameter, filter);
            //取得parent question
            List<DisplayQuestion> parent_questions = questionList
                        .Where(x => x.parent_question_id is null)
                        .Select(x => new DisplayQuestion()
                                    {
                                        question_id = x.question_id,
                                        exam_id = x.exam_id,
                                        exam_question_number = x.exam_question_number,
                                        grade = x.grade,
                                        subject = x.subject,
                                        question_type = x.question_type,
                                        content = x.content,
                                        option = x.option,
                                        answer = x.answer,
                                        question_volume = x.question_volume
                                    }).ToList();
            //取得parent question id
            Dictionary<int, List<DisplayQuestion>> questionsMap = 
                parent_questions
                .Select(x=> new {x.question_id, sub_questions = new List<DisplayQuestion>()}) //因為question_id是PK所以不用特別distinct
                .ToDictionary(x=>x.question_id,x=>x.sub_questions);
            //取得sub question
            var sub_questions = questionList.Where(x => x.parent_question_id is not null);
            //歸類sub question
            foreach(var sub_question in sub_questions)
            {
                //因為取出來的值是join的，所以一定有key
                questionsMap[(int)sub_question.parent_question_id!].Add(
                        new DisplayQuestion()
                        {
                            question_id = sub_question.question_id,
                            exam_id = sub_question.exam_id,
                            exam_question_number = sub_question.exam_question_number,
                            grade = sub_question.grade,
                            subject = sub_question.subject,
                            question_type = sub_question.question_type,
                            content = sub_question.content,
                            option = sub_question.option,
                            answer = sub_question.answer
                        });
            }
            //將總題數減至data_count以下
            while(parent_questions.Sum(x=>x.question_volume) > data_count && parent_questions.Any())
            {
                parent_questions.RemoveAt(0);
            }
            //組合display question
            foreach (var parent_question in parent_questions)
            {
                parent_question.sub_questions = questionsMap[parent_question.question_id];
            }
            return parent_questions;
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
                question_type = inesrtQuestionParameter.question_type,
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
