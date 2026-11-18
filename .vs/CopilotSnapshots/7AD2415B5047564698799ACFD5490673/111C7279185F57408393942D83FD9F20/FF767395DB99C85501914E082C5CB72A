using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Ajudante_Familiar_Global.Domain.Entities
{
    [Table("T_Ajudante_familiar_Parente")]
    public class Parente
    {

        [Key]
        [Column("id_perfil")]
        public int Id_parente { get; set; }
        [Column("nm_parente")]
        public string Nm_parente { get; set; }
        [Column("nr_telefone")]
        public string Nr_telefone { get; set; }
        [Column("ds_email")]
        public string Ds_email { get; set; }
        [Column("ds_senha")]
        public string Ds_senha { get; set; }
        [Required]
        [Column("id_idoso")]
        public int Id_idoso { get; set; }
        [JsonIgnore]
        [ForeignKey("Id_idoso")]
        public Idoso? Idoso { get; set; }
        [Required]
        [Column("id_perfil")]
        public int Id_perfil { get; set; }
        [JsonIgnore]
        [ForeignKey("Id_perfil")]
        public Perfil? Perfil { get; set; }

        public Parente() { }

        public Parente(int id_parente, string nm_parente, string nr_telefone, string ds_email, string ds_senha, int id_idoso, Idoso idoso, int id_perfil, Perfil perfil)
        {
            Id_parente = id_parente;
            Nm_parente = nm_parente;
            Nr_telefone = nr_telefone;
            Ds_email = ds_email;
            Ds_senha = ds_senha;
            Id_idoso = id_idoso;
            Idoso = idoso;
            Id_perfil = id_perfil;
            Perfil = perfil;
        }
    }
}
