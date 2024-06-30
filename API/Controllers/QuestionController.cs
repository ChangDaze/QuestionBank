using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuestionBank.DataEntities;
using QuestionBank.Global;
using QuestionBank.Interfaces;
using QuestionBank.POCOs;
using QuestionBank.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace QuestionBank.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class QuestionController : Controller
    {
        private readonly IQuestionsService _questionsService;
        private readonly ILogger<QuestionController> _logger;
        private IMemoryCache _memoryCache { get; set; } //要能修改
        public QuestionController(IQuestionsService questionBankService, IMemoryCache memoryCache, ILogger<QuestionController> logger)
        {
            _questionsService = questionBankService;
            _memoryCache = memoryCache;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetQuestionFilters()
        {
            _logger.LogInformation("【GetQuestionFilters】【Parameters】");
            QuestionFilters? questionFilters;            
            // 嘗試取得指定的Cache
            if (!_memoryCache.TryGetValue("questionFilters", out questionFilters))//有拿到就會有out但不進入if
            {
                // 指定的Cache不存在，所以給予一個新的值
                questionFilters = _questionsService.GetQuestionFilters();
                // 把資料除存進Cache中，沒設MemoryCacheEntryOptions的話預設是永存
                _memoryCache.Set("questionFilters", questionFilters);
            }
            
            var result = new OkObjectResult(
                        new QuestionBankResultObject<QuestionFilters>(){ data = questionFilters }
                    );

            _logger.LogInformation("【GetQuestionFilters】【Result】" + GlobalClass.JsonSerializeChineseEncode(result));
            return new OkObjectResult(result);
        }
        [HttpGet]
        public IActionResult GetQuestions()
        {
            _logger.LogInformation("【GetQuestions】【Parameters】");
            var result = new QuestionBankResultArray<Question>(){ data = _questionsService.GetQuestions()};
            _logger.LogInformation("【GetQuestions】【Result】" + GlobalClass.JsonSerializeChineseEncode(result));
            return new OkObjectResult(result);
        }
        [HttpGet]
        public IActionResult GetQuestion(int question_id)
        {
            _logger.LogInformation("【GetQuestion】【Parameters】" + question_id.ToString());
            var result = new QuestionBankResultObject<Question>(){ data = _questionsService.GetQuestion(question_id)};
            _logger.LogInformation("【GetQuestion】【Result】" + GlobalClass.JsonSerializeChineseEncode(result));
            return new OkObjectResult(result);
        }
        //因為Get Method不能有request body，用[FromQuery]可以讓swagger比較好測，沒有[FromQuery]還是能自動binding
        [HttpGet]
        public IActionResult PickQuestions([FromQuery]PickQuestionsParameter parameter) 
        {
            _logger.LogInformation("【PickQuestions】【Parameters】" + GlobalClass.JsonSerializeChineseEncode(parameter));
            var result = new QuestionBankResultArray<DisplayQuestion>(){data = _questionsService.PickQuestions(parameter)};
            _logger.LogInformation("【PickQuestions】【Result】" + GlobalClass.JsonSerializeChineseEncode(result));
            return new OkObjectResult(result);
        }
        [HttpPost]
        public IActionResult InesrtQuestion(InesrtQuestionParameter parameter)
        {
            _logger.LogInformation("【InesrtQuestion】【Parameters】" + GlobalClass.JsonSerializeChineseEncode(parameter));
            _memoryCache.Remove("questionFilters"); //變動時要更新緩存
            _questionsService.CreateQuestion(parameter);
            var result = new QuestionBankResult();
            _logger.LogInformation("【PickQuestions】【Result】" + GlobalClass.JsonSerializeChineseEncode(result));
            return new OkObjectResult(result);
        }
        [HttpPut]
        public IActionResult UpdateQuestion(UpdateQuestionParameter parameter)
        {
            _logger.LogInformation("【UpdateQuestion】【Parameters】" + GlobalClass.JsonSerializeChineseEncode(parameter));
            _memoryCache.Remove("questionFilters"); //變動時要更新緩存
            parameter.update_datetime = DateTime.Now;
            _questionsService.UpdateQuestion(parameter);
            var result = new QuestionBankResult();
            _logger.LogInformation("【UpdateQuestion】【Result】" + GlobalClass.JsonSerializeChineseEncode(result));
            return new OkObjectResult(result);
        }
        [HttpDelete]
        public IActionResult DeleteQuestion(int question_id)
        {
            _logger.LogInformation("【DeleteQuestion】【Parameters】" + question_id.ToString());
            _memoryCache.Remove("questionFilters"); //變動時要更新緩存
            _questionsService.DeleteQuestion(question_id);
            var result = new QuestionBankResult();
            _logger.LogInformation("【UpdateQuestion】【Result】" + GlobalClass.JsonSerializeChineseEncode(result));
            return new OkObjectResult(result);
        }
    }
}
