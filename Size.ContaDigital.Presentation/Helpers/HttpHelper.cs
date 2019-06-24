using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Size.ContaDigital.Presentation.Helpers
{
    public class HttpHelper
    {   
        private  string _urlBase;



        public HttpClient ConfigurarHttpClient()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");

            var config = builder.Build();

            _urlBase = config.GetSection("API_Access:UrlBase").Value;

            HttpClientHandler handler = new HttpClientHandler();

            var client = new HttpClient(handler,false);
            
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                // Envio da requisição a fim de autenticar
                // e obter o token de acesso
                HttpResponseMessage respToken = client.PostAsync(
                    _urlBase + "login", new StringContent(
                        JsonConvert.SerializeObject(new
                        {
                            userID = config.GetSection("API_Access:UserID").Value,
                            Password = config.GetSection("API_Access:Password").Value
                        }), Encoding.UTF8, "application/json")).Result;

                string conteudo =
                    respToken.Content.ReadAsStringAsync().Result;
                Console.WriteLine(conteudo);

                if (respToken.StatusCode == HttpStatusCode.OK)
                {
                    Token token = JsonConvert.DeserializeObject<Token>(conteudo);
                    if (token.Authenticated)
                    {
                        // Associar o token aos headers do objeto
                        // do tipo HttpClient
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token.AccessToken);

                    }
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return client;
                  
        }   
    }
}
