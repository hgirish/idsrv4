using System;
using System.Collections.Generic;
using System.Text;
using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Constants
{
  public static   class TokenResponseExtensions
    {
        public static void Show(this TokenResponse response)
        {
            if (!response.IsError) {
               // "Token response:".ConsoleGreen();
                Console.WriteLine(response.Json);

                if (response.AccessToken.Contains(".")) {
                   // "\nAccess Token (decoded):".ConsoleGreen();

                    var parts = response.AccessToken.Split('.');
                    var header = parts[0];
                    var claims = parts[1];

                    Console.WriteLine($"header: {JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(header)))}");
                    Console.WriteLine($"Claims:  {JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(claims)))}" );
                }
            }
            else {
                if (response.IsError) {
                   // "HTTP error: ".ConsoleGreen();
                    Console.WriteLine($"Error status code: {response.HttpStatusCode}");
                  //  "HTTP error reason: ".ConsoleGreen();
                    Console.WriteLine($"Error Reason: {response.HttpErrorReason}");
                    Console.WriteLine(response.Raw);
                    Console.WriteLine(response.ErrorDescription);
                }
                else {
                  //  "Protocol error response:".ConsoleGreen();
                    Console.WriteLine($"response json: {response.Json}");
                }
            }
        }
    }
}
