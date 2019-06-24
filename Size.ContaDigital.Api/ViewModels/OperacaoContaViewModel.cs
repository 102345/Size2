using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Size.ContaDigital.Api.ViewModels
{
    public class OperacaoContaViewModel
    {
        public int IdContaOrigem { get; set; }
        public int IdContaDestino { get; set; }
        public decimal ValorOperacao { get; set; }

    }
}
