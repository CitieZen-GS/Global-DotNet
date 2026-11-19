
using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ajudante_Familiar_Global.Infrastruture.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Alimentacao> Alimentacoes { get; set; } = null!;
        public DbSet<Atividades> Atividades { get; set; } = null!;
        public DbSet<Perfil> Perfis { get; set; } = null!;
        public DbSet<Idoso> Idosos { get; set; } = null!;
        public DbSet<Lembrete_Consulta> LembretesConsulta { get; set; } = null!;
        public DbSet<Medicamentos> Medicamentos { get; set; } = null!;
        public DbSet<Cargo> Cargos { get; set; } = null!;
        public DbSet<Consulta_Medicamento> ConsultasMedicamento { get; set; } = null!;
        public DbSet<Parente> Parentes { get; set; } = null!;
        public DbSet<Cuidador> Cuidadores { get; set; } = null!;
        public DbSet<Doencas> Doencas { get; set; } = null!;

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyParaDateConverter>()
                .HaveColumnType("DATE");

            configurationBuilder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyParaDateConverter>()
                .HaveColumnType("DATE");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>().ToTable("T_AJUDANTE_FAMILIAR_CARGO");
            modelBuilder.Entity<Cargo>().HasKey(g => g.Id_cargo);
            modelBuilder.Entity<Cargo>().Property(g => g.Id_cargo).HasColumnName("ID_CARGO").ValueGeneratedOnAdd();
            modelBuilder.Entity<Cargo>().Property(g => g.Nm_cargo).HasColumnName("NM_CARGO").HasMaxLength(20);
            modelBuilder.Entity<Cargo>().Property(g => g.Fg_permissao).HasColumnName("FG_PERMISSAO")
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<Permissao>(v))
                .HasMaxLength(1);

            modelBuilder.Entity<Perfil>().ToTable("T_AJUDANTE_FAMILIAR_PERFIL");
            modelBuilder.Entity<Perfil>().HasKey(g => g.id_perfil);
            modelBuilder.Entity<Perfil>().Property(g => g.id_perfil).HasColumnName("ID_PERFIL").ValueGeneratedOnAdd();
            modelBuilder.Entity<Perfil>().Property(g => g.nm_perfil).HasColumnName("NM_PERFIL").HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Perfil>().Property(g => g.id_cargo).HasColumnName("ID_CARGO").IsRequired();
            modelBuilder.Entity<Perfil>().HasOne(g => g.cargo).WithMany().HasForeignKey(g => g.id_cargo).IsRequired();

            modelBuilder.Entity<Cuidador>().ToTable("T_AJUDANTE_FAMILIAR_CUIDADOR");
            modelBuilder.Entity<Cuidador>().HasKey(g => g.Id_cuidador);
            modelBuilder.Entity<Cuidador>().Property(g => g.Id_cuidador).HasColumnName("ID_CUIDADOR").ValueGeneratedOnAdd();
            modelBuilder.Entity<Cuidador>().Property(g => g.Nm_cuidador).HasColumnName("NM_CUIDADOR").HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Cuidador>().Property(g => g.Cpf_cuidador).HasColumnName("CPF_CUIDADOR").HasMaxLength(14);
            modelBuilder.Entity<Cuidador>().HasIndex(g => g.Cpf_cuidador).HasDatabaseName("UK_CFP_CUIDADOR").IsUnique();
            modelBuilder.Entity<Cuidador>().Property(g => g.Ds_email).HasColumnName("DS_EMAIL").HasMaxLength(20);
            modelBuilder.Entity<Cuidador>().HasIndex(g => g.Ds_email).HasDatabaseName("UK_DS_CUIDADOR").IsUnique();
            modelBuilder.Entity<Cuidador>().Property(g => g.Ds_senha).HasColumnName("DS_SENHA").HasMaxLength(20);
            modelBuilder.Entity<Cuidador>().Property(g => g.Id_perfil).HasColumnName("ID_PERFIL").IsRequired();
            modelBuilder.Entity<Cuidador>().HasOne(g => g.Perfil).WithMany().HasForeignKey(g => g.Id_perfil).IsRequired();

            modelBuilder.Entity<Idoso>().ToTable("T_AJUDANTE_FAMILIAR_IDOSO");
            modelBuilder.Entity<Idoso>().HasKey(g => g.ID_idoso);
            modelBuilder.Entity<Idoso>().Property(g => g.ID_idoso).HasColumnName("ID_IDOSO").ValueGeneratedOnAdd();
            modelBuilder.Entity<Idoso>().Property(g => g.Nm_idoso).HasColumnName("NM_IDOSO").HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Idoso>().Property(g => g.cpf_idoso).HasColumnName("CPF_IDOSO").HasMaxLength(14);
            modelBuilder.Entity<Idoso>().Property(g => g.vlr_altura).HasColumnName("VLR_ALTURA").HasPrecision(3, 2);
            modelBuilder.Entity<Idoso>().Property(g => g.vlr_peso).HasColumnName("VLR_PESO").HasPrecision(5, 2);
            modelBuilder.Entity<Idoso>().Property(g => g.dt_nascimento).HasColumnName("DT_NASCIMENTO").HasColumnType("DATE").IsRequired();
            modelBuilder.Entity<Idoso>().Property(g => g.ds_comportamento).HasColumnName("DS_COMPORTAMENTO").HasMaxLength(100);
            modelBuilder.Entity<Idoso>().Property(g => g.id_cuidador).HasColumnName("ID_CUIDADOR").IsRequired();
            modelBuilder.Entity<Idoso>().HasOne(g => g.cuidador).WithMany().HasForeignKey(g => g.id_cuidador).IsRequired();

            modelBuilder.Entity<Parente>().ToTable("T_AJUDANTE_FAMILIAR_PARENTE");
            modelBuilder.Entity<Parente>().HasKey(g => g.Id_parente);
            modelBuilder.Entity<Parente>().Property(g => g.Id_parente).HasColumnName("ID_PARENTE").ValueGeneratedOnAdd();
            modelBuilder.Entity<Parente>().Property(g => g.Nm_parente).HasColumnName("NM_PARENTE").HasMaxLength(20);
            modelBuilder.Entity<Parente>().Property(g => g.Nr_telefone).HasColumnName("NR_TELEFONE").HasMaxLength(18);
            modelBuilder.Entity<Parente>().HasIndex(g => g.Nr_telefone).HasDatabaseName("UK_NR_TELEFONE_PARENTE").IsUnique();
            modelBuilder.Entity<Parente>().Property(g => g.Ds_email).HasColumnName("DS_EMAIL").HasMaxLength(20);
            modelBuilder.Entity<Parente>().HasIndex(g => g.Ds_email).HasDatabaseName("UK_DS_EMAIL_PARENTE").IsUnique();
            modelBuilder.Entity<Parente>().Property(g => g.Ds_senha).HasColumnName("DS_SENHA").HasMaxLength(20);
            modelBuilder.Entity<Parente>().Property(g => g.Id_idoso).HasColumnName("ID_IDOSO").IsRequired();
            modelBuilder.Entity<Parente>().HasOne(g => g.Idoso).WithMany().HasForeignKey(g => g.Id_idoso).IsRequired();
            modelBuilder.Entity<Parente>().Property(g => g.Id_perfil).HasColumnName("ID_PERFIL").IsRequired();
            modelBuilder.Entity<Parente>().HasOne(g => g.Perfil).WithMany().HasForeignKey(g => g.Id_perfil).IsRequired();

            modelBuilder.Entity<Doencas>().ToTable("T_AJUDANTE_FAMILIAR_DOENCAS");
            modelBuilder.Entity<Doencas>().HasKey(g => g.id_doenca);
            modelBuilder.Entity<Doencas>().Property(g => g.id_doenca).HasColumnName("ID_DOENCA").ValueGeneratedOnAdd();
            modelBuilder.Entity<Doencas>().Property(g => g.Nm_doenca).HasColumnName("NM_DOENCA").HasMaxLength(20);
            modelBuilder.Entity<Doencas>().Property(g => g.Ds_doenca).HasColumnName("DS_DOENCA").HasMaxLength(20);
            modelBuilder.Entity<Doencas>().Property(g => g.Id_idoso).HasColumnName("ID_IDOSO").IsRequired();
            modelBuilder.Entity<Doencas>().HasOne(g => g.Idoso).WithMany().HasForeignKey(g => g.Id_idoso).IsRequired();

            modelBuilder.Entity<Atividades>().ToTable("T_AJUDANTE_FAMILIAR_ATIVIDADES");
            modelBuilder.Entity<Atividades>().HasKey(g => new { g.Id_atividade, g.id_idoso });
            modelBuilder.Entity<Atividades>().Property(g => g.Id_atividade).HasColumnName("ID_ATIVIDADE").ValueGeneratedOnAdd();
            modelBuilder.Entity<Atividades>().Property(g => g.Nm_atividade).HasColumnName("NM_ATIVIDADE").HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Atividades>().Property(g => g.Ds_atividade).HasColumnName("DS_ATIVIDADE").HasMaxLength(80);
            modelBuilder.Entity<Atividades>().Property(g => g.Dt_atividade).HasColumnName("DT_ATIVIDADE").HasColumnType("DATE").IsRequired();
            modelBuilder.Entity<Atividades>().Property(g => g.Hr_atividade_inicio).HasColumnName("HR_ATIVIDADE_INICIO").HasColumnType("DATE");
            modelBuilder.Entity<Atividades>().Property(g => g.Hr_atividade_fim).HasColumnName("HR_ATIVIDADE_FIM").HasColumnType("DATE");
            modelBuilder.Entity<Atividades>().Property(g => g.id_idoso).HasColumnName("ID_IDOSO").IsRequired();
            modelBuilder.Entity<Atividades>().HasOne(g => g.idoso).WithMany().HasForeignKey(g => g.id_idoso).IsRequired();

            modelBuilder.Entity<Alimentacao>().ToTable("T_AJUDANTE_FAMILIAR_ALIMENTACAO");
            modelBuilder.Entity<Alimentacao>().HasKey(g => g.Id_alimentacao);
            modelBuilder.Entity<Alimentacao>().Property(g => g.Id_alimentacao).HasColumnName("ID_ALIMENTACAO").ValueGeneratedOnAdd();
            modelBuilder.Entity<Alimentacao>().Property(g => g.Nm_alimentacao).HasColumnName("NM_ALIMENTACAO").HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Alimentacao>().Property(g => g.Tp_alimentacao).HasColumnName("TP_ALIMENTACAO").HasMaxLength(20).IsRequired().HasConversion(
                v => v == TipoAlimentacao.cafe_da_manha ? "Cafe da manha"
                   : v == TipoAlimentacao.almoco ? "Almoco"
                   : v == TipoAlimentacao.lache_da_tarde ? "Lanche de tarde"
                   : "Jantar",
                v => v.Trim() == "Cafe da manha" ? TipoAlimentacao.cafe_da_manha
                   : v.Trim() == "Almoco" ? TipoAlimentacao.almoco
                   : v.Trim() == "Lanche de tarde" ? TipoAlimentacao.lache_da_tarde
                   : TipoAlimentacao.jantar
            );

            modelBuilder.Entity<Alimentacao>().Property(g => g.Dt_consumo).HasColumnName("DT_CONSUMO").HasColumnType("DATE").IsRequired();
            modelBuilder.Entity<Alimentacao>().Property(g => g.Hr_consumo).HasColumnName("HR_CONSUMO").HasColumnType("DATE").IsRequired();
            modelBuilder.Entity<Alimentacao>().Property(g => g.Vlr_caloria).HasColumnName("VLR_CALORIA").HasPrecision(6, 2);
            modelBuilder.Entity<Alimentacao>().Property(g => g.Ds_preparacao).HasColumnName("DS_PREPARACAO").HasMaxLength(100);
            modelBuilder.Entity<Alimentacao>().Property(g => g.Id_idoso).HasColumnName("ID_IDOSO").IsRequired();
            modelBuilder.Entity<Alimentacao>().HasOne(g => g.Idoso).WithMany().HasForeignKey(g => g.Id_idoso).IsRequired();

            modelBuilder.Entity<Lembrete_Consulta>().ToTable("T_AJUDANTE_FAMILIAR_LEMBRETE_CONSULTA");
            modelBuilder.Entity<Lembrete_Consulta>().HasKey(g => g.Id_lembrete_consulta);
            modelBuilder.Entity<Lembrete_Consulta>().Property(g => g.Id_lembrete_consulta).HasColumnName("ID_LEMBRETE_CONSULTA").ValueGeneratedOnAdd();
            modelBuilder.Entity<Lembrete_Consulta>().Property(g => g.Ds_titulo_lembrente_consulta).HasColumnName("DS_TITULO_LEMBRENTE_CONSULTA").HasMaxLength(20);
            modelBuilder.Entity<Lembrete_Consulta>().Property(g => g.Nm_consulta_medico).HasColumnName("NM_CONSULTA_MEDICO").HasMaxLength(20);
            modelBuilder.Entity<Lembrete_Consulta>().Property(g => g.Dt_consulta).HasColumnName("DT_CONSULTA").HasColumnType("DATE");
            modelBuilder.Entity<Lembrete_Consulta>().Property(g => g.Hr_consulta).HasColumnName("HR_CONSULTA").HasColumnType("DATE");
            modelBuilder.Entity<Lembrete_Consulta>().Property(g => g.Ds_consulta).HasColumnName("DS_CONSULTA").HasMaxLength(100);
            modelBuilder.Entity<Lembrete_Consulta>().Property(g => g.St_consulta).HasColumnName("ST_CONSULTA").HasConversion<string>().HasMaxLength(10).IsRequired();
            modelBuilder.Entity<Lembrete_Consulta>().Property(g => g.Id_idoso).HasColumnName("ID_IDOSO").IsRequired();
            modelBuilder.Entity<Lembrete_Consulta>().HasOne(g => g.Idoso).WithMany().HasForeignKey(g => g.Id_idoso).IsRequired();

            modelBuilder.Entity<Medicamentos>().ToTable("T_AJUDANTE_FAMILIAR_MEDICAMENTOS");
            modelBuilder.Entity<Medicamentos>().HasKey(g => g.id_medicamentos);
            modelBuilder.Entity<Medicamentos>().Property(g => g.id_medicamentos).HasColumnName("ID_MEDICAMENTOS").ValueGeneratedOnAdd();
            modelBuilder.Entity<Medicamentos>().Property(g => g.Nm_medicamentos).HasColumnName("NM_MEDICAMENTOS").HasMaxLength(20);
            modelBuilder.Entity<Medicamentos>().Property(g => g.Via_administracao).HasColumnName("VIA_ADMINISTRACAO").HasMaxLength(20);
            modelBuilder.Entity<Medicamentos>().Property(g => g.Vl_frquencia).HasColumnName("VL_FRQUENCIA").HasMaxLength(50);
            modelBuilder.Entity<Medicamentos>().Property(g => g.Ds_instrucao_uso).HasColumnName("DS_INSTRUCAO_USO").HasMaxLength(100);
            modelBuilder.Entity<Medicamentos>().Property(g => g.Dt_medicamento).HasColumnName("DT_MEDICAMENTO").HasColumnType("DATE");
            modelBuilder.Entity<Medicamentos>().Property(g => g.Hr_medicamento).HasColumnName("HR_MEDICAMENTO").HasColumnType("DATE").IsRequired();
            modelBuilder.Entity<Medicamentos>().Property(g => g.Id_idoso).HasColumnName("ID_IDOSO").IsRequired();
            modelBuilder.Entity<Medicamentos>().HasOne(g => g.Idoso).WithMany().HasForeignKey(g => g.Id_idoso).IsRequired();

            modelBuilder.Entity<Consulta_Medicamento>().ToTable("T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO");
            modelBuilder.Entity<Consulta_Medicamento>().HasKey(g => g.id_consulta_medicamento);
            modelBuilder.Entity<Consulta_Medicamento>().Property(g => g.id_consulta_medicamento).HasColumnName("ID_CONSULTA_MEDICAMENTO").ValueGeneratedOnAdd();
            modelBuilder.Entity<Consulta_Medicamento>().Property(g => g.id_lembrete_consulta).HasColumnName("ID_LEMBRETE_CONSULTA").IsRequired();
            modelBuilder.Entity<Consulta_Medicamento>().HasOne(g => g.lembrete_Consulta).WithMany().HasForeignKey(g => g.id_lembrete_consulta).IsRequired();
            modelBuilder.Entity<Consulta_Medicamento>().Property(g => g.id_Medicamentos).HasColumnName("ID_MEDICAMENTOS").IsRequired();
            modelBuilder.Entity<Consulta_Medicamento>().HasOne(g => g.medicamentos).WithMany().HasForeignKey(g => g.id_Medicamentos).IsRequired();
        }

        public class DateOnlyParaDateConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyParaDateConverter() : base(
                date => date.ToDateTime(TimeOnly.MinValue),
                dateTime => DateOnly.FromDateTime(dateTime))
            {
            }
        }

        public class TimeOnlyParaDateConverter : ValueConverter<TimeOnly, DateTime>
        {
            public TimeOnlyParaDateConverter() : base(
                time => new DateTime(1900, 1, 1, time.Hour, time.Minute, time.Second),
                dateTime => TimeOnly.FromDateTime(dateTime))
            {
            }
        }
    }
}
