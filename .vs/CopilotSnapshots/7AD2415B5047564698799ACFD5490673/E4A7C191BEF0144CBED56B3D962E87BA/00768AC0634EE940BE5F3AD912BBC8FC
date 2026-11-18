using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Ajudante_Familiar_Global.Domain.Entities
{
    [Table("T_Ajudante_familiar_Consulta_Medicamento")]
    public class Consulta_Medicamento
    {
        

        [Key]
        [Column("id_consulta_medicamento")]
        public int id_consulta_medicamento { get; set; }
        
        [Required]
        [Column("id_lembrete_consulta")]
        public int id_lembrete_consulta { get; set; }
        [JsonIgnore]
        [ForeignKey("id_lembrete_consulta")]
        public Lembrete_Consulta lembrete_Consulta { get; set; }

        [Required]
        [Column("id_Medicamentos")]
        public int id_Medicamentos { get; set; }
        [JsonIgnore]
        [ForeignKey("id_Medicamentos")]
        public Medicamentos medicamentos { get; set; }

        public Consulta_Medicamento() { }

        public Consulta_Medicamento(int id_consulta_medicamento, int id_lembrete_consulta, Lembrete_Consulta lembrete_Consulta, int id_Medicamentos, Medicamentos medicamentos)
        {
            this.id_consulta_medicamento = id_consulta_medicamento;
            this.id_lembrete_consulta = id_lembrete_consulta;
            this.lembrete_Consulta = lembrete_Consulta;
            this.id_Medicamentos = id_Medicamentos;
            this.medicamentos = medicamentos;
        }
    }
}
