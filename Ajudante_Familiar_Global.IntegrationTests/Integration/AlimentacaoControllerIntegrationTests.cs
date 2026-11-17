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
    public class AlimentacaoControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public AlimentacaoControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CriarAlimentacao_DadosValidos_RetornaOk()
        {
            // Arrange
            var id_alimentacao = Random.Shared.Next(1, 100);

            var novaAlimentacao = new
            {
                Id_alimentacao = id_alimentacao,
                Nm_alimentacao = "Refeição de teste",
                Tp_alimentacao = TipoAlimentacao.almoco,
                Dt_consumo = new DateOnly(2026, 8, 9),
                Hr_consumo = new TimeOnly(12, 0),
                Vlr_caloria = 450m,
                Ds_preparacao = "Preparação de teste",
                Id_idoso = 1
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Alimentacao/alimentacao/criar", novaAlimentacao);

            // Assert
            var erro = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Não foi criada a alimentação: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var alimentacaoCriada = await response.Content.ReadFromJsonAsync<Alimentacao>();

            Assert.NotNull(alimentacaoCriada);
            Assert.Equal(id_alimentacao, alimentacaoCriada.Id_alimentacao);
        }

        [Fact]
        public async Task AtualizarAlimentacao_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_alimentacao = 6;

            var alimentacaoAtualizada = new
            {
                Id_alimentacao = id_alimentacao,
                Nm_alimentacao = "Refeição atualizada",
                Tp_alimentacao = TipoAlimentacao.jantar,
                Dt_consumo = new DateOnly(2026, 10, 12),
                Hr_consumo = new TimeOnly(19, 0),
                Vlr_caloria = 500m,
                Ds_preparacao = "Preparação atualizada",
                Id_idoso = 1
            };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"api/Alimentacao/alimentacao/atualiza/{id_alimentacao}", alimentacaoAtualizada);

            // Assert
            var erro = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Não foi atualizada a alimentação: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAlimentacao_dados_RetornaOk()
        {
            // Arrange
            var id_alimentacao = 6;

            // Act
            var response = await _client.GetAsync($"api/Alimentacao/alimentacao/{id_alimentacao}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Alimentação não encontrada");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var alimentacao = await response.Content.ReadFromJsonAsync<Alimentacao>();

            Assert.NotNull(alimentacao);
            Assert.Equal(id_alimentacao, alimentacao.Id_alimentacao);
        }

        [Fact]
        public async Task GetAllAlimentacao_dados_RetornarOk()
        {
            //Act
            var response = await _client.GetAsync("api/Alimentacao/alimentacao");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var alimentacoes = await response.Content.ReadFromJsonAsync<List<Alimentacao>>();

            Assert.NotNull(alimentacoes);
        }

        [Fact]
        public async Task DeleteAlimentacao_dados_RetornaOk()
        {
            // Arrange
            var id_alimentacao = 55;

            // Act
            var response = await _client.DeleteAsync(
                $"api/Alimentacao/alimentacao/deleta/{id_alimentacao}");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erroAchado = await response.Content.ReadAsStringAsync();

                Assert.Fail($"Não foi deletada a alimentação pelo motivo: {erroAchado}");
            }

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
