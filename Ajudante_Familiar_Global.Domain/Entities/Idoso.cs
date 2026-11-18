using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace Ajudante_Familiar_Global.Domain.Entities
{
    [Table("T_Ajudante_familiar_Idoso")]
    public class Idoso
    {
        
        [Key]
        [Column("id_idoso")]
        public int ID_idoso { set; get; }
        [Required]
        [Column("nm_idoso")]
        public string Nm_idoso { set; get; }
        [Column("cpf_idoso")]
        public string cpf_idoso { set; get; }
        [Column("vlr_altura")]
        public decimal vlr_altura { set; get; }
        [Column("vlr_peso")]
        public decimal vlr_peso { set; get; }
        [Column("dt_nascimento")]
        public DateOnly dt_nascimento { set; get; }
        [Column("ds_comportamento")]
        public string ds_comportamento { set; get; }
        [Required]
        [Column("id_cuidador")]
        public int id_cuidador { set; get; }
        [JsonIgnore]
        [ForeignKey("id_cuidador")]
        public Cuidador? cuidador { set; get; }

        public Idoso() { }

        public Idoso(int iD_idoso, string nm_idoso, string cpf_idoso, decimal vlr_altura, decimal vlr_peso, DateOnly dt_nascimento, string ds_comportamento, int id_cuidador, Cuidador cuidador)
        {
            ID_idoso = iD_idoso;
            Nm_idoso = nm_idoso;
            this.cpf_idoso = cpf_idoso;
            this.vlr_altura = vlr_altura;
            this.vlr_peso = vlr_peso;
            this.dt_nascimento = dt_nascimento;
            this.ds_comportamento = ds_comportamento;
            this.id_cuidador = id_cuidador;
            this.cuidador = cuidador;
        }

    }
}
