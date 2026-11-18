using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;


namespace Ajudante_Familiar_Global.Domain.Entities
{
    [Table("T_Ajudante_familiar_Alimentacao")]
    public class Atividades
    {
        [Key]
        [Column("id_atividade")]
        public int Id_atividade { get; set; }
        [Required]
        [Column("Nm_atividade")]
        public string Nm_atividade { get; set; }
        [Column("Ds_atividade")]
        public string Ds_atividade { get; set; }
        [Required]
        [Column("Dt_atividade")]
        public DateOnly Dt_atividade { get; set; }
        [Column("Hr_atividade_inicio")]
        public TimeOnly Hr_atividade_inicio { get; set; }
        [Column("Hr_atividade_fim")]
        public TimeOnly Hr_atividade_fim { get; set; }
        [Required]
        [Column("id_idoso")]
        public int id_idoso { get; set; }
        [JsonIgnore]
        [ForeignKey("id_idoso")]
        public Idoso idoso { get; set; }

        public Atividades() { }

        public Atividades(int id_atividade, string nm_atividade, string ds_atividade, DateOnly dt_atividade, TimeOnly hr_atividade_inicio, TimeOnly hr_atividade_fim, int id_idoso)
        {
            Id_atividade = id_atividade;
            Nm_atividade = nm_atividade;
            Ds_atividade = ds_atividade;
            Dt_atividade = dt_atividade;
            Hr_atividade_inicio = hr_atividade_inicio;
            Hr_atividade_fim = hr_atividade_fim;
            this.id_idoso = id_idoso;
        }
    }
}
