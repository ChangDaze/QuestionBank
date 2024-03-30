namespace QuestionBank.DTOs
{
    public class resultDTO
    {
        protected string _code = "0000";
        protected string _message = "執行成功";
        public string code 
        { 
            get { return _code; }
            set { _code = value; } 
        }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}

