using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TesteCapgeminiMvc.Models
{
    public class Excel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Data de entrega não pode ser menor que a data atual")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime? DtEntrega { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(50,ErrorMessage = "Nome do produto não pode ter mais que 50 caracteres")]
        public string NomeProduto { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(1,ErrorMessage = "Quantidade tem que ser maior que zero")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(1,ErrorMessage = "Valor Unitário tem que ser maior que zero")]
        public decimal ValorUnitario { get; set; }
    }
}
