using Ajudante_Familiar_Global.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Ajudante_Familiar_Global.Domain.Entities
{
    [Table("T_Ajudante_familiar_Lembrete_Consulta")]
    public class Lembrete_Consulta
    {
        

        [Key]
        [Column("id_lembrete_consulta")]
        public int Id_lembrete_consulta { get; set; }
        [Column("ds_titulo_lembrente_consulta")]
        public string Ds_titulo_lembrente_consulta { get; set; }
        [Column("nm_consulta_medico")]
        public string Nm_consulta_medico { get; set; }
        [Column("dt_consulta")]
        public DateTime Dt_consulta { get; set; }
        [Column("hr_consulta")]
        public TimeOnly? Hr_consulta { get; set; }
        [Column("ds_consulta")]
        public string Ds_consulta { get; set; }
        [Required]
        [Column("st_consulta")]
        public Status St_consulta { get; set; }
        [Required]
        [Column("id_idoso")]
        public int Id_idoso { get; set; }
        [JsonIgnore]
        [ForeignKey("Id_idoso")]
        public Idoso? Idoso { get; set; }

        public Lembrete_Consulta() { }

        public Lembrete_Consulta(int id_lembrete_consulta, string ds_titulo_lembrente_consulta, string nm_consulta_medico, DateTime dt_consulta, TimeOnly? hr_consulta, string ds_consulta, Status st_consulta, int id_idoso, Idoso? idoso)
        {
            Id_lembrete_consulta = id_lembrete_consulta;
            Ds_titulo_lembrente_consulta = ds_titulo_lembrente_consulta;
            Nm_consulta_medico = nm_consulta_medico;
            Dt_consulta = dt_consulta;
            Hr_consulta = hr_consulta;
            Ds_consulta = ds_consulta;
            St_consulta = st_consulta;
            Id_idoso = id_idoso;
            Idoso = idoso;
        }
    }
}
