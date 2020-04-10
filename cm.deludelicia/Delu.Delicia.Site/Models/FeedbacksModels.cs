using System.ComponentModel.DataAnnotations;

namespace Delu.Delicia.Site.Models
{
    public class FeedbacksModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo Descrição é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Descrição deve conter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Nome deve conter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}