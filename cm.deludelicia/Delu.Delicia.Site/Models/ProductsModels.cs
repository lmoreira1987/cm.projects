using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Delu.Delicia.Site.Models
{
    public class ProductsModels
    {
        public int Id { get; set; }

        public int IdCategory { get; set; }

        public string Category { get; set; }

        public List<ProductsModels> Categories()
        {
            return new List<ProductsModels>
            {
                new ProductsModels { IdCategory = 1, Category = "Bolos para festas"},
                new ProductsModels { IdCategory = 2, Category = "Bolos tradicionais"},
                new ProductsModels { IdCategory = 3, Category = "Tortas doces e salgadas"},
                new ProductsModels { IdCategory = 4, Category = "Cupcakes"},
                new ProductsModels { IdCategory = 5, Category = "Trufas diversas"},
                new ProductsModels { IdCategory = 6, Category = "Outros produtos"}
            };
        }


        [Required(ErrorMessage = "Campo Imagem é obrigatório")]
        public string Img { get; set; }

        [Required(ErrorMessage = "Campo Título é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Título deve conter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Campo Descrição é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Descrição deve conter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Description { get; set; }
    }
}