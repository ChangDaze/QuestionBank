namespace QuestionBank.DTOs
{
    public class resultObjectDTO<T> : resultDTO
    {
        public T data { get; set; }
    }
}
