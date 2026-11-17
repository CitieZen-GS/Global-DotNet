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

public class ParenteControllerTests
{
    private readonly Mock<IParenteService> _parente_ServiceMock;
    private readonly ParenteController _controller;
    private readonly ILogger<ParenteController> _logger;
    private readonly AppDbContext _context;

    public ParenteControllerTests()
    {
        _parente_ServiceMock = new Mock<IParenteService>();
        var loggMock = new Mock<ILogger<ParenteController>>();

        _logger = loggMock.Object;

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);

        _controller = new ParenteController(_parente_ServiceMock.Object, _logger);

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
    public async Task Create_Parente_RetornaCreated()
    {
        // Arrange
        var parente = new Parente
        {
            Id_parente = 1,
            Nm_parente = "Parente teste",
            Nr_telefone = "11999999999",
            Ds_email = "teste@email.com",
            Ds_senha = "senha",
            Id_idoso = 1,
            Id_perfil = 1
        };

        _parente_ServiceMock.Setup(service => service.CreateParenteAsync(parente)).ReturnsAsync(parente);

        // Act
        var resultado = await _controller.PostParente(parente);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(resultado.Result);
        var parenteRetornado = Assert.IsType<Parente>(createdResult.Value);
        Assert.NotNull(parenteRetornado);
        Assert.Equal(parente.Id_parente, parenteRetornado.Id_parente);
    }

    //Atualização
    [Fact]
    public async Task Update_Parente_RetornaOk()
    {
        // Arrange
        var id_parente = 1;
        var parente = new Parente
        {
            Id_parente = id_parente,
            Nm_parente = "Parente atualizado",
            Nr_telefone = "11999999999",
            Ds_email = "atualizado@email.com",
            Ds_senha = "senha2",
            Id_idoso = 1,
            Id_perfil = 1
        };

        _parente_ServiceMock.Setup(service => service.UpdateParenteAsync(id_parente, parente)).ReturnsAsync(parente);

        //Act
        var atualizacaoRealizada = await _controller.PutParente(id_parente, parente);

        //Assert
        var okResultado = Assert.IsType<OkObjectResult>(atualizacaoRealizada.Result);
        var retornoParente = Assert.IsType<Parente>(okResultado.Value);
        //Dados atualizados
        Assert.Equal(parente.Id_parente, retornoParente.Id_parente);
        Assert.Equal(parente.Nm_parente, retornoParente.Nm_parente);
        Assert.Equal(parente.Nr_telefone, retornoParente.Nr_telefone);
        Assert.Equal(parente.Ds_email, retornoParente.Ds_email);
        Assert.Equal(parente.Ds_senha, retornoParente.Ds_senha);
        Assert.Equal(parente.Id_idoso, retornoParente.Id_idoso);
        Assert.Equal(parente.Id_perfil, retornoParente.Id_perfil);
    }

    [Fact]
    public async Task GetAll_Parente_RetornandoOk()
    {
        // Arrange
        var list = new List<Parente>
        {
            new Parente
            {
                Id_parente = 1,
                Nm_parente = "Parente teste",
                Nr_telefone = "11999999999",
                Ds_email = "teste@email.com",
                Ds_senha = "senha",
                    Id_idoso = 1,
                    Id_perfil = 1
                }
                };

                _parente_ServiceMock.Setup(service => service.GetAllParenteAsync()).ReturnsAsync(list);

        // Act
        var resultado = await _controller.GetAllParente();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
        var returnedList = Assert.IsType<List<Parente>>(okResult.Value);

        Assert.NotNull(returnedList);
        Assert.Single(returnedList);
        Assert.Equal(1, returnedList[0].Id_parente);
    }

    [Fact]
    public async Task GetId_Parente_RetornandoOk()
    {
        // Arrange
        var id_parente = 1;
        var parente = new Parente { Id_parente = id_parente, Nm_parente = "Parente teste", Id_idoso = 1, Id_perfil = 1 };

        _parente_ServiceMock.Setup(service => service.GetParenteAsync(id_parente)).ReturnsAsync(parente);

        // Act
        var resultado = await _controller.GetParente(id_parente);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
        var returnedParente = Assert.IsType<Parente>(okResult.Value);

        Assert.NotNull(returnedParente);
        Assert.Equal(id_parente, returnedParente.Id_parente);
    }

    [Fact]
    public async Task Deleta_Parente()
    {
        // Arrange
        var id_parente = 1;

        _parente_ServiceMock.Setup(service => service.DeleteParenteAsync(id_parente)).ReturnsAsync(new Parente { Id_parente = id_parente });

        //Act
        var resultado = await _controller.DeleteParente(id_parente);

        //Assert
        Assert.IsType<NoContentResult>(resultado);
    }
}
