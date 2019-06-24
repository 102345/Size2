using Size.ContaDigital.Model;
using System.Collections.Generic;

namespace Size.ContaDigital.Service.Interface
{
    public interface IContaDigitalService
    {
        bool Depositar(Conta conta, decimal valor);
        bool Sacar(Conta conta, decimal valor);
        Conta BuscarConta(int idConta);
        Conta BuscarConta(string agencia, string contacorrente);
        Conta BuscarContaPorUsuario(int idUser);
        Conta BuscarContaPorUsuario(string codDocumento);
        bool Transferir(Conta contaOrigem, Conta ContaDestino, decimal valor);
        IEnumerable<MovimentoConta> ListarExtrato(int idUsuario);

    }
}
