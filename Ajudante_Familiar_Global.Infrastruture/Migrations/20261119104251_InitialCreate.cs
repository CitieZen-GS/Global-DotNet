
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ajudante_Familiar_Global.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_CARGO",
                columns: table => new
                {
                    ID_CARGO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_CARGO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    FG_PERMISSAO = table.Column<string>(type: "NVARCHAR2(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_CARGO", x => x.ID_CARGO);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_PERFIL",
                columns: table => new
                {
                    ID_PERFIL = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_PERFIL = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ID_CARGO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_PERFIL", x => x.ID_PERFIL);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_PERFIL_T_AJUDANTE_FAMILIAR_CARGO_ID_CARGO",
                        column: x => x.ID_CARGO,
                        principalTable: "T_AJUDANTE_FAMILIAR_CARGO",
                        principalColumn: "ID_CARGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_CUIDADOR",
                columns: table => new
                {
                    ID_CUIDADOR = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_CUIDADOR = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CPF_CUIDADOR = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    DS_EMAIL = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DS_SENHA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ID_PERFIL = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_CUIDADOR", x => x.ID_CUIDADOR);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_CUIDADOR_T_AJUDANTE_FAMILIAR_PERFIL_ID_PERFIL",
                        column: x => x.ID_PERFIL,
                        principalTable: "T_AJUDANTE_FAMILIAR_PERFIL",
                        principalColumn: "ID_PERFIL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_IDOSO",
                columns: table => new
                {
                    ID_IDOSO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_IDOSO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CPF_IDOSO = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    VLR_ALTURA = table.Column<decimal>(type: "DECIMAL(3,2)", precision: 3, scale: 2, nullable: false),
                    VLR_PESO = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(type: "DATE", nullable: false),
                    DS_COMPORTAMENTO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ID_CUIDADOR = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_IDOSO", x => x.ID_IDOSO);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_IDOSO_T_AJUDANTE_FAMILIAR_CUIDADOR_ID_CUIDADOR",
                        column: x => x.ID_CUIDADOR,
                        principalTable: "T_AJUDANTE_FAMILIAR_CUIDADOR",
                        principalColumn: "ID_CUIDADOR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_ALIMENTACAO",
                columns: table => new
                {
                    ID_ALIMENTACAO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_ALIMENTACAO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TP_ALIMENTACAO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DT_CONSUMO = table.Column<DateTime>(type: "DATE", nullable: false),
                    HR_CONSUMO = table.Column<DateTime>(type: "DATE", nullable: false),
                    VLR_CALORIA = table.Column<decimal>(type: "DECIMAL(6,2)", precision: 6, scale: 2, nullable: false),
                    DS_PREPARACAO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ID_IDOSO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_ALIMENTACAO", x => x.ID_ALIMENTACAO);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_ALIMENTACAO_T_AJUDANTE_FAMILIAR_IDOSO_ID_IDOSO",
                        column: x => x.ID_IDOSO,
                        principalTable: "T_AJUDANTE_FAMILIAR_IDOSO",
                        principalColumn: "ID_IDOSO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_ATIVIDADES",
                columns: table => new
                {
                    ID_ATIVIDADE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_IDOSO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NM_ATIVIDADE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DS_ATIVIDADE = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    DT_ATIVIDADE = table.Column<DateTime>(type: "DATE", nullable: false),
                    HR_ATIVIDADE_INICIO = table.Column<DateTime>(type: "DATE", nullable: false),
                    HR_ATIVIDADE_FIM = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_ATIVIDADES", x => new { x.ID_ATIVIDADE, x.ID_IDOSO });
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_ATIVIDADES_T_AJUDANTE_FAMILIAR_IDOSO_ID_IDOSO",
                        column: x => x.ID_IDOSO,
                        principalTable: "T_AJUDANTE_FAMILIAR_IDOSO",
                        principalColumn: "ID_IDOSO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_DOENCAS",
                columns: table => new
                {
                    ID_DOENCA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_DOENCA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DS_DOENCA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ID_IDOSO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_DOENCAS", x => x.ID_DOENCA);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_DOENCAS_T_AJUDANTE_FAMILIAR_IDOSO_ID_IDOSO",
                        column: x => x.ID_IDOSO,
                        principalTable: "T_AJUDANTE_FAMILIAR_IDOSO",
                        principalColumn: "ID_IDOSO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_LEMBRETE_CONSULTA",
                columns: table => new
                {
                    ID_LEMBRETE_CONSULTA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DS_TITULO_LEMBRENTE_CONSULTA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    NM_CONSULTA_MEDICO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DT_CONSULTA = table.Column<DateTime>(type: "DATE", nullable: false),
                    HR_CONSULTA = table.Column<DateTime>(type: "DATE", nullable: true),
                    DS_CONSULTA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ST_CONSULTA = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    ID_IDOSO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_LEMBRETE_CONSULTA", x => x.ID_LEMBRETE_CONSULTA);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_LEMBRETE_CONSULTA_T_AJUDANTE_FAMILIAR_IDOSO_ID_IDOSO",
                        column: x => x.ID_IDOSO,
                        principalTable: "T_AJUDANTE_FAMILIAR_IDOSO",
                        principalColumn: "ID_IDOSO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_MEDICAMENTOS",
                columns: table => new
                {
                    ID_MEDICAMENTOS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_MEDICAMENTOS = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    VIA_ADMINISTRACAO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    VL_FRQUENCIA = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DS_INSTRUCAO_USO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DT_MEDICAMENTO = table.Column<DateTime>(type: "DATE", nullable: true),
                    HR_MEDICAMENTO = table.Column<DateTime>(type: "DATE", nullable: false),
                    ID_IDOSO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_MEDICAMENTOS", x => x.ID_MEDICAMENTOS);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_MEDICAMENTOS_T_AJUDANTE_FAMILIAR_IDOSO_ID_IDOSO",
                        column: x => x.ID_IDOSO,
                        principalTable: "T_AJUDANTE_FAMILIAR_IDOSO",
                        principalColumn: "ID_IDOSO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_PARENTE",
                columns: table => new
                {
                    ID_PARENTE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_PARENTE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    NR_TELEFONE = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false),
                    DS_EMAIL = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DS_SENHA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ID_IDOSO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_PERFIL = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_PARENTE", x => x.ID_PARENTE);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_PARENTE_T_AJUDANTE_FAMILIAR_IDOSO_ID_IDOSO",
                        column: x => x.ID_IDOSO,
                        principalTable: "T_AJUDANTE_FAMILIAR_IDOSO",
                        principalColumn: "ID_IDOSO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_PARENTE_T_AJUDANTE_FAMILIAR_PERFIL_ID_PERFIL",
                        column: x => x.ID_PERFIL,
                        principalTable: "T_AJUDANTE_FAMILIAR_PERFIL",
                        principalColumn: "ID_PERFIL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO",
                columns: table => new
                {
                    ID_CONSULTA_MEDICAMENTO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_LEMBRETE_CONSULTA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_MEDICAMENTOS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO", x => x.ID_CONSULTA_MEDICAMENTO);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO_T_AJUDANTE_FAMILIAR_LEMBRETE_CONSULTA_ID_LEMBRETE_CONSULTA",
                        column: x => x.ID_LEMBRETE_CONSULTA,
                        principalTable: "T_AJUDANTE_FAMILIAR_LEMBRETE_CONSULTA",
                        principalColumn: "ID_LEMBRETE_CONSULTA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO_T_AJUDANTE_FAMILIAR_MEDICAMENTOS_ID_MEDICAMENTOS",
                        column: x => x.ID_MEDICAMENTOS,
                        principalTable: "T_AJUDANTE_FAMILIAR_MEDICAMENTOS",
                        principalColumn: "ID_MEDICAMENTOS",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_ALIMENTACAO_ID_IDOSO",
                table: "T_AJUDANTE_FAMILIAR_ALIMENTACAO",
                column: "ID_IDOSO");

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_ATIVIDADES_ID_IDOSO",
                table: "T_AJUDANTE_FAMILIAR_ATIVIDADES",
                column: "ID_IDOSO");

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO_ID_LEMBRETE_CONSULTA",
                table: "T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO",
                column: "ID_LEMBRETE_CONSULTA");

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO_ID_MEDICAMENTOS",
                table: "T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO",
                column: "ID_MEDICAMENTOS");

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_CUIDADOR_ID_PERFIL",
                table: "T_AJUDANTE_FAMILIAR_CUIDADOR",
                column: "ID_PERFIL");

            migrationBuilder.CreateIndex(
                name: "UK_CFP_CUIDADOR",
                table: "T_AJUDANTE_FAMILIAR_CUIDADOR",
                column: "CPF_CUIDADOR",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_DS_CUIDADOR",
                table: "T_AJUDANTE_FAMILIAR_CUIDADOR",
                column: "DS_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_DOENCAS_ID_IDOSO",
                table: "T_AJUDANTE_FAMILIAR_DOENCAS",
                column: "ID_IDOSO");

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_IDOSO_ID_CUIDADOR",
                table: "T_AJUDANTE_FAMILIAR_IDOSO",
                column: "ID_CUIDADOR");

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_LEMBRETE_CONSULTA_ID_IDOSO",
                table: "T_AJUDANTE_FAMILIAR_LEMBRETE_CONSULTA",
                column: "ID_IDOSO");

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_MEDICAMENTOS_ID_IDOSO",
                table: "T_AJUDANTE_FAMILIAR_MEDICAMENTOS",
                column: "ID_IDOSO");

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_PARENTE_ID_IDOSO",
                table: "T_AJUDANTE_FAMILIAR_PARENTE",
                column: "ID_IDOSO");

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_PARENTE_ID_PERFIL",
                table: "T_AJUDANTE_FAMILIAR_PARENTE",
                column: "ID_PERFIL");

            migrationBuilder.CreateIndex(
                name: "UK_DS_EMAIL_PARENTE",
                table: "T_AJUDANTE_FAMILIAR_PARENTE",
                column: "DS_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_NR_TELEFONE_PARENTE",
                table: "T_AJUDANTE_FAMILIAR_PARENTE",
                column: "NR_TELEFONE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_AJUDANTE_FAMILIAR_PERFIL_ID_CARGO",
                table: "T_AJUDANTE_FAMILIAR_PERFIL",
                column: "ID_CARGO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_ALIMENTACAO");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_ATIVIDADES");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_CONSULTA_MEDICAMENTO");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_DOENCAS");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_PARENTE");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_LEMBRETE_CONSULTA");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_MEDICAMENTOS");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_IDOSO");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_CUIDADOR");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_PERFIL");

            migrationBuilder.DropTable(
                name: "T_AJUDANTE_FAMILIAR_CARGO");
        }
    }
}
