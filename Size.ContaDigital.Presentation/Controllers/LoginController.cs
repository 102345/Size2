using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Size.ContaDigital.Presentation.Contract;
using Size.ContaDigital.Presentation.ViewModels;

namespace Size.ContaDigital.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private string urlBaseApi = string.Empty;

        public LoginController(IConfiguration configuration)
        {
            urlBaseApi = configuration.GetValue<string>("API_Access:UrlBase");
      
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Logar([Bind] LoginViewModel loginViewModel)
        {
           
           
            var userContract = Autenticar(new Helpers.HttpHelper().ConfigurarHttpClient(), loginViewModel,urlBaseApi);

            if (userContract.Status)
            {
                int IdUser = userContract.Object.IdUsuario;
                string titular = userContract.Object.Nome;

                HttpContext.Session.SetInt32("_idUser", IdUser);
                HttpContext.Session.SetString("_Titular", titular);

                return RedirectToAction("Index", "ContaDigital");
               
            }

            return RedirectToAction("Index", "Login");
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }



        private UserContract Autenticar(HttpClient client, LoginViewModel loginViewModel, string urlBaseApi)
        {

            var userContract = new UserContract();

            var urlCompleta = urlBaseApi + "user/ValidaUsuario";

            HttpResponseMessage response = client.PostAsync(
                       urlCompleta, new StringContent(
                       JsonConvert.SerializeObject(new
                       {
                           Login = loginViewModel.Usuario,
                           Senha = loginViewModel.Senha
                       }), Encoding.UTF8, "application/json")).Result;

            string conteudo =
                  response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                userContract = JsonConvert.DeserializeObject<UserContract>(conteudo);
                
            }

            return userContract;
        }

    }
}