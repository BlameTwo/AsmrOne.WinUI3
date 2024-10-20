using System.Text.Json;
using AsmrOne.WinUI3.Models;
using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Common
{
    public static class TokenInstance
    {
        public static void SaveToken(RegisterReponse reponse)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var token = JsonSerializer.Serialize(reponse, JsonContext.Default.RegisterReponse);
            localSettings.Values["Token"] = token;
        }

        public static RegisterReponse GetToken()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var token = localSettings.Values["Token"];
            if (token == null)
            {
                return null;
            }
            return JsonSerializer.Deserialize(
                token.ToString(),
                JsonContext.Default.RegisterReponse
            );
        }
    }
}
