using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Delu.Delicia.Site.Models
{
    public class ContactModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Nome deve conter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo Telefone é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo Telefone deve conter no mínimo {2} caracteres.", MinimumLength = 8)]
        public string Phone { get; set; }

        [Required(ErrorMessage ="Campo Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato do campo email não está correto")]
        [StringLength(100, ErrorMessage = "O campo Email deve conter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Mensagem é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo Mensagem deve conter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Message { get; set; }
    }
}