using Common.Api.Dtos;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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


        public async Task<bool> Refresh()
        {
            try
            {
                client = new HttpClient();

                RefreshRequest refreshRequest = new RefreshRequest(Barrel.Current.Get<LoginResult>("Login").RefreshToken);
                string data = JsonConvert.SerializeObject(refreshRequest);
                var contentRequest = new StringContent(data, Encoding.UTF8, "application/json");
                var uri = new Uri(string.Format("https://td-api.julienmialon.com/auth/refresh", string.Empty));
                var response = await client.PostAsync(uri, contentRequest);
                if (response.IsSuccessStatusCode)
                {
                    //Console.WriteLine("Test API REUSSI" + "  " + response.StatusCode);
                    var res = await response.Content.ReadAsStringAsync();
                    Response<LoginResult> jsonRes = JsonConvert.DeserializeObject<Response<LoginResult>>(res);
                    Barrel.Current.Add("Login", jsonRes.Data, TimeSpan.FromDays(1));
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

        public async Task<bool> Login(string mail, string mdp)
        {
            /*if (!Barrel.Current.IsExpired("Login"))
            {
                Console.WriteLine("Barrel Login ");
                return true;
            }*/
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
                    var res = await response.Content.ReadAsStringAsync();
                    Response<LoginResult> jsonRes = JsonConvert.DeserializeObject<Response<LoginResult>>(res);
                    Barrel.Current.Add("Login", jsonRes.Data, TimeSpan.FromDays(1));
                    return true;
                }
                else
                {
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
                    var res = await response.Content.ReadAsStringAsync();
                    Response<LoginResult> jsonRes = JsonConvert.DeserializeObject<Response<LoginResult>>(res);
                    Barrel.Current.Add("Login", jsonRes.Data, TimeSpan.FromDays(1));
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

        public async Task<bool> ModificationUser(string firstName, string lastName, int? imageId) 
        {
            try
            {
                client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                UpdateProfileRequest updateProfileRequest = new UpdateProfileRequest(firstName, lastName, imageId);
                string data = JsonConvert.SerializeObject(updateProfileRequest);
                var contentRequest = new StringContent(data, Encoding.UTF8, "application/json");
                var uri = new Uri(string.Format("https://td-api.julienmialon.com/me", string.Empty));
                HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), uri);
                requestMessage.Content = contentRequest;
                var response = await client.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Patch API worked");
                    await GetMe();
                    return true;
                }
                else
                {
                    Console.WriteLine("Patch API RATEE" + " " + response.StatusCode +"  " + response.RequestMessage  + "  " + response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }


        public async Task<bool> ModificationMdp(string oldMdp, string newMdp)
        {
            try
            {
                client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                UpdatePasswordRequest updatePasswordRequest = new UpdatePasswordRequest(oldMdp, newMdp);
                string data = JsonConvert.SerializeObject(updatePasswordRequest);
                var contentRequest = new StringContent(data, Encoding.UTF8, "application/json");
                var uri = new Uri(string.Format("https://td-api.julienmialon.com/me/password", string.Empty));
                HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), uri);
                requestMessage.Content = contentRequest;
                var response = await client.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Mdp changé");
                    await GetMe();
                    return true;
                }
                else
                {
                    Console.WriteLine("Mdp pas changé" + " " + response.StatusCode + "  " + response.RequestMessage + "  " + response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }


        public async Task<bool> GetMe()
        {
            try
            {
                client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);

                var uri = new Uri(string.Format("https://td-api.julienmialon.com/me", string.Empty));
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Response<UserItem> jsonRes = JsonConvert.DeserializeObject<Response<UserItem>>(res);
                    Barrel.Current.Add("Me",jsonRes.Data, TimeSpan.FromDays(1));
                    Console.WriteLine("Get API worked" + jsonRes.Data.FirstName );
                    return true;
                }
                else
                {
                    Console.WriteLine("Get API RATEE" + " " + response.StatusCode + "  " + response.RequestMessage + "  " + response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }


        public async Task<bool> GetPlaces()
        {
            try
            {
                client = new HttpClient();

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);

                var uri = new Uri(string.Format("https://td-api.julienmialon.com/places", string.Empty));
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Response <List<PlaceItemSummary>> jsonRes = JsonConvert.DeserializeObject<Response<List<PlaceItemSummary>>>(res);
                    Barrel.Current.Add("ListeLieux", jsonRes.Data, TimeSpan.FromDays(1));
                    Console.WriteLine("Get Lieux REUSSI");
                    return true;
                }
                else
                {
                    Console.WriteLine("Get Lieux RATEE" + " " + response.StatusCode + "  " + response.RequestMessage + "  " + response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }

        public async Task<bool> GetPlacesId(int idLieu)
        {
            try
            {
                client = new HttpClient();

                var uri = new Uri(string.Format("https://td-api.julienmialon.com/places/" + idLieu, string.Empty));
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Response<PlaceItem> jsonRes = JsonConvert.DeserializeObject<Response<PlaceItem>>(res);
                    Barrel.Current.Add("Lieu" + idLieu, jsonRes.Data, TimeSpan.FromDays(1));
                    Console.WriteLine("Get Lieu REUSSI");
                    return true;
                }
                else
                {
                    Console.WriteLine("Get Lieu RATEE" + " " + response.StatusCode + "  " + response.RequestMessage + "  " + response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }

        /*public async Task<bool> GetImages(int idImage)
        {
            try
            {
                client = new HttpClient();

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);

                var uri = new Uri(string.Format("https://td-api.julienmialon.com/images/" + idImage, string.Empty));
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Response<ImageItem> jsonRes = JsonConvert.DeserializeObject<Response<ImageItem>>(res);
                    Barrel.Current.Add("Image" + idImage, jsonRes.Data, TimeSpan.FromDays(1));
                    Console.WriteLine("Get Image REUSSI");
                    return true;
                }
                else
                {
                    Console.WriteLine("Get Image RATEE" + " " + response.StatusCode + "  " + response.RequestMessage + "  " + response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }*/

    }
}
