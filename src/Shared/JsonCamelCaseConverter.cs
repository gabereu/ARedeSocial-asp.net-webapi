using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace dotnetServer.Shared
{
    public static class JsonCamelCaseConverter
    {
        public static string Serialize(object target, bool processDictionaryKeys = false){
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy(){
                    ProcessDictionaryKeys = processDictionaryKeys,
                }
            };
            return JsonConvert.SerializeObject(
                target,
                new JsonSerializerSettings{
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                }
            );
        }
    }
}