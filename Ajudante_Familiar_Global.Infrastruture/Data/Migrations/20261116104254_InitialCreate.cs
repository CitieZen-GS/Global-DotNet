using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ajudante_Familiar_Global.Infrastruture.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Cargo",
                columns: table => new
                {
                    id_cargo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_cargo = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    fg_permissao = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Cargo", x => x.id_cargo);
                    table.CheckConstraint("ck_cargo", "\"fg_permissao\" IN (''S'',''N'')");
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Perfil",
                columns: table => new
                {
                    id_perfil = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_perfil = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    id_cargo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Perfil", x => x.id_perfil);
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Perfil_T_Ajudante_familiar_Cargo_id_cargo",
                        column: x => x.id_cargo,
                        principalTable: "T_Ajudante_familiar_Cargo",
                        principalColumn: "id_cargo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Cuidador",
                columns: table => new
                {
                    id_cuidador = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_cuidador = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    cpf_cuidador = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    ds_email = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ds_senha = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    id_perfil = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Cuidador", x => x.id_cuidador);
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Cuidador_T_Ajudante_familiar_Perfil_id_perfil",
                        column: x => x.id_perfil,
                        principalTable: "T_Ajudante_familiar_Perfil",
                        principalColumn: "id_perfil",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Idoso",
                columns: table => new
                {
                    id_idoso = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_idoso = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    cpf_idoso = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    vlr_altura = table.Column<decimal>(type: "DECIMAL(3,2)", precision: 3, scale: 2, nullable: false),
                    vlr_peso = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    dt_nascimento = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    ds_comportamento = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    id_cuidador = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Idoso", x => x.id_idoso);
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Idoso_T_Ajudante_familiar_Cuidador_id_cuidador",
                        column: x => x.id_cuidador,
                        principalTable: "T_Ajudante_familiar_Cuidador",
                        principalColumn: "id_cuidador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Alimentacao",
                columns: table => new
                {
                    id_alimentacao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_alimentacao = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    tp_alimentacao = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    dt_consumo = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    hr_consumo = table.Column<string>(type: "NVARCHAR2(48)", nullable: false),
                    vlr_caloria = table.Column<decimal>(type: "DECIMAL(6,2)", precision: 6, scale: 2, nullable: false),
                    ds_preparacao = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    id_idoso = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Alimentacao", x => x.id_alimentacao);
                    table.CheckConstraint("ck_alimentacao_tipo", "\"tp_alimentacao\" IN (''Cafe da manha'', ''Almoco'', ''Lanche de tarde'', ''Jantar'')");
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Alimentacao_T_Ajudante_familiar_Idoso_id_idoso",
                        column: x => x.id_idoso,
                        principalTable: "T_Ajudante_familiar_Idoso",
                        principalColumn: "id_idoso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Doencas",
                columns: table => new
                {
                    id_doenca = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_doenca = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ds_doenca = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    id_idoso = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Doencas", x => x.id_doenca);
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Doencas_T_Ajudante_familiar_Idoso_id_idoso",
                        column: x => x.id_idoso,
                        principalTable: "T_Ajudante_familiar_Idoso",
                        principalColumn: "id_idoso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Lembrete_Consulta",
                columns: table => new
                {
                    id_lembrete_consulta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_titulo_lembrente_consulta = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    nm_consulta_medico = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    dt_consulta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    hr_consulta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ds_consulta = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    st_consulta = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    id_idoso = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Lembrete_Consulta", x => x.id_lembrete_consulta);
                    table.CheckConstraint("ck_consulta_status", "\"st_consulta\" IN (''Pendente'', ''Realizado'')");
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Lembrete_Consulta_T_Ajudante_familiar_Idoso_id_idoso",
                        column: x => x.id_idoso,
                        principalTable: "T_Ajudante_familiar_Idoso",
                        principalColumn: "id_idoso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Medicamentos",
                columns: table => new
                {
                    id_medicamentos = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_medicamentos = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    via_administracao = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    vl_frquencia = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    ds_instrucao_uso = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    dt_medicamento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    hr_medicamento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    id_idoso = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Medicamentos", x => x.id_medicamentos);
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Medicamentos_T_Ajudante_familiar_Idoso_id_idoso",
                        column: x => x.id_idoso,
                        principalTable: "T_Ajudante_familiar_Idoso",
                        principalColumn: "id_idoso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Parente",
                columns: table => new
                {
                    id_parente = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_parente = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    nr_telefone = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false),
                    ds_email = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ds_senha = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    id_idoso = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_perfil = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Parente", x => x.id_parente);
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Parente_T_Ajudante_familiar_Idoso_id_idoso",
                        column: x => x.id_idoso,
                        principalTable: "T_Ajudante_familiar_Idoso",
                        principalColumn: "id_idoso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Parente_T_Ajudante_familiar_Perfil_id_perfil",
                        column: x => x.id_perfil,
                        principalTable: "T_Ajudante_familiar_Perfil",
                        principalColumn: "id_perfil",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_atividades",
                columns: table => new
                {
                    id_atividade = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_idoso = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    nm_atividade = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ds_atividade = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    dt_atividade = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    hr_atividade_inicio = table.Column<string>(type: "NVARCHAR2(48)", nullable: false),
                    hr_atividade_fim = table.Column<string>(type: "NVARCHAR2(48)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_atividades", x => new { x.id_atividade, x.id_idoso });
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_atividades_T_Ajudante_familiar_Idoso_id_idoso",
                        column: x => x.id_idoso,
                        principalTable: "T_Ajudante_familiar_Idoso",
                        principalColumn: "id_idoso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Ajudante_familiar_Consulta_Medicamento",
                columns: table => new
                {
                    id_consulta_medicamento = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_lembrete_consulta = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_medicamentos = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Ajudante_familiar_Consulta_Medicamento", x => x.id_consulta_medicamento);
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Consulta_Medicamento_T_Ajudante_familiar_Lembrete_Consulta_id_lembrete_consulta",
                        column: x => x.id_lembrete_consulta,
                        principalTable: "T_Ajudante_familiar_Lembrete_Consulta",
                        principalColumn: "id_lembrete_consulta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Ajudante_familiar_Consulta_Medicamento_T_Ajudante_familiar_Medicamentos_id_medicamentos",
                        column: x => x.id_medicamentos,
                        principalTable: "T_Ajudante_familiar_Medicamentos",
                        principalColumn: "id_medicamentos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Alimentacao_id_idoso",
                table: "T_Ajudante_familiar_Alimentacao",
                column: "id_idoso");

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Consulta_Medicamento_id_lembrete_consulta",
                table: "T_Ajudante_familiar_Consulta_Medicamento",
                column: "id_lembrete_consulta");

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Consulta_Medicamento_id_medicamentos",
                table: "T_Ajudante_familiar_Consulta_Medicamento",
                column: "id_medicamentos");

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Cuidador_id_perfil",
                table: "T_Ajudante_familiar_Cuidador",
                column: "id_perfil");

            migrationBuilder.CreateIndex(
                name: "uk_cfp_cuidador",
                table: "T_Ajudante_familiar_Cuidador",
                column: "cpf_cuidador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_ds_cuidador",
                table: "T_Ajudante_familiar_Cuidador",
                column: "ds_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Doencas_id_idoso",
                table: "T_Ajudante_familiar_Doencas",
                column: "id_idoso");

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Idoso_id_cuidador",
                table: "T_Ajudante_familiar_Idoso",
                column: "id_cuidador");

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Lembrete_Consulta_id_idoso",
                table: "T_Ajudante_familiar_Lembrete_Consulta",
                column: "id_idoso");

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Medicamentos_id_idoso",
                table: "T_Ajudante_familiar_Medicamentos",
                column: "id_idoso");

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Parente_id_idoso",
                table: "T_Ajudante_familiar_Parente",
                column: "id_idoso");

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Parente_id_perfil",
                table: "T_Ajudante_familiar_Parente",
                column: "id_perfil");

            migrationBuilder.CreateIndex(
                name: "uk_ds_email_parente",
                table: "T_Ajudante_familiar_Parente",
                column: "ds_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_nr_telefone_parente",
                table: "T_Ajudante_familiar_Parente",
                column: "nr_telefone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_Perfil_id_cargo",
                table: "T_Ajudante_familiar_Perfil",
                column: "id_cargo");

            migrationBuilder.CreateIndex(
                name: "IX_T_Ajudante_familiar_atividades_id_idoso",
                table: "T_Ajudante_familiar_atividades",
                column: "id_idoso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Alimentacao");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Consulta_Medicamento");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Doencas");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Parente");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_atividades");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Lembrete_Consulta");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Medicamentos");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Idoso");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Cuidador");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Perfil");

            migrationBuilder.DropTable(
                name: "T_Ajudante_familiar_Cargo");
        }
    }
}
