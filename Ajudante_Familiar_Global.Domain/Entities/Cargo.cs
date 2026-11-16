using Ajudante_Familiar_Global.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ajudante_Familiar_Global.Domain.Entities
{
    [Table("T_Ajudante_familiar_Cargo")]
    public class Cargo
    {
        [Key]
        [Column("id_cargo")]
        public int Id_cargo { get; set; }
        [Column("nm_cargo")]
        public string Nm_cargo { get; set; }
        [Column("fg_permissao")]
        public Permissao Fg_permissao { get; set; }

        public Cargo(int id_cargo, string nm_cargo, Permissao fg_permissao)
        {
            Id_cargo = id_cargo;
            Nm_cargo = nm_cargo;
            Fg_permissao = fg_permissao;
        }

    }
}
