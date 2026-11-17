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
    public class CargoControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public CargoControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CriarCargo_DadosValidos_RetornaOk()
        {
            // Arrange
            var id_cargo = Random.Shared.Next(1, 100);

            var novoCargo = new
            {
                Id_cargo = id_cargo,
                Nm_cargo = "Cargo teste",
                Fg_permissao = Permissao.N
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Cargo/cargo/criar", novoCargo);

            // Assert
            var erro = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Não foi criado por: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var cargoCriado = await response.Content.ReadFromJsonAsync<Cargo>();

            Assert.NotNull(cargoCriado);
            Assert.Equal(id_cargo, cargoCriado.Id_cargo);
        }

        [Fact]
        public async Task AtualizarCargo_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_cargo = 6;

            var cargoAtualizado = new
            {
                Id_cargo = id_cargo,
                Nm_cargo = "Cargo atualizado",
                Fg_permissao = Permissao.S
            };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"api/Cargo/cargo/atualiza/{id_cargo}", cargoAtualizado);

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
        public async Task GetCargo_dados_RetornaOk()
        {
            // Arrange
            var id_cargo = 6;

            // Act
            var response = await _client.GetAsync($"api/Cargo/cargo/{id_cargo}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Cargo não encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var cargo = await response.Content.ReadFromJsonAsync<Cargo>();

            Assert.NotNull(cargo);
            Assert.Equal(id_cargo, cargo.Id_cargo);
        }

        [Fact]
        public async Task GetAllCargo_dados_RetornarOk()
        {
            //Act
            var response = await _client.GetAsync("api/Cargo/cargo");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var cargos = await response.Content.ReadFromJsonAsync<List<Cargo>>();

            Assert.NotNull(cargos);
        }

        [Fact]
        public async Task DeleteCargo_dados_RetornaOk()
        {
            // Arrange
            var id_cargo = 55;

            // Act
            var response = await _client.DeleteAsync(
                $"api/Cargo/cargo/deleta/{id_cargo}");

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
