using Ajudante_Familiar_Global.integrationTests.FactoryFixture;
using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Enums;
using System;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ajudante_Familiar_Global.integrationTests.Integration
{
    //Atenção!!! Tanto o create quanto o update precisam ter cuidado com os dados que serão adicionados, porque, se não, pode dar erro.
    //Recomendação faz um a cada vez, não faz eles tudo junto
    [Collection("ApiCollection")]
    public class Lembrete_ConsultaControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public Lembrete_ConsultaControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CriarLembreteConsulta_DadosValidos_RetornaOk()
        {
            // Arrange
            var id_lembrete_consulta = Random.Shared.Next(1, 100);

            var novoLembrete = new
            {
                id_lembrete_consulta = id_lembrete_consulta,
                Ds_titulo_lembrente_consulta = "Consulta teste",
                Nm_consulta_medico = "Médico teste",
                Dt_consulta = new DateTime(2026, 8, 9),
                Hr_consulta = new DateTime(2026, 8, 9, 9, 0, 0),
                Ds_consulta = "Descrição teste",
                St_consulta = Status.Pendente,
                Id_idoso = 1
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "api/Lembrete_Consulta/lembrete_consulta/criar", novoLembrete);

            // Assert
            var erro = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Não foi criado por: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var lembreteCriado = await response.Content.ReadFromJsonAsync<Lembrete_Consulta>();

            Assert.NotNull(lembreteCriado);
            Assert.Equal(id_lembrete_consulta, lembreteCriado.id_lembrete_consulta);
        }

        [Fact]
        public async Task AtualizarLembreteConsulta_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_lembrete_consulta = 6;

            var lembreteAtualizado = new
            {
                id_lembrete_consulta = id_lembrete_consulta,
                Ds_titulo_lembrente_consulta = "Consulta atualizada",
                Nm_consulta_medico = "Médico atualizado",
                Dt_consulta = new DateTime(2026, 10, 12),
                Hr_consulta = new DateTime(2026, 10, 12, 10, 0, 0),
                Ds_consulta = "Descrição atualizada",
                St_consulta = Status.Realizado,
                Id_idoso = 1
            };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"api/Lembrete_Consulta/lembrete_consulta/atualiza/{id_lembrete_consulta}", lembreteAtualizado);

            // Assert
            var erro = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Não foi atualizado por: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetLembreteConsulta_dados_RetornaOk()
        {
            // Arrange
            var id_lembrete_consulta = 6;

            // Act
            var response = await _client.GetAsync(
                $"api/Lembrete_Consulta/lembrete_consulta/{id_lembrete_consulta}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Lembrete de consulta não encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var lembrete = await response.Content.ReadFromJsonAsync<Lembrete_Consulta>();

            Assert.NotNull(lembrete);
            Assert.Equal(id_lembrete_consulta, lembrete.id_lembrete_consulta);
        }

        [Fact]
        public async Task GetAllLembreteConsulta_dados_RetornarOk()
        {
            //Act
            var response = await _client.GetAsync("api/Lembrete_Consulta/lembrete_consulta");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var lembretes = await response.Content.ReadFromJsonAsync<List<Lembrete_Consulta>>();

            Assert.NotNull(lembretes);
        }

        [Fact]
        public async Task DeleteLembreteConsulta_dados_RetornaOk()
        {
            // Arrange
            var id_lembrete_consulta = 55;

            // Act
            var response = await _client.DeleteAsync(
                $"api/Lembrete_Consulta/lembrete_consulta/deleta/{id_lembrete_consulta}");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erroAchado = await response.Content.ReadAsStringAsync();

                Assert.Fail($"Não foi deletado pelo motivo: {erroAchado}");
            }

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
