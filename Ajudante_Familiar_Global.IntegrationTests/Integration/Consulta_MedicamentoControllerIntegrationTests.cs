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
    public class Consulta_MedicamentoControllerIntegrationTests : IAsyncLifetime
    {
        private readonly ApiFactoryFixture _factory;
        private HttpClient _client = null!;

        public Consulta_MedicamentoControllerIntegrationTests(ApiFactoryFixture factory)
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
                Ds_senha = "12356",
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

        private async Task<Lembrete_Consulta> CriarLembreteConsultaAsync()
        {
            // Arrange
            var idoso = await CriarIdosoAsync();
            var novoLembrete = new
            {
                Ds_titulo_lembrente_consulta = "Consulta teste",
                Nm_consulta_medico = "Medico teste",
                Dt_consulta = new DateTime(2026, 8, 9),
                Hr_consulta = new TimeOnly(9, 0),
                Ds_consulta = "Consulta de integração",
                St_consulta = Status.Pendente,
                Id_idoso = idoso.ID_idoso
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/Lembrete_Consulta/lembrete_consulta/criar", novoLembrete);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var lembrete = await response.Content.ReadFromJsonAsync<Lembrete_Consulta>();
            Assert.NotNull(lembrete);
            return lembrete!;
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
            var response = await _client.PostAsJsonAsync("api/Medicamentos/medicamentos/criar", novoMedicamento);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var medicamento = await response.Content.ReadFromJsonAsync<Medicamentos>();
            Assert.NotNull(medicamento);
            return medicamento!;
        }

        private async Task<Consulta_Medicamento> CriarConsultaMedicamentoAsync()
        {
            // Arrange
            var lembrete = await CriarLembreteConsultaAsync();
            var medicamento = await CriarMedicamentoAsync();
            var novaConsultaMedicamento = new
            {
                id_lembrete_consulta = lembrete.Id_lembrete_consulta,
                id_Medicamentos = medicamento.id_medicamentos
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "api/Consulta_Medicamento/consulta_medicamento/criar", novaConsultaMedicamento);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criada a consulta medicamento: {erro}");
            }

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var consulta = await response.Content.ReadFromJsonAsync<Consulta_Medicamento>();
            Assert.NotNull(consulta);
            return consulta!;
        }

        [Fact]
        public async Task CriarConsultaMedicamento_DadosValidos_RetornaOk()
        {
            // Act
            var consultaCriada = await CriarConsultaMedicamentoAsync();

            // Assert
            Assert.True(consultaCriada.id_consulta_medicamento > 0);
            Assert.True(consultaCriada.id_lembrete_consulta > 0);
            Assert.True(consultaCriada.id_Medicamentos > 0);
        }

        [Fact]
        public async Task AtualizarConsultaMedicamento_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var consultaExistente = await CriarConsultaMedicamentoAsync();
            var lembrete = await CriarLembreteConsultaAsync();
            var medicamento = await CriarMedicamentoAsync();
            var consultaAtualizada = new
            {
                id_lembrete_consulta = lembrete.Id_lembrete_consulta,
                id_Medicamentos = medicamento.id_medicamentos
            };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"api/Consulta_Medicamento/consulta_medicamento/atualiza/{consultaExistente.id_consulta_medicamento}", consultaAtualizada);

            // Assert
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var consulta = await response.Content.ReadFromJsonAsync<Consulta_Medicamento>();
            Assert.True(consulta is not null, $"Resposta sem consulta: {responseBody}");
            Assert.Equal(lembrete.Id_lembrete_consulta, consulta!.id_lembrete_consulta);
            Assert.Equal(medicamento.id_medicamentos, consulta.id_Medicamentos);
        }

        [Fact]
        public async Task GetConsultaMedicamento_dados_RetornaOk()
        {
            // Arrange
            var consultaCriada = await CriarConsultaMedicamentoAsync();

            // Act
            var response = await _client.GetAsync(
                $"api/Consulta_Medicamento/consulta_medicamento/{consultaCriada.id_consulta_medicamento}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Consulta medicamento não encontrada");
            }
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var consulta = await response.Content.ReadFromJsonAsync<Consulta_Medicamento>();
            Assert.NotNull(consulta);
            Assert.Equal(consultaCriada.id_consulta_medicamento, consulta.id_consulta_medicamento);
        }

        [Fact]
        public async Task GetAllConsultaMedicamento_dados_RetornarOk()
        {
            // Arrange
            var consultaCriada = await CriarConsultaMedicamentoAsync();

            // Act
            var response = await _client.GetAsync("api/Consulta_Medicamento/consulta_medicamento");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var consultas = await response.Content.ReadFromJsonAsync<List<Consulta_Medicamento>>();
            Assert.NotNull(consultas);
            Assert.Contains(consultas, consulta => consulta.id_consulta_medicamento == consultaCriada.id_consulta_medicamento);
        }

        [Fact]
        public async Task DeleteConsultaMedicamento_dados_RetornaOk()
        {
            // Arrange
            var consultaCriada = await CriarConsultaMedicamentoAsync();

            // Act
            var response = await _client.DeleteAsync(
                $"api/Consulta_Medicamento/consulta_medicamento/deleta/{consultaCriada.id_consulta_medicamento}");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi deletada a consulta medicamento: {erro}");
            }
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var consultaResponse = await _client.GetAsync(
                $"api/Consulta_Medicamento/consulta_medicamento/{consultaCriada.id_consulta_medicamento}");
            Assert.Equal(HttpStatusCode.NotFound, consultaResponse.StatusCode);
        }
    }
}
