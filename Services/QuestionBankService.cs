using QuestionBank.DataEntities;
using QuestionBank.Interfaces;

namespace QuestionBank.Services
{
    public class QuestionBankService:IQuestionBankService
    {
        private readonly IDbService _dbService;
        public QuestionBankService(IDbService dbService)
        {
            _dbService = dbService;
        }
        public bool CreateQuestion(Question _question)
        {
            var result =
                _dbService.EditData(
                    @"INSERT INTO public.questions(
	                    question_id, exam_id, exam_question_number, grade, subject,
	                    type, content, option, answer, parent_question_id,
	                    question_volume, update_datetime, update_user, create_datetime, create_user)
                    VALUES (@question_id, @exam_id, @exam_question_number, @grade, @subject,
	                       @type, @content, @option, @answer, @parent_question_id,
	                       @question_volume, @update_datetime, @update_user, @create_datetime, @create_user);",
                    _question);
            return true;
        }
        public List<Question> GetQuestionList()
        {
            var questionList = _dbService.GetAll<Question>(
                @"SELECT question_id, exam_id, exam_question_number, grade, subject,
                        type, content, option, answer, parent_question_id,
                        question_volume, update_datetime, update_user, create_datetime, create_user 
                FROM public.questions", new { });
            return questionList;
        }
        public Question GetQuestion(long _question_id)
        {
            var question = _dbService.Get<Question>("SELECT question_id, exam_id, exam_question_number, grade, subject, type, content, option, answer, parent_question_id, question_volume, update_datetime, update_user, create_datetime, create_user FROM public.questions where question_id = @question_id", new { _question_id });
            return question;
        }
        public bool UpdateQuestion(Question _question)
        {
            var updateEmployee =
                _dbService.EditData(
                    @"UPDATE public.questions
                    SET exam_id=@exam_id, exam_question_number=@exam_question_number, grade=@grade,
	                    subject=@subject, type=@type, content=@content,
	                    option=@option, answer=@answer, parent_question_id=@parent_question_id,
	                    question_volume=@question_volume, update_datetime=@update_datetime, update_user=@update_user
                    WHERE question_id = @question_id;",
                    _question);
            return true;
        }
        public bool DeleteQuestion(long _question_id)
        {
            var deleteQuestion = _dbService.EditData("DELETE FROM public.questions where question_id = @question_id", new { _question_id });
            return true;
        }        
    }
}
