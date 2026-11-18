using Ajudante_Familiar_Global.Infrastruture.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace Ajudante_Familiar_Global.integrationTests.FactoryFixture
{
    public class ApiFactoryFixture : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<AppDbContext>();
                services.RemoveAll<DbContextOptions<AppDbContext>>();
                services.RemoveAll<IDbContextOptionsConfiguration<AppDbContext>>();
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("ApiIntegrationTests"));
            });
        }
        //O motivo disso ter o authorize presente na classe e por causa disso para testa o integrationTest foi adicionar a questao do jwt
        public async Task<HttpClient> CreateAuthenticatedClientAsync()
        {
            var client = CreateClient();
            var loginResponse = await client.PostAsJsonAsync("api/auth/login", new
            {
                Username = "admin",
                Password = "123456"
            });

            if (!loginResponse.IsSuccessStatusCode)
            {
                var error = await loginResponse.Content.ReadAsStringAsync();
                client.Dispose();
                throw new InvalidOperationException($"Não foi possível autenticar o cliente de integração: {error}");
            }

            var login = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
            if (string.IsNullOrWhiteSpace(login?.Token))
            {
                client.Dispose();
                throw new InvalidOperationException("A resposta de login não contém um token JWT.");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Token);
            return client;
        }

        private sealed class LoginResponse
        {
            public string? Token { get; set; }
        }
    }

    [CollectionDefinition("ApiCollection")]
    public class ApiCollection : ICollectionFixture<ApiFactoryFixture>
    {

    }
   
}
