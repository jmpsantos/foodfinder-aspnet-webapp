using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodFinder_WebApp.Migrations
{
    public partial class FirstStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bloqueado",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    motivo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloqueado", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Localizacao",
                columns: table => new
                {
                    localizacao_id = table.Column<long>(type: "bigint", nullable: false),
                    codigo_postal = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    morada = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    localidade = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    gps_Latitude = table.Column<double>(type: "float", nullable: false),
                    gps_Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacao", x => x.localizacao_id);
                });

            migrationBuilder.CreateTable(
                name: "Prato_do_Dia",
                columns: table => new
                {
                    prato_id = table.Column<long>(type: "bigint", nullable: false),
                    descricao = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    tipo = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prato_do__8C40FE1F46BE0181", x => x.prato_id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    username = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    registo_Confirmado = table.Column<bool>(type: "bit", nullable: false),
                    bloqueado_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Utilizad__F3DBC5738F795E7B", x => x.username);
                    table.ForeignKey(
                        name: "FK__Utilizado__bloqu__38996AB5",
                        column: x => x.bloqueado_ID,
                        principalTable: "Bloqueado",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    administrador_id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.administrador_id);
                    table.ForeignKey(
                        name: "FK__Administr__admin__45F365D3",
                        column: x => x.administrador_id,
                        principalTable: "Utilizador",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    cliente_id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.cliente_id);
                    table.ForeignKey(
                        name: "FK__Cliente__cliente__4316F928",
                        column: x => x.cliente_id,
                        principalTable: "Utilizador",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Restaurante",
                columns: table => new
                {
                    restaurante_id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    contacto_email = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    contacto_telefone = table.Column<long>(type: "bigint", nullable: false),
                    horario_funcionamento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    dia_de_descanso = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    tipo_de_servico = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    localizacao_id = table.Column<long>(type: "bigint", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurante", x => x.restaurante_id);
                    table.ForeignKey(
                        name: "FK__Restauran__local__403A8C7D",
                        column: x => x.localizacao_id,
                        principalTable: "Localizacao",
                        principalColumn: "localizacao_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Restauran__resta__3F466844",
                        column: x => x.restaurante_id,
                        principalTable: "Utilizador",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favoritar_Prato_do_Dia",
                columns: table => new
                {
                    prato_id = table.Column<long>(type: "bigint", nullable: false),
                    cliente_id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorita__D83ECAC971115171", x => new { x.prato_id, x.cliente_id });
                    table.ForeignKey(
                        name: "FK__Favoritar__clien__571DF1D5",
                        column: x => x.cliente_id,
                        principalTable: "Cliente",
                        principalColumn: "cliente_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Favoritar__prato__5629CD9C",
                        column: x => x.prato_id,
                        principalTable: "Prato_do_Dia",
                        principalColumn: "prato_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Adicionar_Prato_do_Dia",
                columns: table => new
                {
                    restaurante_id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    prato_id = table.Column<long>(type: "bigint", nullable: false),
                    data_prato = table.Column<DateTime>(type: "date", nullable: false),
                    preco = table.Column<double>(type: "float", nullable: false),
                    destacado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Adiciona__522977CAF98A22AC", x => new { x.prato_id, x.restaurante_id, x.data_prato });
                    table.ForeignKey(
                        name: "FK__Adicionar__prato__4F7CD00D",
                        column: x => x.prato_id,
                        principalTable: "Prato_do_Dia",
                        principalColumn: "prato_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Adicionar__resta__4E88ABD4",
                        column: x => x.restaurante_id,
                        principalTable: "Restaurante",
                        principalColumn: "restaurante_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comentario_Restaurante",
                columns: table => new
                {
                    comentario_id = table.Column<long>(type: "bigint", nullable: false),
                    data_comentario = table.Column<DateTime>(type: "date", nullable: false),
                    restaurante_id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    cliente_id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    corpo = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comentar__5661CA6A7722218D", x => x.comentario_id);
                    table.ForeignKey(
                        name: "FK__Comentari__clien__49C3F6B7",
                        column: x => x.cliente_id,
                        principalTable: "Cliente",
                        principalColumn: "cliente_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Comentari__resta__48CFD27E",
                        column: x => x.restaurante_id,
                        principalTable: "Restaurante",
                        principalColumn: "restaurante_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favoritar_Restaurante",
                columns: table => new
                {
                    restaurante_id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    cliente_id = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorita__25831046647C40B4", x => new { x.restaurante_id, x.cliente_id });
                    table.ForeignKey(
                        name: "FK__Favoritar__clien__534D60F1",
                        column: x => x.cliente_id,
                        principalTable: "Cliente",
                        principalColumn: "cliente_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Favoritar__resta__52593CB8",
                        column: x => x.restaurante_id,
                        principalTable: "Restaurante",
                        principalColumn: "restaurante_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adicionar_Prato_do_Dia_restaurante_id",
                table: "Adicionar_Prato_do_Dia",
                column: "restaurante_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_Restaurante_cliente_id",
                table: "Comentario_Restaurante",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_Restaurante_restaurante_id",
                table: "Comentario_Restaurante",
                column: "restaurante_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritar_Prato_do_Dia_cliente_id",
                table: "Favoritar_Prato_do_Dia",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritar_Restaurante_cliente_id",
                table: "Favoritar_Restaurante",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurante_localizacao_id",
                table: "Restaurante",
                column: "localizacao_id");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizador_bloqueado_ID",
                table: "Utilizador",
                column: "bloqueado_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adicionar_Prato_do_Dia");

            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "Comentario_Restaurante");

            migrationBuilder.DropTable(
                name: "Favoritar_Prato_do_Dia");

            migrationBuilder.DropTable(
                name: "Favoritar_Restaurante");

            migrationBuilder.DropTable(
                name: "Prato_do_Dia");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Restaurante");

            migrationBuilder.DropTable(
                name: "Localizacao");

            migrationBuilder.DropTable(
                name: "Utilizador");

            migrationBuilder.DropTable(
                name: "Bloqueado");
        }
    }
}
