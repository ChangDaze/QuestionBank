namespace QuestionBank.POCOs
{
    public class QuestionBankResultObject<T> : QuestionBankResult
    {
        public T? data { get; set; }
    }
}
