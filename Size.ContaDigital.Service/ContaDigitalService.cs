using Size.ContaDigital.DAL.Repositories;
using Size.ContaDigital.Model;
using Size.ContaDigital.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Size.ContaDigital.Service
{
    public class ContaDigitalService : IContaDigitalService
    {
        private readonly ContaRepository _contaRepository;
        private readonly MovimentoContaRepository _movimentoContaRepository;

        enum eTipoMovimento
        {
            Credito ,
            Debito 

        }

        public ContaDigitalService()
        {
            _contaRepository = new ContaRepository();
            _movimentoContaRepository = new MovimentoContaRepository();
        }

        public Conta BuscarConta(int idConta)
        {
            return _contaRepository.GetById(idConta);
        }

        public Conta BuscarContaPorUsuario(int idUser)
        {
            List<Conta> contas = _contaRepository.GetAll().ToList();

            return contas.Find(a => a.IdUsuario == idUser);
        }

        public bool Depositar(Conta conta, decimal valor)
        {
            bool operacao = false;

            try
            {
                var contaDestino = BuscarConta(conta.IdConta);

                contaDestino.IdConta = conta.IdConta;
                contaDestino.IdUsuario = conta.IdUsuario;
                contaDestino.TipoConta = conta.TipoConta;
                contaDestino.NroDocumento = conta.NroDocumento;
                contaDestino.Agencia = conta.Agencia;
                contaDestino.ContaCorrente = contaDestino.ContaCorrente;
                contaDestino.Saldo = contaDestino.Saldo + valor;

                _contaRepository.UpdateAsync(contaDestino);

                RegistrarMovimento(conta, valor, eTipoMovimento.Credito);

                operacao = true;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                operacao = false;
                throw;
            }

            return operacao;
            

        }

     

        private void RegistrarMovimento(Conta conta, decimal valor , eTipoMovimento eTipoMovimento)
        {
            var movimento = new MovimentoConta();

            string descTipoMovimento = "C";

            if (eTipoMovimento == eTipoMovimento.Debito) descTipoMovimento = "D";

            movimento.IdUsuario = conta.IdUsuario;
            movimento.TipoMovimento = descTipoMovimento;
            movimento.Valor = valor;
            movimento.DataMovimento = DateTime.Now;

            _movimentoContaRepository.Add(movimento);
        }

        public IEnumerable<MovimentoConta> ListarExtrato(int idUsuario)
        {
            return _movimentoContaRepository.GetAll().Where(x => x.IdUsuario == idUsuario).OrderByDescending( x => x.DataMovimento);
        }

        public bool Sacar(Conta conta, decimal valor)
        {
            bool operacao = false;

            try
            {
                var contaDestino = BuscarConta(conta.IdConta);

                contaDestino.IdConta = conta.IdConta;
                contaDestino.IdUsuario = conta.IdUsuario;
                contaDestino.TipoConta = conta.TipoConta;
                contaDestino.NroDocumento = conta.NroDocumento;
                contaDestino.Agencia = conta.Agencia;
                contaDestino.ContaCorrente = contaDestino.ContaCorrente;
                contaDestino.Saldo = contaDestino.Saldo - valor;

                _contaRepository.UpdateAsync(contaDestino);

                RegistrarMovimento(conta, valor, eTipoMovimento.Debito);

                operacao = true;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                operacao = false;
                throw;
            }

            return operacao;
        }

        public bool Transferir(Conta contaOrigem, Conta ContaDestino, decimal valor)
        {
            bool retDeposita = false;
            bool retSaca = this.Sacar(contaOrigem, valor);

            if(retSaca)
                retDeposita = this.Depositar(ContaDestino, valor);

            return retDeposita;
        }

        public Conta BuscarContaPorUsuario(string codDocumento)
        {
            List<Conta> contas = _contaRepository.GetAll().ToList();

            return contas.Find(a => a.NroDocumento.Replace(".","").Replace("-","").Replace("/","") == codDocumento.Replace(".","").Replace("-","").Replace("/",""));
        }

        public Conta BuscarConta(string agencia, string contacorrente)
        {
            List<Conta> contas = _contaRepository.GetAll().ToList();

            return contas.Find(a => a.Agencia.Replace(".", "").Replace("-", "") == agencia.Replace(".", "").Replace("-", "") &&
                                a.ContaCorrente.Replace(".", "").Replace("-", "") == contacorrente.Replace(".", "").Replace("-", ""));
        }
    }
}
