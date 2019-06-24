using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Size.ContaDigital.Model
{
    public class MovimentoConta
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMovimento { get; set; }

        [ForeignKey("User"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUsuario { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; }
        public DateTime DataMovimento { get; set; }

    }
}
