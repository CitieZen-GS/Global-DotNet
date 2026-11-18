using Ajudante_Familiar_Global.integrationTests.FactoryFixture;
using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Enums;
using System;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ajudante_Familiar_Global.integrationTests.Integration
{

    [Collection("ApiCollection")]
    public class MedicamentosControllerIntegrationTests : IAsyncLifetime
    {
        private readonly ApiFactoryFixture _factory;
        private HttpClient _client = null!;

        public MedicamentosControllerIntegrationTests(ApiFactoryFixture factory)
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
                Nm_cuidador = $"cuidador-{identificador}"[..20],
                Cpf_cuidador = identificador[..11],
                Ds_email = $"a{identificador[..8]}@x.co",
                Ds_senha = "senha123",
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
                Nm_idoso = $"idoso-{identificador}"[..20],
                cpf_idoso = identificador[..11],
                vlr_altura = 1.70m,
                vlr_peso = 70m,
                dt_nascimento = new DateOnly(1950, 1, 1),
                ds_comportamento = "Teste de integração",
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

        private async Task<Medicamentos> CriarMedicamentoAsync()
        {
            // Arrange
            var idoso = await CriarIdosoAsync();
            var novoMedicamento = new
            {
                Nm_medicamentos = "Medicamento teste",
                Via_administracao = "Oral",
                Vl_frquencia = "Uma vez ao dia",
                Ds_instrucao_uso = "Após refeição",
                Dt_medicamento = new DateTime(2026, 8, 9),
                Hr_medicamento = new TimeOnly(9, 0),
                Id_idoso = idoso.ID_idoso
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "api/Medicamentos/medicamentos/criar", novoMedicamento);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado o medicamento: {erro}");
            }

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var medicamento = await response.Content.ReadFromJsonAsync<Medicamentos>();
            Assert.NotNull(medicamento);
            return medicamento!;
        }

        [Fact]
        public async Task CriarMedicamentos_DadosValidos_RetornaOk()
        {
            // Act
            var medicamentoCriado = await CriarMedicamentoAsync();

            // Assert
            Assert.True(medicamentoCriado.id_medicamentos > 0);
            Assert.Equal("Medicamento teste", medicamentoCriado.Nm_medicamentos);
            Assert.True(medicamentoCriado.Id_idoso > 0);
        }

        [Fact]
        public async Task AtualizarMedicamentos_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var medicamentoExistente = await CriarMedicamentoAsync();
            var medicamentoAtualizado = new
            {
                Nm_medicamentos = "Medicamento atualizado",
                Via_administracao = "Oral",
                Vl_frquencia = "2 vezes ao dia",
                Ds_instrucao_uso = "Após refeição",
                Dt_medicamento = new DateTime(2026, 10, 12),
                Hr_medicamento = new TimeOnly(10, 0),
                Id_idoso = medicamentoExistente.Id_idoso
            };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"api/Medicamentos/medicamentos/atualiza/{medicamentoExistente.id_medicamentos}", medicamentoAtualizado);

            // Assert
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var medicamento = await response.Content.ReadFromJsonAsync<Medicamentos>();
            Assert.True(medicamento is not null, $"Resposta sem medicamento: {responseBody}");
            Assert.Equal("Medicamento atualizado", medicamento!.Nm_medicamentos);
            Assert.Equal("2 vezes ao dia", medicamento.Vl_frquencia);
        }

        [Fact]
        public async Task GetMedicamentos_dados_RetornaOk()
        {
            // Arrange
            var medicamentoCriado = await CriarMedicamentoAsync();

            // Act
            var response = await _client.GetAsync(
                $"api/Medicamentos/medicamentos/{medicamentoCriado.id_medicamentos}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Medicamento não encontrado");
            }
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var medicamento = await response.Content.ReadFromJsonAsync<Medicamentos>();
            Assert.NotNull(medicamento);
            Assert.Equal(medicamentoCriado.id_medicamentos, medicamento.id_medicamentos);
        }

        [Fact]
        public async Task GetAllMedicamentos_dados_RetornarOk()
        {
            // Arrange
            var medicamentoCriado = await CriarMedicamentoAsync();

            // Act
            var response = await _client.GetAsync("api/Medicamentos/medicamentos");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var medicamentos = await response.Content.ReadFromJsonAsync<List<Medicamentos>>();
            Assert.NotNull(medicamentos);
            Assert.Contains(medicamentos, medicamento => medicamento.id_medicamentos == medicamentoCriado.id_medicamentos);
        }

        [Fact]
        public async Task DeleteMedicamentos_dados_RetornaOk()
        {
            // Arrange
            var medicamentoCriado = await CriarMedicamentoAsync();

            // Act
            var response = await _client.DeleteAsync(
                $"api/Medicamentos/medicamentos/deleta/{medicamentoCriado.id_medicamentos}");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi deletado o medicamento: {erro}");
            }
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var consultaResponse = await _client.GetAsync(
                $"api/Medicamentos/medicamentos/{medicamentoCriado.id_medicamentos}");
            Assert.Equal(HttpStatusCode.NotFound, consultaResponse.StatusCode);
        }
    }
}
