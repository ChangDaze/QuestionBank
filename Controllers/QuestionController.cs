using Microsoft.AspNetCore.Mvc;
using QuestionBank.DataEntities;
using QuestionBank.DTOs;
using QuestionBank.Interfaces;
using QuestionBank.ParameterObjects;
using QuestionBank.Services;
using System.Collections.Generic;
using System.Reflection;

namespace QuestionBank.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QuestionController : Controller
    {
        private readonly IQuestionBankService _questionBankService;

        public QuestionController(IQuestionBankService questionBankService)
        {
            _questionBankService = questionBankService;
        }

        [HttpGet]
        public IActionResult DisplayQuestions()
        {
            IActionResult result ;
            result = new OkObjectResult(
                        _questionBankService.GetQuestionList()
                    );
            return result;
        }

        [HttpPost]
        public IActionResult PickQuestions(PickQuestionsPara _parameter)
        {
            IActionResult result;
            List < Question > questions = new List<Question>() 
            { 
                //new Question()
                //{
                //    pick_no = 1,
                //    question_id = 1,
                //    question = "question",
                //    sub_question = null,
                //    option = "A:a,B:b,C:c",
                //    answer = "A"
                //}
            };

            result = new OkObjectResult(
                        new resultArrayDTO<Question>() { data = questions }
                    );
            return result;
        }

        [HttpPost]
        public IActionResult InesrtQuestion(InesrtQuestionPara _parameter)
        {
            Question question = new Question(_parameter);
            question.question_id = IDGeneraterService.GetNewQuestionID();
            question.create_user = question.update_user = "Tom";
            question.create_datetime = question.update_datetime = DateTime.Now;
            _questionBankService.CreateQuestion(question);
            return new OkObjectResult(
                        new resultDTO()
                    );
        }

        [HttpPut]
        public IActionResult UpdateQuestion(UpdateQuestionPara _parameter)
        {
            Question question = new Question(_parameter);
            question.update_user = "Tom2";
            question.update_datetime = DateTime.Now;
            _questionBankService.UpdateQuestion(question);
            return new OkObjectResult(
                        new resultDTO()
                    );
        }

        [HttpDelete]
        public IActionResult DeleteQuestion(long _question_id)
        {
            _questionBankService.DeleteQuestion(_question_id);
            return new OkObjectResult(
                        new resultDTO()
                    );
        }
    }
}
