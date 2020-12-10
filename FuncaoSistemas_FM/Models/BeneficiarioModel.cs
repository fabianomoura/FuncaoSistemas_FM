using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FuncaoSistemas_FM.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Digite um nome completo de beneficiário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite um CPF válido")]
        public string CPF { get; set; }

        /*[Required]
        [ForeignKey("ClienteModel")]*/
        public long ClienteModelID { get; set; }

        /*public virtual ClienteModel Cliente { get; set; }*/
    }
}