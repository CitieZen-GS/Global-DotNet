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
    public class Consulta_MedicamentoControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public Consulta_MedicamentoControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CriarConsultaMedicamento_DadosValidos_RetornaOk()
        {
            // Arrange
            var id_consulta_medicamento = Random.Shared.Next(1, 100);

            var novaConsultaMedicamento = new
            {
                id_consulta_medicamento = id_consulta_medicamento,
                id_lembrete_consulta = 1,
                id_Medicamentos = 1
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "api/Consulta_Medicamento/consulta_medicamento/criar", novaConsultaMedicamento);

            // Assert
            var erro = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail($"Não foi criada por: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var consultaCriada = await response.Content.ReadFromJsonAsync<Consulta_Medicamento>();

            Assert.NotNull(consultaCriada);
            Assert.Equal(id_consulta_medicamento, consultaCriada.id_consulta_medicamento);
        }

        [Fact]
        public async Task AtualizarConsultaMedicamento_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_consulta_medicamento = 6;

            var consultaAtualizada = new
            {
                id_consulta_medicamento = id_consulta_medicamento,
                id_lembrete_consulta = 1,
                id_Medicamentos = 1
            };

            // Act
            var response = await _client.PutAsJsonAsync(
                $"api/Consulta_Medicamento/consulta_medicamento/atualiza/{id_consulta_medicamento}", consultaAtualizada);

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
        public async Task GetConsultaMedicamento_dados_RetornaOk()
        {
            // Arrange
            var id_consulta_medicamento = 6;

            // Act
            var response = await _client.GetAsync(
                $"api/Consulta_Medicamento/consulta_medicamento/{id_consulta_medicamento}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Consulta medicamento não encontrada");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var consulta = await response.Content.ReadFromJsonAsync<Consulta_Medicamento>();

            Assert.NotNull(consulta);
            Assert.Equal(id_consulta_medicamento, consulta.id_consulta_medicamento);
        }

        [Fact]
        public async Task GetAllConsultaMedicamento_dados_RetornarOk()
        {
            //Act
            var response = await _client.GetAsync("api/Consulta_Medicamento/consulta_medicamento");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var consultas = await response.Content.ReadFromJsonAsync<List<Consulta_Medicamento>>();

            Assert.NotNull(consultas);
        }

        [Fact]
        public async Task DeleteConsultaMedicamento_dados_RetornaOk()
        {
            // Arrange
            var id_consulta_medicamento = 55;

            // Act
            var response = await _client.DeleteAsync(
                $"api/Consulta_Medicamento/consulta_medicamento/deleta/{id_consulta_medicamento}");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erroAchado = await response.Content.ReadAsStringAsync();

                Assert.Fail($"Não foi deletada a consulta pelo motivo: {erroAchado}");
            }

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
