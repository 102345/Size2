using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Size.ContaDigital.Model
{
    public class Conta
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConta { get; set; }

        [ForeignKey("User"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUsuario { get; set; }

        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public string TipoConta { get; set; }
        public string NroDocumento { get; set; }
        public decimal Saldo { get; set; }
       // public virtual  User User { get; set; }
        //public virtual ICollection<MovimentoConta> MovimentosConta { get; set; }
    }
}
