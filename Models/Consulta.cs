using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Trabalho_Gustavo_Karoline.Models
{
    public class Consulta
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Paciente")]
        public int PacienteID { get; set; }
        [ForeignKey("PacienteID")]
        public Paciente paciente { get; set; }

        [Display(Name = "Medico")]
        public int MadicoId { get; set; }
        [ForeignKey("MadicoId")]
        public Medico medico { get; set; }

        [Display(Name = "Medicamento")]
        public int MedicamentoId { get; set; }
        [ForeignKey("MedicamentoId")]
        public Medicamento_Injetaveis Medicamento_Injetaveis { get; set; }

        [Display(Name = "Quantidade usada")]
        [Required(ErrorMessage = "Campo 'Quantidade usada' é obrigatório.")]
        public int Qtde_Vacina { get; set; }
    }
}
