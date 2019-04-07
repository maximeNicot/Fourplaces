using Common.Api.Dtos;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                    Console.WriteLine("Login REUSSI" + "  " + response.StatusCode);
                    var res = await response.Content.ReadAsStringAsync();
                    Response<LoginResult> jsonRes = JsonConvert.DeserializeObject<Response<LoginResult>>(res);
                    Barrel.Current.Add("Login", jsonRes.Data, TimeSpan.FromDays(1));
                    return true;
                }
                else
                {
                    Console.WriteLine("Login RATEE" + "  " + response.StatusCode);
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
                    Console.WriteLine("Test Register REUSSI" + "  " + response.StatusCode);
                    var res = await response.Content.ReadAsStringAsync();
                    Response<LoginResult> jsonRes = JsonConvert.DeserializeObject<Response<LoginResult>>(res);
                    Barrel.Current.Add("Login", jsonRes.Data, TimeSpan.FromDays(1));
                    return true;
                }
                else
                {
                    Console.WriteLine("Test Register RATEE" + " " + response.StatusCode +"  " + response.RequestMessage  + "  " + response.ReasonPhrase);
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

                    Console.WriteLine("TESTE updateProfileRequest = " + updateProfileRequest.FirstName +" " + updateProfileRequest.LastName + updateProfileRequest.ImageId);
                    Console.WriteLine("Patch User API worked");
                    await GetMe();
                    return true;
                }
                else
                {
                    if (await Refresh())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                        response = await client.SendAsync(requestMessage);
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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
                    if (await Refresh())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                        response = await client.SendAsync(requestMessage);
                        if (response.IsSuccessStatusCode){
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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
                    Barrel.Current.Add("Me", jsonRes.Data, TimeSpan.FromDays(1));

                    Console.WriteLine("Get API worked Firstname = " + jsonRes.Data.FirstName  + "Lastname = " + jsonRes.Data.LastName);
                    return true;
                }
                else
                {
                    if (await Refresh())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                        response = await client.GetAsync(uri);
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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

        public async Task<bool> GetPlaceId(int idLieu)
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

        public async Task<bool> NouveauCommentaire(string commentaire, int idLieu)
        {
            try
            {
                client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                var uri = new Uri(string.Format("https://td-api.julienmialon.com/places/" + idLieu + "/comments", string.Empty));
                CreateCommentRequest createCommentRequest = new CreateCommentRequest(commentaire);
                string data = JsonConvert.SerializeObject(createCommentRequest);
                var contentRequest = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, contentRequest);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Post commentaire REUSSI");
                    return true;
                }
                else
                {
                    if (await Refresh())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                        response = await client.PostAsync(uri, contentRequest);
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }

        public async Task<bool> NouveauLieu(string title, string description, int imageId, double Latitude, double longitude)
        {
            try
            {
                client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                var uri = new Uri(string.Format("https://td-api.julienmialon.com/places", string.Empty));
                CreatePlaceRequest createPlaceRequest = new CreatePlaceRequest(title, description, imageId, Latitude, longitude);
                string data = JsonConvert.SerializeObject(createPlaceRequest);
                var contentRequest = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, contentRequest);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Post Lieu REUSSI");
                    return true;
                }
                else
                {
                    if (await Refresh())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                        response = await client.PostAsync(uri, contentRequest);
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"		ERROR {0}", e.Message);
                return false;
            }
        }

        public async Task<bool> NouvelleImage(String imagePath)
        {
            try
            {
                client = new HttpClient();
                byte[] imageData  = File.ReadAllBytes(imagePath);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);

                MultipartFormDataContent requestContent = new MultipartFormDataContent();

                var imageContent = new ByteArrayContent(imageData);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

                // Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
                requestContent.Add(imageContent, "file", "file.jpg");

                request.Content = requestContent;

                HttpResponseMessage response = await client.SendAsync(request);

                string result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Image uploaded!");
                    return true;
                }
                else
                {
                    if (await Refresh())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Barrel.Current.Get<LoginResult>("Login").AccessToken);
                        response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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
