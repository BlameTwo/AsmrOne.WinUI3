using System.Text.Json;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Common
{
    public static class TokenInstance
    {
        public static void SaveToken(RegisterReponse reponse)
        {
            var token = JsonSerializer.Serialize(reponse, JsonContext.Default.RegisterReponse);
            GlobalUsing.Token = token;
        }

        public static RegisterReponse GetToken()
        {
            var localSettings = GlobalUsing.Token;
            return JsonSerializer.Deserialize(localSettings, JsonContext.Default.RegisterReponse);
        }
    }
}
