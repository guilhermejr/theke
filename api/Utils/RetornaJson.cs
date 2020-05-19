using System;
using Newtonsoft.Json;

namespace api.Utils
{
    public static class RetornaJson
    {
        public static string Retornar(object model)
        {
            return JsonConvert.SerializeObject(model, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}
