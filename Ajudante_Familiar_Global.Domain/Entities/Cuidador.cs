using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Ajudante_Familiar_Global.Domain.Entities
{
    [Table("T_Ajudante_familiar_Cuidador")]
    public class Cuidador
    {
        [Key]
        [Column("id_cuidador")]
        public int Id_cuidador { get; set; }
        [Required]
        [Column("nm_cuidador")]
        public string Nm_cuidador { get; set; }
        [Column("cpf_cuidador")]
        public string Cpf_cuidador { get; set; }
        [Column("ds_email")]
        public string Ds_email { get; set; }
        [Column("ds_senha")]
        public string Ds_senha { get; set; }
        [Required]
        [Column("id_perfil")]
        public int Id_perfil { get; set; }
        [JsonIgnore]
        [ForeignKey("Id_perfil")]
        public Perfil Perfil { get; set; }

        public Cuidador()
        {

        }

        public Cuidador(int id_cuidador, string nm_cuidador, string cpf_cuidador, string ds_email, string ds_senha, int id_perfil)
        {
            Id_cuidador = id_cuidador;
            Nm_cuidador = nm_cuidador;
            Cpf_cuidador = cpf_cuidador;
            Ds_email = ds_email;
            Ds_senha = ds_senha;
            Id_perfil = id_perfil;
        }
    }
}
