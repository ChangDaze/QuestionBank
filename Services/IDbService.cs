namespace QuestionBank.Services
{
    public interface IDbService
    {
        T Get<T>(string command, object parms);
        List<T> GetAll<T>(string command, object parms);
        int EditData(string command, object parms);
    }
}
