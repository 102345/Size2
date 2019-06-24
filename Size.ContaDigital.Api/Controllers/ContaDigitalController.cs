using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Size.ContaDigital.Api.ViewModels;
using Size.ContaDigital.Service.Interface;

namespace Size.ContaDigital.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaDigitalController : ControllerJsonBase
    {
        private IContaDigitalService _contaDigitalService;


        public ContaDigitalController(IContaDigitalService contaDigitalService)
        {
            _contaDigitalService = contaDigitalService;
        }


        // GET: api/ListaExtrato/5
        [Authorize("Bearer")]
        [HttpGet("ListaExtrato/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);
                }

                var lista = _contaDigitalService.ListarExtrato(id);

                JsonResult.Status = true;
                JsonResult.Message = "Lista encontrada.";
                JsonResult.Object = lista;
                return Ok(JsonResult);
            }
            catch (Exception ex)
            {
                JsonResult.Status = false;
                JsonResult.Message = ex.Message; 
                return BadRequest(JsonResult);
            }

          
        }


        [Authorize("Bearer")]
        [HttpGet("BuscaContaPorUsuario/{idUser}")]
        public IActionResult BuscaContaPorUsuario(int idUser)
        {
            try
            {
                if (idUser == 0)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);
                }

                var conta = _contaDigitalService.BuscarContaPorUsuario(idUser);

                JsonResult.Status = true;
                JsonResult.Message = "Conta encontrada.";
                JsonResult.Object = conta;
                return Ok(JsonResult);
            }
            catch (Exception ex)
            {
                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return BadRequest(JsonResult);
            }


        }


        [Authorize("Bearer")]
        [HttpGet("BuscaConta/{documento}")]
        public IActionResult BuscaConta(string documento)
        {
            try
            {
                if (string.IsNullOrEmpty(documento))
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);
                }

                var conta = _contaDigitalService.BuscarContaPorUsuario(documento);

                JsonResult.Status = true;
                JsonResult.Message = "Conta encontrada.";
                JsonResult.Object = conta;
                return Ok(JsonResult);
            }
            catch (Exception ex)
            {
                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return BadRequest(JsonResult);
            }


        }


        [Authorize("Bearer")]
        [HttpGet("BuscaContaPorAgenciaCC")]
        public IActionResult BuscaContaPorAgenciaCC([FromQuery] string agencia , [FromQuery] string contacorrente)
        {
            try
            {
                if (string.IsNullOrEmpty(agencia))
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);
                }

                if (string.IsNullOrEmpty(contacorrente))
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);
                }

                var conta = _contaDigitalService.BuscarConta(agencia, contacorrente);

                JsonResult.Status = true;
                JsonResult.Message = "Conta encontrada.";
                JsonResult.Object = conta;
                return Ok(JsonResult);
            }
            catch (Exception ex)
            {
                JsonResult.Status = false;
                JsonResult.Message = ex.Message;
                return BadRequest(JsonResult);
            }

        }



        [Authorize("Bearer")]
        [HttpPost("Deposita")]
        public IActionResult Deposita([FromBody] OperacaoContaViewModel operacao)
        {
            try
            {
                if(operacao.IdContaOrigem == 0)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);

                }

                var contaOrigem = _contaDigitalService.BuscarContaPorUsuario(operacao.IdContaOrigem);

                if(contaOrigem == null)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);

                }

                bool ret = _contaDigitalService.Depositar(contaOrigem, operacao.ValorOperacao);

                JsonResult.Status = true;
                JsonResult.Message = "Deposito efetuado com sucesso";
                JsonResult.Object = null;
                return Ok(JsonResult);

            }
            catch (Exception ex)
            {
                JsonResult.Status = false;
                JsonResult.Message = "404 Not Found";
                return BadRequest(JsonResult);

            }



        }


        [HttpPost("Saque")]
        public IActionResult Saque([FromBody] OperacaoContaViewModel operacao)
        {
            try
            {
                if (operacao.IdContaOrigem == 0)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);

                }

                var contaOrigem = _contaDigitalService.BuscarContaPorUsuario(operacao.IdContaOrigem);

                if (contaOrigem == null)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);

                }

                bool ret = _contaDigitalService.Sacar(contaOrigem, operacao.ValorOperacao);

                JsonResult.Status = true;
                JsonResult.Message = "Saque efetuado com sucesso";
                JsonResult.Object = null;
                return Ok(JsonResult);

            }
            catch (Exception ex)
            {
                JsonResult.Status = false;
                JsonResult.Message = "404 Not Found";
                return BadRequest(JsonResult);

            }
        }

        [HttpPost("Transferencia")]
        public IActionResult Transferencia([FromBody] OperacaoContaViewModel operacao)
        {
            try
            {
                if (operacao.IdContaOrigem == 0)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);

                }


                var contaOrigem = _contaDigitalService.BuscarContaPorUsuario(operacao.IdContaOrigem);

                if (contaOrigem == null)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);

                }

                if(contaOrigem.Saldo == 0)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "Operação inválida. Saldo insuficiente";
                    return BadRequest(JsonResult);
                }


                if (operacao.IdContaDestino == 0)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);

                }


                var contaDestino = _contaDigitalService.BuscarContaPorUsuario(operacao.IdContaDestino);

                if (contaDestino == null)
                {
                    JsonResult.Status = false;
                    JsonResult.Message = "404 Not Found";
                    return BadRequest(JsonResult);

                }

                bool ret = _contaDigitalService.Transferir(contaOrigem, contaDestino, operacao.ValorOperacao);

                JsonResult.Status = true;
                JsonResult.Message = "Transferência efetuada com sucesso";
                JsonResult.Object = null;
                return Ok(JsonResult);

            }
            catch (Exception ex)
            {
                JsonResult.Status = false;
                JsonResult.Message = "404 Not Found";
                return BadRequest(JsonResult);

            }



        }


    }
}
