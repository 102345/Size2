using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Size.ContaDigital.Model;
using Size.ContaDigital.Presentation.Contract;
using Size.ContaDigital.Presentation.Helpers;
using Size.ContaDigital.Presentation.ViewModels;
using AutoMapper;
using System;
using System.Text;

namespace Size.ContaDigital.Presentation.Controllers
{
    public class ContaDigitalController : Controller
    {
        private string urlBaseApi = string.Empty;
        private IMapper _mapper;

        public ContaDigitalController(IConfiguration configuration , IMapper mapper)
        {
            urlBaseApi = configuration.GetValue<string>("API_Access:UrlBase");
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            int idUser = HttpContext.Session.GetInt32("_idUser").Value;
            string titular = HttpContext.Session.GetString("_Titular");

            var contaContract = BuscarConta(new HttpHelper().ConfigurarHttpClient(), idUser, urlBaseApi);

            HttpContext.Session.SetObject("_contaContract", contaContract);

            var colecaoExtrato = ListarExtrato(new HttpHelper().ConfigurarHttpClient(), idUser, urlBaseApi);

            ViewData["Titular"] = titular;
            ViewData["Agencia"] = contaContract.Object.Agencia;
            ViewData["ContaCorrente"] = contaContract.Object.ContaCorrente;
            ViewData["Saldo"] = contaContract.Object.Saldo;

            if(colecaoExtrato.ToList().Count > 0)
            {
                var modelVM = _mapper.Map<IEnumerable<MovimentoConta>, IEnumerable<MovimentoContaViewModel>>(colecaoExtrato);
                return View(modelVM);
            }
            
           

            return View();
        }

        public IActionResult Deposita()
        {
            string titular = HttpContext.Session.GetString("_Titular");

            var contaContract = HttpContext.Session.GetObject<ContaContract>("_contaContract");

            ViewData["Titular"] = titular;
            ViewData["Agencia"] = contaContract.Object.Agencia;
            ViewData["ContaCorrente"] = contaContract.Object.ContaCorrente;
            ViewData["Saldo"] = contaContract.Object.Saldo;

            return View();
        }


        public IActionResult EfetivarDeposito(ContaViewModel contaVM)
        {

            var contaContract = HttpContext.Session.GetObject<ContaContract>("_contaContract");

            string valor1 = contaVM.Valor.Replace(".", "");

            string valor = valor1.Replace(",", ".");

            bool retOp = Depositar(new HttpHelper().ConfigurarHttpClient(), valor, Convert.ToString(contaContract.Object.IdConta), urlBaseApi);

            if (retOp)
            {
                ViewData["MsgOperacao"] = "Deposito feito com sucesso";
            }
            else
            {
                ViewData["MsgOperacao"] = "Problemas de operação de depósito";
            }

            return RedirectToAction("Deposita", "ContaDigital");
        }


        public IActionResult Saca()
        {
            string titular = HttpContext.Session.GetString("_Titular");

            var contaContract = HttpContext.Session.GetObject<ContaContract>("_contaContract");

            ViewData["Titular"] = titular;
            ViewData["Agencia"] = contaContract.Object.Agencia;
            ViewData["ContaCorrente"] = contaContract.Object.ContaCorrente;
            ViewData["Saldo"] = contaContract.Object.Saldo;

            return View();
        }





        public IActionResult EfetivarSaque(ContaViewModel contaVM)
        {

            var contaContract = HttpContext.Session.GetObject<ContaContract>("_contaContract");


            string valor1 = contaVM.Valor.Replace(".", "");

            string valor = valor1.Replace(",", ".");

            
            if(contaContract.Object.Saldo < Convert.ToDecimal(valor))
            {
                TempData["MsgOperacao"] = "Valor do saque não pode ser maior que saldo disponível em conta corrente";
                return RedirectToAction("Deposita", "ContaDigital");
            }



            bool retOp = Sacar(new HttpHelper().ConfigurarHttpClient(), valor, Convert.ToString(contaContract.Object.IdConta), urlBaseApi);

            if (retOp)
            {
                TempData["MsgOperacao"] = "Saque feito com sucesso";
            }
            else
            {
                TempData["MsgOperacao"] = "Problemas de operação de Saque";
            }

            return RedirectToAction("Deposita", "ContaDigital");
        }



        public IActionResult Transfere()
        {
            string titular = HttpContext.Session.GetString("_Titular");

            var contaContract = HttpContext.Session.GetObject<ContaContract>("_contaContract");

            ViewData["Titular"] = titular;
            ViewData["Agencia"] = contaContract.Object.Agencia;
            ViewData["ContaCorrente"] = contaContract.Object.ContaCorrente;
            ViewData["Saldo"] = contaContract.Object.Saldo;

            return View();
        }



        public IActionResult EfetivarTransferencia(ContaViewModel contaVM)
        {
            var contaDestino = BuscarContaAgenciaCC(new HttpHelper().ConfigurarHttpClient(), contaVM.Agencia, contaVM.ContaCorrente, urlBaseApi);

            if(contaDestino == null)
            {
                ViewData["MsgOperacao"] = "Não existe esta agência e conta corrente";
                return RedirectToAction("Deposita", "ContaDigital");
            }

            var contaContract = HttpContext.Session.GetObject<ContaContract>("_contaContract");

            if (contaDestino.Object.IdConta == contaContract.Object.IdConta)
            {
                ViewData["MsgOperacao"] = "Não pode transferir valor para a mesma conta corrente";
                return RedirectToAction("Deposita", "ContaDigital");
            }


           

            string valor1 = contaVM.Valor.Replace(".", "");

            string valor = valor1.Replace(",", ".");

            bool retOp = Transferir(new HttpHelper().ConfigurarHttpClient(), valor,
                                 Convert.ToString(contaContract.Object.IdConta),
                                 Convert.ToString(contaDestino.Object.IdConta), urlBaseApi);

            if (retOp)
            {
                ViewData["MsgOperacao"] = "Transferência feita com sucesso";
            }
            else
            {
                ViewData["MsgOperacao"] = "Problemas de operação de Transferência";
            }

            return RedirectToAction("Transfere", "ContaDigital");
        }


        private ContaContract BuscarContaAgenciaCC(HttpClient client,string agencia, string contacorrente, string urlBaseApi)
        {

            var contaContract = new ContaContract();

            var urlCompleta = urlBaseApi + string.Format("contadigital/BuscaContaPorAgenciaCC/?agencia={0}&contacorrente={1}",agencia,contacorrente);

            HttpResponseMessage response = client.GetAsync(
                       urlCompleta).Result;

            string conteudo =
                  response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                contaContract = JsonConvert.DeserializeObject<ContaContract>(conteudo);

            }

            return contaContract;
        }



        private bool Transferir(HttpClient client, string valor, string idContaOrigem, string idContaDestino, string urlBaseApi)
        {

            bool retOp = false;

            var urlCompleta = urlBaseApi + "contadigital/Transferencia";

            HttpResponseMessage response = client.PostAsync(
                       urlCompleta, new StringContent(
                       JsonConvert.SerializeObject(new
                       {
                           idContaOrigem = idContaOrigem,
                           idContaDestino = idContaDestino,
                           ValorOperacao = valor
                       }), Encoding.UTF8, "application/json")).Result;

            string conteudo =
                  response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                retOp = true;

            }

            return retOp;
        }





        private bool Sacar(HttpClient client, string valor, string idContaOrigem, string urlBaseApi)
        {

            bool retOp = false;

            var urlCompleta = urlBaseApi + "contadigital/Saque";

            HttpResponseMessage response = client.PostAsync(
                       urlCompleta, new StringContent(
                       JsonConvert.SerializeObject(new
                       {
                           idContaOrigem = idContaOrigem,
                           idContaDestino = "0",
                           ValorOperacao = valor
                       }), Encoding.UTF8, "application/json")).Result;

            string conteudo =
                  response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                retOp = true;

            }

            return retOp;
        }



        private bool Depositar(HttpClient client, string valor, string idContaOrigem, string urlBaseApi)
        {

            bool retOp = false;

            var urlCompleta = urlBaseApi + "contadigital/Deposita";

            HttpResponseMessage response = client.PostAsync(
                       urlCompleta, new StringContent(
                       JsonConvert.SerializeObject(new
                       {
                           idContaOrigem = idContaOrigem,
                           idContaDestino = "0",
                           ValorOperacao = valor
                       }), Encoding.UTF8, "application/json")).Result;

            string conteudo =
                  response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                retOp = true;

            }

            return retOp;
        }
        






        private IEnumerable<MovimentoConta> ListarExtrato(HttpClient client, int idUser, string urlBaseApi)
        {

            var modelVM = new MovimentoContaViewModel();

            var urlCompleta = urlBaseApi + string.Format("contadigital/ListaExtrato/{0}", idUser);

            HttpResponseMessage response = client.GetAsync(
                       urlCompleta).Result;

            string conteudo =
                  response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var movimentosContract = JsonConvert.DeserializeObject<MovimentoContaContract>(conteudo);

                if (movimentosContract != null)
                    modelVM.ColecaoMovimento = movimentosContract.Object;

            }

            return modelVM.ColecaoMovimento;


        }



        private ContaContract BuscarConta(HttpClient client,int idUser, string urlBaseApi)
        {

            var contaContract = new ContaContract();

            var urlCompleta = urlBaseApi + string.Format("contadigital/BuscaContaPorUsuario/{0}",idUser);

            HttpResponseMessage response = client.GetAsync(
                       urlCompleta).Result;

            string conteudo =
                  response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                contaContract = JsonConvert.DeserializeObject<ContaContract>(conteudo);

            }

            return contaContract;
        }

   

    // GET: ContaDigital/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }





       

        // POST: ContaDigital/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContaDigital/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContaDigital/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       
    }
}