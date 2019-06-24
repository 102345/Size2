namespace Size.ContaDigital.Presentation.ViewModels
{
    public class ContaViewModel
    {
        
        public int IdConta { get; set; }
        public int IdUsuario { get; set; }
        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public string TipoConta { get; set; }
        public string NroDocumento { get; set; }
        public decimal Saldo { get; set; }
        public string Valor { get; set; }
    }
}
