using Ajudante_Familiar_Global.Controllers;
using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Ajudante_Familiar_Global.Infrastruture.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ajudante_Familiar_Global.UnitTests.Controllers;

public class DoencasControllerTests
{
    private readonly Mock<IDoencasService> _doencas_ServiceMock;
    private readonly DoencasController _controller;
    private readonly ILogger<DoencasController> _logger;
    private readonly AppDbContext _context;

    public DoencasControllerTests()
    {
        _doencas_ServiceMock = new Mock<IDoencasService>();
        var loggMock = new Mock<ILogger<DoencasController>>();

        _logger = loggMock.Object;

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);

        _controller = new DoencasController(_doencas_ServiceMock.Object, _logger);

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        var urlHelper = new Mock<IUrlHelper>();
        urlHelper.Setup(helper => helper.Link(It.IsAny<string?>(), It.IsAny<object?>()))
                .Returns((string? routeName, object? _) => $"https://localhost/{routeName}");
        _controller.Url = urlHelper.Object;
    }

    //Criação
    [Fact]
    public async Task Create_Doencas_RetornaCreated()
    {
        // Arrange
        var cuidador = new Cuidador
        {
            Id_cuidador = 1,
            Nm_cuidador = " teste",
            Cpf_cuidador = "12345678900",
            Ds_email = "teste@email.com",
            Ds_senha = "senha",
            Id_perfil = 1
        };
        var idoso = new Idoso
        {
            ID_idoso = 1,
            Nm_idoso = " teste",
            cpf_idoso = "12345678900",
            vlr_altura = 1.7m,
            vlr_peso = 70m,
            dt_nascimento = new DateOnly(1950, 1, 1),
            ds_comportamento = " teste",
            id_cuidador = 1,
            cuidador = cuidador
        };
        var doencas = new Doencas
        {
            id_doenca = 1,
            Nm_doenca = " teste",
            Ds_doenca = " teste",
            Id_idoso = 1,
            Idoso = idoso
        };

        _doencas_ServiceMock.Setup(service => service.CreateDoencasAsync(doencas)).ReturnsAsync(doencas);

        // Act
        var resultado = await _controller.PostDoencas(doencas);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(resultado.Result);
        var doencasRetornadas = Assert.IsType<Doencas>(createdResult.Value);
        Assert.NotNull(doencasRetornadas);
        Assert.Equal(doencas.id_doenca, doencasRetornadas.id_doenca);
    }

    //Atualização
    [Fact]
    public async Task Update_Doencas_RetornaOk()
    {
        // Arrange
        var id_doenca = 1;
        var cuidador = new Cuidador
        {
            Id_cuidador = 1,
            Nm_cuidador = " teste",
            Cpf_cuidador = "12345678900",
            Ds_email = "teste@email.com",
            Ds_senha = "senha",
            Id_perfil = 1
        };
        var idoso = new Idoso
        {
            ID_idoso = 1,
            Nm_idoso = " teste",
            cpf_idoso = "12345678900",
            vlr_altura = 1.7m,
            vlr_peso = 70m,
            dt_nascimento = new DateOnly(1950, 1, 1),
            ds_comportamento = "Descrição teste",
            id_cuidador = 1,
            cuidador = cuidador
        };
        var doencas = new Doencas
        {
            id_doenca = id_doenca,
            Nm_doenca = " atualizada",
            Ds_doenca = "Descrição atualizada",
            Id_idoso = 1,
            Idoso = idoso
        };

        _doencas_ServiceMock.Setup(service => service.UpdateDoencasAsync(id_doenca, doencas)).ReturnsAsync(doencas);

        //Act
        var atualizacaoRealizada = await _controller.PutDoencas(id_doenca, doencas);

        //Assert
        var okResultado = Assert.IsType<OkObjectResult>(atualizacaoRealizada.Result);
        var retornoDoencas = Assert.IsType<Doencas>(okResultado.Value);
        //Dados atualizados
        Assert.Equal(doencas.id_doenca, retornoDoencas.id_doenca);
        Assert.Equal(doencas.Nm_doenca, retornoDoencas.Nm_doenca);
        Assert.Equal(doencas.Ds_doenca, retornoDoencas.Ds_doenca);
        Assert.Equal(doencas.Id_idoso, retornoDoencas.Id_idoso);
    }

    [Fact]
    public async Task GetAll_Doencas_RetornandoOk()
    {
        // Arrange
        var cuidador = new Cuidador
        {
            Id_cuidador = 1,
            Nm_cuidador = " teste",
            Cpf_cuidador = "12345678900",
            Ds_email = "teste@email.com",
            Ds_senha = "senha",
            Id_perfil = 1
        };
        var idoso = new Idoso
        {
            ID_idoso = 1,
            Nm_idoso = " teste",
            cpf_idoso = "12345678900",
            vlr_altura = 1.7m,
            vlr_peso = 70m,
            dt_nascimento = new DateOnly(1950, 1, 1),
            ds_comportamento = "Descrição teste",
            id_cuidador = 1,
            cuidador = cuidador
        };
        var list = new List<Doencas>
        {
            new Doencas
            {
                id_doenca = 1,
                Nm_doenca = " teste",
                Ds_doenca = "Descrição teste",
                Id_idoso = 1,
                Idoso = idoso
            }
        };

        _doencas_ServiceMock.Setup(service => service.GetAllDoencasAsync()).ReturnsAsync(list);

        // Act
        var resultado = await _controller.GetAllDoencas();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
        var returnedList = Assert.IsType<List<Doencas>>(okResult.Value);

        Assert.NotNull(returnedList);
        Assert.Single(returnedList);
        Assert.Equal(1, returnedList[0].id_doenca);
    }

    [Fact]
    public async Task GetId_Doencas_RetornandoOk()
    {
        // Arrange
        var id_doenca = 1;
        var cuidador = new Cuidador
        {
            Id_cuidador = 1,
            Nm_cuidador = " teste",
            Cpf_cuidador = "12345678900",
            Ds_email = "teste@email.com",
            Ds_senha = "senha",
            Id_perfil = 1
        };
        var idoso = new Idoso
        {
            ID_idoso = 1,
            Nm_idoso = " teste",
            cpf_idoso = "12345678900",
            vlr_altura = 1.7m,
            vlr_peso = 70m,
            dt_nascimento = new DateOnly(1950, 1, 1),
            ds_comportamento = " teste",
            id_cuidador = 1,
            cuidador = cuidador
        };
        var doencas = new Doencas
        {
            id_doenca = id_doenca,
            Nm_doenca = " teste",
            Ds_doenca = " teste",
            Id_idoso = 1,
            Idoso = idoso
        };

        _doencas_ServiceMock.Setup(service => service.GetDoencasAsync(id_doenca)).ReturnsAsync(doencas);

        // Act
        var resultado = await _controller.GetDoencas(id_doenca);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
        var returnedDoencas = Assert.IsType<Doencas>(okResult.Value);

        Assert.NotNull(returnedDoencas);
        Assert.Equal(id_doenca, returnedDoencas.id_doenca);
    }

    [Fact]
    public async Task Deleta_Doencas()
    {
        // Arrange
        var id_doenca = 1;
        var cuidador = new Cuidador
        {
            Id_cuidador = 1,
            Nm_cuidador = " teste",
            Cpf_cuidador = "12345678900",
            Ds_email = "teste@email.com",
            Ds_senha = "senha",
            Id_perfil = 1
        };
        var idoso = new Idoso
        {
            ID_idoso = 1,
            Nm_idoso = " teste",
            cpf_idoso = "12345678900",
            vlr_altura = 1.7m,
            vlr_peso = 70m,
            dt_nascimento = new DateOnly(1950, 1, 1),
            ds_comportamento = "Descrição teste",
            id_cuidador = 1,
            cuidador = cuidador
        };
        var doencas = new Doencas
        {
            id_doenca = id_doenca,
            Nm_doenca = " teste",
            Ds_doenca = " teste",
            Id_idoso = 1,
            Idoso = idoso
        };

        _doencas_ServiceMock.Setup(service => service.DeleteDoencasAsync(id_doenca))
            .ReturnsAsync(doencas);

        //Act
        var resultado = await _controller.DeleteDoencas(id_doenca);

        //Assert
        Assert.IsType<NoContentResult>(resultado);
    }
}
