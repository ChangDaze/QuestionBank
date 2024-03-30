using Microsoft.AspNetCore.Mvc;
using QuestionBank.DTOs;
using QuestionBank.Models;
using QuestionBank.ParameterObjects;
using System.Collections.Generic;

namespace QuestionBank.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QuestionController : Controller
    {
        [HttpGet]
        public IActionResult DisplayQuestions()
        {
            IActionResult result ;
            DisplayQuestionsDTO displayQuestionsDTO = new DisplayQuestionsDTO();
            displayQuestionsDTO.grades = new List<string> { "1", "2", "3", "4","5", "6" };
            displayQuestionsDTO.subjects = new List<string> { "a", "c", "c" };
            displayQuestionsDTO.papers = new List<string> { "A", "B", "C" };
            displayQuestionsDTO.types = new List<int> { 1, 2, 3, 4, 5, 6 };
            result = new OkObjectResult(
                        new resultObjectDTO<DisplayQuestionsDTO>() 
                        {
                            data=displayQuestionsDTO
                        }
                    );
            return result;
        }

        [HttpPost]
        public IActionResult PickQuestions(PickQuestionsPara para)
        {
            IActionResult result;
            List < Question > questions = new List<Question>() 
            { 
                new Question()
                {
                    pick_no = 1,
                    question_id = 1,
                    question = "question",
                    sub_question = null,
                    option = "A:a,B:b,C:c",
                    answer = "A"
                }
            };

            result = new OkObjectResult(
                        new resultArrayDTO<Question>() { data = questions }
                    );
            return result;
        }

        [HttpPost]
        public IActionResult InesrtQuestion(Question para)
        {
            IActionResult result;

            result = new OkObjectResult(
                        new resultDTO()
                    );
            return result;
        }

        [HttpPut]
        public IActionResult UpdateQuestion(Question para)
        {
            IActionResult result;

            result = new OkObjectResult(
                        new resultDTO()
                    );
            return result;
        }

        [HttpDelete]
        public IActionResult UpdateQuestion(int para)
        {
            IActionResult result;

            result = new OkObjectResult(
                        new resultDTO()
                    );
            return result;
        }
    }
}
