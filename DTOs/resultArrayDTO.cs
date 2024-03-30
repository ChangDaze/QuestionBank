namespace QuestionBank.DTOs
{
    public class resultArrayDTO<T> : resultDTO
    {
        public List<T> data { get; set; }
    }
}
