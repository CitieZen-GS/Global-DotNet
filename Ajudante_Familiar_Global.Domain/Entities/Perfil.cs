using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Ajudante_Familiar_Global.Domain.Entities
{
    [Table("T_Ajudante_familiar_Perfil")]
    public class Perfil
    {
        
        [Key]
        [Column("id_perfil")]
        public int id_perfil { get; set; }
        [Required]
        [Column("nm_perfil")]
        public string nm_perfil { get; set; }
        [Required]
        [Column("id_cargo")]
        public int id_cargo { get; set; }
        [JsonIgnore]
        [ForeignKey("id_cargo")]
        public Cargo? cargo { get; set; }

        public Perfil() { }

        public Perfil(int id_perfil, string nm_perfil, int id_cargo)
        {
            this.id_perfil = id_perfil;
            this.nm_perfil = nm_perfil;
            this.id_cargo = id_cargo;
        }



    }
}
