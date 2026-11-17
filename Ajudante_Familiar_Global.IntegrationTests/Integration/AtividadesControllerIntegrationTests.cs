using Ajudante_Familiar_Global.integrationTests.FactoryFixture;
using Ajudante_Familiar_Global.Domain.Entities;
using System;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ajudante_Familiar_Global.integrationTests.Integration
{
    //Atenção!!! Tanto o create quanto o update precisam ter cuidado com os dados que serão adicionados, porque, se não, pode dar erro.
    //Recomendação faz um a cada vez, não faz eles tudo junto
    [Collection("ApiCollection")]
    public class AtividadesControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public AtividadesControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CriarAtividades_DadosValidos_RetornaOk()
        {
            // Arrange
            var id_atividade = Random.Shared.Next(1, 100);

            var novaAtividade = new
            {
                Id_atividade = id_atividade,
                Nm_atividade = "Atividade teste",
                Ds_atividade = "Descrição teste",
                Dt_atividade = new DateOnly(2026, 8, 9),
                Hr_atividade_inicio = new TimeOnly(9, 0),
                Hr_atividade_fim = new TimeOnly(10, 0),
                id_idoso = 1
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Atividades/atividades/criar", novaAtividade);

            // Assert
            var erro = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Não foi criada por: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var atividadeCriada = await response.Content.ReadFromJsonAsync<Atividades>();

            Assert.NotNull(atividadeCriada);
            Assert.Equal(id_atividade, atividadeCriada.Id_atividade);
        }

        [Fact]
        public async Task AtualizarAtividades_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_atividade = 6;

            var atividadeAtualizada = new
            {
                Id_atividade = id_atividade,
                Nm_atividade = "Atividade atualizada",
                Ds_atividade = "Descrição atualizada",
                Dt_atividade = new DateOnly(2026, 10, 12),
                Hr_atividade_inicio = new TimeOnly(10, 0),
                Hr_atividade_fim = new TimeOnly(11, 0),
                id_idoso = 1
            };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"api/Atividades/atividades/atualiza/{id_atividade}", atividadeAtualizada);

            // Assert
            var erro = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Não foi atualizada por: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAtividades_dados_RetornaOk()
        {
            // Arrange
            var id_atividade = 6;

            // Act
            var response = await _client.GetAsync($"api/Atividades/atividades/{id_atividade}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Atividade não encontrada");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var atividade = await response.Content.ReadFromJsonAsync<Atividades>();

            Assert.NotNull(atividade);
            Assert.Equal(id_atividade, atividade.Id_atividade);
        }

        [Fact]
        public async Task GetAllAtividades_dados_RetornarOk()
        {
            //Act
            var response = await _client.GetAsync("api/Atividades/atividades");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var atividades = await response.Content.ReadFromJsonAsync<List<Atividades>>();

            Assert.NotNull(atividades);
        }

        [Fact]
        public async Task DeleteAtividades_dados_RetornaOk()
        {
            // Arrange
            var id_atividade = 55;

            // Act
            var response = await _client.DeleteAsync(
                $"api/Atividades/atividades/deleta/{id_atividade}");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erroAchado = await response.Content.ReadAsStringAsync();

                Assert.Fail($"Não foi deletada a atividade pelo motivo: {erroAchado}");
            }

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
