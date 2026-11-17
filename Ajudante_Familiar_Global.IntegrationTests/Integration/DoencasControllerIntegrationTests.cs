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
    public class DoencasControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public DoencasControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CriarDoencas_DadosValidos_RetornaOk()
        {
            // Arrange
            var id_doenca = Random.Shared.Next(1, 100);

            var novaDoenca = new
            {
                id_doenca = id_doenca,
                Nm_doenca = "Doença teste",
                Ds_doenca = "Descrição teste",
                Id_idoso = 1
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Doencas/doencas/criar", novaDoenca);

            // Assert
            var erro = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Não foi criada por: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var doencaCriada = await response.Content.ReadFromJsonAsync<Doencas>();

            Assert.NotNull(doencaCriada);
            Assert.Equal(id_doenca, doencaCriada.id_doenca);
        }

        [Fact]
        public async Task AtualizarDoencas_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_doenca = 6;

            var doencaAtualizada = new
            {
                id_doenca = id_doenca,
                Nm_doenca = "Doença atualizada",
                Ds_doenca = "Descrição atualizada",
                Id_idoso = 1
            };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"api/Doencas/doencas/atualiza/{id_doenca}", doencaAtualizada);

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
        public async Task GetDoencas_dados_RetornaOk()
        {
            // Arrange
            var id_doenca = 6;

            // Act
            var response = await _client.GetAsync($"api/Doencas/doencas/{id_doenca}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Doença não encontrada");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var doenca = await response.Content.ReadFromJsonAsync<Doencas>();

            Assert.NotNull(doenca);
            Assert.Equal(id_doenca, doenca.id_doenca);
        }

        [Fact]
        public async Task GetAllDoencas_dados_RetornarOk()
        {
            //Act
            var response = await _client.GetAsync("api/Doencas/doencas");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var doencas = await response.Content.ReadFromJsonAsync<List<Doencas>>();

            Assert.NotNull(doencas);
        }

        [Fact]
        public async Task DeleteDoencas_dados_RetornaOk()
        {
            // Arrange
            var id_doenca = 55;

            // Act
            var response = await _client.DeleteAsync(
                $"api/Doencas/doencas/deleta/{id_doenca}");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erroAchado = await response.Content.ReadAsStringAsync();

                Assert.Fail($"Não foi deletada pelo motivo: {erroAchado}");
            }

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
