using Size.ContaDigital.Model;
using System;
using System.Collections.Generic;

namespace Size.ContaDigital.Presentation.ViewModels
{
    public class MovimentoContaViewModel
    {
        
        public int IdMovimento { get; set; }
        public int IdUsuario { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; }
        public DateTime DataMovimento { get; set; }
        public IEnumerable<MovimentoConta> ColecaoMovimento { get; set; }
    }
}
