using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;

namespace Td1.Data
{
    public class RestService
    {
      

        HttpClient client;


        public RestService()
        {
           
            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;
        }


        public async Task<bool> Login(string mail, string mdp)
        {
            try
            {
                client = new HttpClient();

                var uri = new Uri(string.Format("https://td-api.julienmialon.com/auth/login", string.Empty));
                LoginRequest loginRequest = new LoginRequest(mail, mdp);
                string data = JsonConvert.SerializeObject(loginRequest);
                var contentRequest = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, contentRequest);
                if (response.IsSuccessStatusCode)
                {
                    //Console.WriteLine("Test API REUSSI" + response.StatusCode);
                    return true;
                }
                else
                {
                    //Console.WriteLine("Test API RATEE" + response.StatusCode);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }

        

        public async Task<bool> Register(string mail, string fisrtName, string lastName, string mdp)
        {
            try
            {
                client = new HttpClient();

                RegisterRequest registerRequest = new RegisterRequest(mail, fisrtName, lastName, mdp);
                string data = JsonConvert.SerializeObject(registerRequest);
                var contentRequest = new StringContent(data, Encoding.UTF8, "application/json");
                var uri = new Uri(string.Format("https://td-api.julienmialon.com/auth/register", string.Empty));
                var response = await client.PostAsync(uri, contentRequest);
                if (response.IsSuccessStatusCode)
                {
                    //Console.WriteLine("Test API REUSSI" + "  " + response.StatusCode);
                    return true;
                }
                else
                {
                    //Console.WriteLine("Test API RATEE" + " " + response.StatusCode +"  " + response.RequestMessage  + "  " + response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }



    }
}
