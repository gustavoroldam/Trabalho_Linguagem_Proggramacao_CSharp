using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trabalho_Gustavo_Karoline.Models
{
    [Table("Paciente")]
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Campo 'CPF' é obrigatório.")]
        public int cpf { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo 'Nome' é obrigatório.")]
        [StringLength(35)]
        public string nome { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Campo 'Telefone' é obrigatório.")]
        public int telefone { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "Campo 'Endereço' é obrigatório.")]
        [StringLength(50)]
        public string endereco { get; set; }
    }
}
