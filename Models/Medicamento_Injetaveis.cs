using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Trabalho_Gustavo_Karoline.Models
{
    public class Medicamento_Injetaveis
    {
        [Key]
        public int codigo { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo 'Nome' é obrigatório.")]
        [StringLength(35)]
        public string nome { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Campo 'Unidade' é obrigatório.")]
        [StringLength(5)]
        public string unidade { get; set; }

        [Display(Name = "Quantidade de Estoque")]
        [Required(ErrorMessage = "Campo 'Quantidade de Estoque' é obrigatório.")]
        public int Qtde_Estoque { get; set; }

        public virtual void Atualizacao(int qtde, int id)
        {
            if(id == codigo)
            {
                Qtde_Estoque -= qtde;
            }
        }
    }
}
