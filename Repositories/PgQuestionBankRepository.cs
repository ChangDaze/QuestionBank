﻿using Dapper;
using Npgsql;
using QuestionBank.DataEntities;
using QuestionBank.Interfaces;
using QuestionBank.POCOs;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace QuestionBank.Repositories
{
    public class PgQuestionBankRepository : IQuestionBankRepository
    {
        private readonly IDbConnection _db;
        private readonly IConfiguration _configuration;
        public PgQuestionBankRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new NpgsqlConnection(_configuration["ConnectionStrings:QuestionBankDB"]);
        }
        #region 底層連線方法
        private T? Get<T>(string command, object parms)
        {
            var result = _db.Query<T>(command, parms).FirstOrDefault();
            return result;
        }
        private List<T> GetAll<T>(string command, object parms)
        {
            List<T> result = new List<T>();
            result = _db.Query<T>(command, parms).ToList();
            return result;
        }
        private int EditData(string command, object parms)
        {
            int result;
            result = _db.Execute(command, parms);
            return result;
        }
        #endregion
        public List<Question> GetQuestions()
        {
            var questions = this.GetAll<Question>(
                @"SELECT question_id, exam_id, exam_question_number, grade, subject,
                        type, content, option, answer, parent_question_id,
                        question_volume, update_datetime, update_user, create_datetime, create_user 
                FROM public.questions", new { });
            return questions;
        }
        public Question? GetQuestion(int question_id)
        {
            var question = this.Get<Question>("SELECT question_id, exam_id, exam_question_number, grade, subject, type, content, option, answer, parent_question_id, question_volume, update_datetime, update_user, create_datetime, create_user FROM public.questions where question_id = @question_id", new { question_id });
            return question;
        }
        public List<Question> PickQuestions(PickQuestionsParameter parameter)
        {
            var questions = this.GetAll<Question>(
                @"select 
	                question_id, exam_id, exam_question_number, grade, subject,
	                type, content, option, answer, parent_question_id,
	                question_volume, update_datetime, update_user, create_datetime, create_user 
                from public.pick_questions()", new { parameter });
            return questions;
        }
        public bool CreateQuestion(Question question)
        {
            var result =
                this.EditData(
                    @"INSERT INTO public.questions(
	                    question_id, exam_id, exam_question_number, grade, subject,
	                    type, content, option, answer, parent_question_id,
	                    question_volume, update_datetime, update_user, create_datetime, create_user)
                    VALUES (@question_id, @exam_id, @exam_question_number, @grade, @subject,
	                       @type, @content, @option, @answer, @parent_question_id,
	                       @question_volume, @update_datetime, @update_user, @create_datetime, @create_user);",
                    question);
            return true;
        }
        public bool UpdateQuestion(UpdateQuestionParameter parameter)
        {
            var updateEmployee =
                this.EditData(
                    @"UPDATE public.questions
                        SET exam_id=@exam_id, exam_question_number=@exam_question_number, grade=@grade,
	                        subject=@subject, type=@type, content=@content,
	                        option=@option, answer=@answer, update_datetime=@update_datetime,
	                        update_user=@update_user
                        WHERE question_id = @question_id;",
                    parameter);
            return true;
        }
        public bool DeleteQuestion(int question_id)
        {
            var deleteQuestion = this.EditData(
                                        @"DELETE FROM public.questions
                                            WHERE question_id in (
	                                            select distinct T2.question_id from
		                                            (select question_id from public.questions where question_id = @question_id) T1
		                                            join public.questions T2
			                                            on	T1.question_id = T2.question_id or T1.question_id = T2.parent_question_id
                                            )", new { question_id });
            return true;
        }
    }
}
