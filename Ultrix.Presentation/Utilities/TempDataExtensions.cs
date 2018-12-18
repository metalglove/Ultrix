using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Ultrix.Presentation.Utilities
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out object o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
        public static bool Peek<T>(this ITempDataDictionary tempData, string key, out T item) where T : class
        {
            object o = tempData.Peek(key);
            if (o == null)
            {
                item = null;
                return false;
            }
            item = JsonConvert.DeserializeObject<T>((string)o);
            return true;
        }
    }
}
