using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IQuestionsService _questionsService;
        private IMemoryCache _memoryCache { get; set; } //要能修改
        public QuestionController(IQuestionsService questionBankService, IMemoryCache memoryCache)
        {
            _questionsService = questionBankService;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult GetQuestionFilters()
        {
            QuestionFilters? questionFilters;            
            // 嘗試取得指定的Cache
            if (!_memoryCache.TryGetValue("questionFilters", out questionFilters))//有拿到就會有out但不進入if
            {
                // 指定的Cache不存在，所以給予一個新的值
                questionFilters = _questionsService.GetQuestionFilters();
                // 把資料除存進Cache中，沒設MemoryCacheEntryOptions的話預設是永存
                _memoryCache.Set("questionFilters", questionFilters);
            }
            IActionResult result;
            result = new OkObjectResult(
                        new QuestionBankResultObject<QuestionFilters>()
                        {
                            data = questionFilters
                        }
                    );
            return result;
        }
        [HttpGet]
        public IActionResult GetQuestions()
        {
            IActionResult result ;
            result = new OkObjectResult(
                        new QuestionBankResultArray<Question>()
                        {
                            data = _questionsService.GetQuestions()
                        }                        
                    );
            return result;
        }
        [HttpGet]
        public IActionResult GetQuestion(int question_id)
        {
            IActionResult result;
            result = new OkObjectResult(
                        new QuestionBankResultObject<Question>()
                        {
                            data = _questionsService.GetQuestion(question_id)
                        }                        
                    );
            return result;
        }
        //因為Get Method不能有request body，用[FromQuery]可以讓swagger比較好測，沒有[FromQuery]還是能自動binding
        [HttpGet]
        public IActionResult PickQuestions([FromQuery]PickQuestionsParameter parameter) 
        {
            IActionResult result;
            result = new OkObjectResult(
                        new QuestionBankResultArray<DisplayQuestion>()
                        {
                            data = _questionsService.PickQuestions(parameter)
                        }
                    );
            return result;
        }
        [HttpPost]
        public IActionResult InesrtQuestion(InesrtQuestionParameter parameter)
        {
            _memoryCache.Remove("questionFilters"); //變動時要更新緩存
            _questionsService.CreateQuestion(parameter);
            return new OkObjectResult(
                        new QuestionBankResult()
                    );
        }
        [HttpPut]
        public IActionResult UpdateQuestion(UpdateQuestionParameter parameter)
        {
            _memoryCache.Remove("questionFilters"); //變動時要更新緩存
            parameter.update_datetime = DateTime.Now;
            _questionsService.UpdateQuestion(parameter);
            return new OkObjectResult(
                        new QuestionBankResult()
                    );
        }
        [HttpDelete]
        public IActionResult DeleteQuestion(int question_id)
        {
            _memoryCache.Remove("questionFilters"); //變動時要更新緩存
            _questionsService.DeleteQuestion(question_id);
            return new OkObjectResult(
                        new QuestionBankResult()
                    );
        }
    }
}
