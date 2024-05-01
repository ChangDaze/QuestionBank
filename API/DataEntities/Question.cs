namespace QuestionBank.DataEntities
{
    public class Question
    {
        public int question_id { get; set; }
        public string? exam_id { get; set; }
        public string? exam_question_number { get; set; }
        public string? grade { get; set; }
        public string? subject { get; set; }
        public string? question_type { get; set; }
        public string? content { get; set; }
        public string? option { get; set; }
        public string? answer { get; set; }
        public int? parent_question_id { get; set; }
        public int question_volume { get; set; }
        public DateTime update_datetime { get; set; }
        public required string update_user { get; set; }
        public DateTime create_datetime { get; set; }
        public required string create_user { get; set; }

        //資料實體不能有其他建構式，不然就要自己實作預設建構式，不然會影響Dapper映射
    }
}
