using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FuncaoSistemas_FM.Models
{
    public class ClienteModel
    {
        public ClienteModel()
        {
            this.Beneficiarios = new List<BeneficiarioModel>();
        }
        public long Id { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        [Required(ErrorMessage = "Digite um CEP válido")]
        public string CEP { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [Required(ErrorMessage = "Digite uma cidade válida")]
        public string Cidade { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required(ErrorMessage = "Escolha um Estado")]
        [MaxLength(2)]
        public string Estado { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        [Required(ErrorMessage = "Digite um logradouro")]
        public string Logradouro { get; set; }

        /// <summary>
        /// Nacionalidade
        /// </summary>
        [Required(ErrorMessage = "Digite uma nacionalidade")]
        public string Nacionalidade { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required(ErrorMessage = "Digite um nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome
        /// </summary>
        [Required(ErrorMessage = "Digite um sobrenome")]
        public string Sobrenome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required(ErrorMessage = "Digite um CPF válido")]
        public string CPF { get; set; }

        public ICollection<BeneficiarioModel> Beneficiarios { get; set; }
    }
}