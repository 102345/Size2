using Microsoft.VisualStudio.TestTools.UnitTesting;
using Size.ContaDigital.Service;
using Size.ContaDigital.Model;
using System.Linq;

namespace Size.ContaDigital.Test
{
    [TestClass]
    public class UnitContaDigital
    {
        //[TestMethod]
        public void BuscarUsuario()
        {
            var user = new UserService().BuscarUsuario("TESTE", "TESTE");

            Assert.AreNotEqual(null, user);

        }


        //[TestMethod]
        public void BuscarContaDigital()
        {
            var conta = new ContaDigitalService().BuscarContaPorUsuario(1);

            Assert.AreNotEqual(null, conta);

        }

        [TestMethod]
        public void BuscarContaDigitalPorDocumento()
        {

            var conta = new ContaDigitalService().BuscarContaPorUsuario("11111111111");

            Assert.AreNotEqual(null, conta);
        }


        //[TestMethod]
        public void DepositarConta()
        {
            var conta = new ContaDigitalService().BuscarContaPorUsuario(1);
           

            bool ret = new ContaDigitalService().Depositar(conta, 20);

            Assert.IsTrue(ret);

        }

        //[TestMethod]
        public void SacarConta()
        {
            var conta = new ContaDigitalService().BuscarContaPorUsuario(1);

            bool ret = new ContaDigitalService().Sacar(conta, 20);

            Assert.IsTrue(ret);

        }


        //[TestMethod]
        public void TransferirConta()
        {
            var contaOrigem = new ContaDigitalService().BuscarContaPorUsuario(1);
            var contaDestino = new ContaDigitalService().BuscarContaPorUsuario(2);

            bool ret = new ContaDigitalService().Transferir(contaOrigem, contaDestino, 100);

            Assert.IsTrue(ret);

        }

       // [TestMethod]
        public void ListarExtrato()
        {

            var lista = new ContaDigitalService().ListarExtrato(1).ToList();

            Assert.IsNotNull(lista);
        }


        //[TestMethod]
        public void TesteReg()
        {
            var conta = new ContaDigitalService().BuscarContaPorUsuario(1);


            //new ContaDigitalService().TesteRegistrar(conta, 100);

            Assert.IsTrue(true);

        }



    }
}
