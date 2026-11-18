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
    public class DoencasControllerIntegrationTests : IAsyncLifetime
    {
        private readonly ApiFactoryFixture _factory;
        private HttpClient _client = null!;

        public DoencasControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _factory = factory;
        }

        public async Task InitializeAsync()
        {
            _client = await _factory.CreateAuthenticatedClientAsync();
        }

        public Task DisposeAsync()
        {
            _client.Dispose();
            return Task.CompletedTask;
        }

        private async Task<Cargo> CriarCargoAsync()
        {
            // Arrange
            var novoCargo = new
            {
                Nm_cargo = $"{Guid.NewGuid():N}"[..20],
                Fg_permissao = Permissao.S
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Cargo/cargo/criar", novoCargo);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var cargo = await response.Content.ReadFromJsonAsync<Cargo>();
            Assert.NotNull(cargo);
            return cargo!;
        }

        private async Task<Perfil> CriarPerfilAsync()
        {
            // Arrange
            var cargo = await CriarCargoAsync();
            var novoPerfil = new
            {
                nm_perfil = $"{Guid.NewGuid():N}"[..20],
                id_cargo = cargo.Id_cargo
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Perfil/perfil/criar", novoPerfil);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var perfil = await response.Content.ReadFromJsonAsync<Perfil>();

            Assert.NotNull(perfil);
            return perfil!;
        }

        private async Task<Cuidador> CriarCuidadorAsync()
        {
            // Arrange
            var perfil = await CriarPerfilAsync();
            var identificador = Guid.NewGuid().ToString("N");
            var novoCuidador = new
            {
                Nm_cuidador = $"{identificador}"[..20],
                Cpf_cuidador = identificador[..11],
                Ds_email = $"{identificador[..8]}",
                Ds_senha = "12213",
                Id_perfil = perfil.id_perfil
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Cuidador/cuidador/criar", novoCuidador);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var cuidador = await response.Content.ReadFromJsonAsync<Cuidador>();

            Assert.NotNull(cuidador);
            return cuidador!;
        }

        private async Task<Idoso> CriarIdosoAsync()
        {
            // Arrange
            var cuidador = await CriarCuidadorAsync();
            var identificador = Guid.NewGuid().ToString("N");
            var novoIdoso = new
            {
                Nm_idoso = $"{identificador}"[..20],
                cpf_idoso = identificador[..11],
                vlr_altura = 1.70m,
                vlr_peso = 70m,
                dt_nascimento = new DateOnly(1950, 1, 1),
                ds_comportamento = "Teste",
                id_cuidador = cuidador.Id_cuidador
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Idoso/idoso/criar", novoIdoso);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var idoso = await response.Content.ReadFromJsonAsync<Idoso>();
            Assert.NotNull(idoso);
            return idoso!;
        }

        private async Task<Doencas> CriarDoencaAsync()
        {
            // Arrange
            var idoso = await CriarIdosoAsync();
            var novaDoenca = new
            {
                Nm_doenca = "Doença teste",
                Ds_doenca = "Descrição teste",
                Id_idoso = idoso.ID_idoso
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Doencas/doencas/criar", novaDoenca);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criada a doença: {erro}");
            }

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var doenca = await response.Content.ReadFromJsonAsync<Doencas>();
            Assert.NotNull(doenca);
            return doenca!;
        }

        [Fact]
        public async Task CriarDoencas_DadosValidos_RetornaOk()
        {
            // Act
            var doencaCriada = await CriarDoencaAsync();

            // Assert
            Assert.True(doencaCriada.id_doenca > 0);
            Assert.Equal("Doença teste", doencaCriada.Nm_doenca);
        }

        [Fact]
        public async Task AtualizarDoencas_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var doencaExistente = await CriarDoencaAsync();
            var doencaAtualizada = new
            {
                Nm_doenca = "Doença atualizada",
                Ds_doenca = "Descrição atualizada",
                Id_idoso = doencaExistente.Id_idoso
            };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"api/Doencas/doencas/atualiza/{doencaExistente.id_doenca}", doencaAtualizada);

            // Assert
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var doenca = await response.Content.ReadFromJsonAsync<Doencas>();
            Assert.True(doenca is not null, $"Resposta sem doença: {responseBody}");
            Assert.Equal("Doença atualizada", doenca!.Nm_doenca);
        }

        [Fact]
        public async Task GetDoencas_dados_RetornaOk()
        {
            // Arrange
            var doencaCriada = await CriarDoencaAsync();

            // Act
            var response = await _client.GetAsync($"api/Doencas/doencas/{doencaCriada.id_doenca}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Doença não encontrada");
            }
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var doenca = await response.Content.ReadFromJsonAsync<Doencas>();
            Assert.NotNull(doenca);
            Assert.Equal(doencaCriada.id_doenca, doenca.id_doenca);
        }

        [Fact]
        public async Task GetAllDoencas_dados_RetornarOk()
        {
            // Arrange
            var doencaCriada = await CriarDoencaAsync();

            // Act
            var response = await _client.GetAsync("api/Doencas/doencas");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var doencas = await response.Content.ReadFromJsonAsync<List<Doencas>>();
            Assert.NotNull(doencas);
            Assert.Contains(doencas, doenca => doenca.id_doenca == doencaCriada.id_doenca);
        }

        [Fact]
        public async Task DeleteDoencas_dados_RetornaOk()
        {
            // Arrange
            var doencaCriada = await CriarDoencaAsync();

            // Act
            var response = await _client.DeleteAsync(
                $"api/Doencas/doencas/deleta/{doencaCriada.id_doenca}");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi deletada a doença: {erro}");
            }
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var consultaResponse = await _client.GetAsync(
                $"api/Doencas/doencas/{doencaCriada.id_doenca}");
            Assert.Equal(HttpStatusCode.NotFound, consultaResponse.StatusCode);
        }
    }
}
