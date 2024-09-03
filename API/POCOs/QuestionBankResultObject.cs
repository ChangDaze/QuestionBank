namespace QuestionBank.POCOs
{
    //繼承成功的result結果
    public class QuestionBankResultObject<T> : QuestionBankResult
    {
        public T? data { get; set; }
    }
}
