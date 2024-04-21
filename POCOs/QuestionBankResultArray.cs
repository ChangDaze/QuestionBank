namespace QuestionBank.POCOs
{
    public class QuestionBankResultArray<T> : QuestionBankResult
    {        
        public List<T>? data { get; set; }
    }
}
