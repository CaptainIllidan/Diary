using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Diary.Common.Extensions;

namespace Diary.Common
{
    public class EnumExport
    {
        public static string ExportEnumToJSON<T>() where T : Enum
        {
            var type = typeof(T);
            var values = (T[])Enum.GetValues(type);
            var dict = values.ToDictionary(e=> Convert.ToInt32(e), e => e.EnumDisplayNameFor());
            return JsonConvert.SerializeObject(dict);
        }
    }
}