using System.Text;

namespace QuestionBank.Services
{
    public class GeneralService
    {
        public string TypePropertiesGenerateFilterSQLCommand<T>(T filter)
        {
            //GetType會取得instance，所以儘管進來的是interface，還是會照instance的原Type取值，所以取到的還是傳進來的filter的原property(而不是interface的)
            StringBuilder result = new StringBuilder("");
            var filterProperties = filter!.GetType().GetProperties();
            for (int index = 0, length = filterProperties.Length ; index < length; index++)
            {
                if(filterProperties[index].GetValue(filter) is not null)
                {
                    result.Append(" and ");
                    result.Append(filterProperties[index].Name);
                    result.Append(" = @");
                    result.Append(filterProperties[index].Name);
                }
            }
            return result.ToString();
        }
    }
}
