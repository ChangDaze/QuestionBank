using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace QuestionBank.Global
{
    public static class GlobalClass
    {
        /// <summary>
        /// 從filter相關型別取出屬性產生過濾用SQL語法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static string TypePropertiesGenerateFilterSQLCommand<T>(T filter)
        {
            //GetType會取得instance，所以儘管進來的是interface，還是會照instance的原Type取值，所以取到的還是傳進來的filter的原property(而不是interface的)
            StringBuilder result = new StringBuilder("");
            var filterProperties = filter!.GetType().GetProperties();
            for (int index = 0, length = filterProperties.Length; index < length; index++)
            {
                if (filterProperties[index].GetValue(filter) is not null)
                {
                    result.Append(" and ");
                    result.Append(filterProperties[index].Name);
                    result.Append(" = @");
                    result.Append(filterProperties[index].Name);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// JsonSerializer.Serialize加上JsonSerializerOptions讓中文正常顯示
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        public static string JsonSerializeChineseEncode(object _object)
        {
            return JsonSerializer.Serialize(_object,
                new JsonSerializerOptions { 
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
                     }
                );
        }
    }
}
