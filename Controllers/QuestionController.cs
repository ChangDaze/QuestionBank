using Microsoft.AspNetCore.Mvc;
using QuestionBank.DataEntities;
using QuestionBank.Interfaces;
using QuestionBank.POCOs;
using QuestionBank.Services;
using System.Collections.Generic;
using System.Diagnostics;
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
        //問題:缺選題API
        [HttpGet]
        public IActionResult DisplayQuestions()
        {
            IActionResult result ;
            result = new OkObjectResult(
                        new QuestionBankResultArray<Question>()
                        {
                            data = _questionBankService.GetQuestionList()
                        }                        
                    );
            return result;
        }
        [HttpPost]
        public IActionResult GetQuestion(int question_id)
        {
            IActionResult result;

            result = new OkObjectResult(
                        new QuestionBankResultObject<Question>()
                        {
                            data = _questionBankService.GetQuestion(question_id)
                        }                        
                    );
            return result;
        }
        [HttpPost]
        public IActionResult InesrtQuestion(InesrtQuestionParameter parameter)
        {            
            _questionBankService.CreateQuestion(parameter);
            return new OkObjectResult(
                        new QuestionBankResult()
                    );
        }
        [HttpPut]
        public IActionResult UpdateQuestion(UpdateQuestionParameter parameter)
        {
            parameter.update_datetime = DateTime.Now;
            _questionBankService.UpdateQuestion(parameter);
            return new OkObjectResult(
                        new QuestionBankResult()
                    );
        }
        //問題:題組刪除
        [HttpDelete]
        public IActionResult DeleteQuestion(int question_id)
        {
            _questionBankService.DeleteQuestion(question_id);
            return new OkObjectResult(
                        new QuestionBankResult()
                    );
        }
    }
}
