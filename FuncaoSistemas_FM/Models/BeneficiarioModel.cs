using System.ComponentModel.DataAnnotations;

namespace FuncaoSistemas_FM.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Digite um nome completo de beneficiário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite um CPF válido")]
        public string CPF { get; set; }

        public long ClienteModelID { get; set; }       
    }
}