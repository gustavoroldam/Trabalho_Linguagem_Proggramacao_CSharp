using System.ComponentModel.DataAnnotations;

namespace Trabalho_Gustavo_Karoline.Models
{
    public class Medico
    {
        [Key]
        [Display(Name = "CRM")]
        [Required(ErrorMessage = "Campo 'CRM' é obrigatório.")]
        public int crm { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo 'Nome' é obrigatório.")]
        [StringLength(35)]
        public string nome { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Campo 'Telefone' é obrigatório.")]
        public int telefone { get; set; }

        [Display(Name = "Especilidade")]
        [Required(ErrorMessage = "Campo 'Especialidade' é obrigatório.")]
        [StringLength(15)]
        public string especialidade { get; set; }
    }
}
