using Ajudante_Familiar_Global.Controllers;
using Ajudante_Familiar_Global.Domain.Entities;
using Ajudante_Familiar_Global.Domain.Enums;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Ajudante_Familiar_Global.Infrastruture.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ajudante_Familiar_Global.UnitTests.Controllers;

public class AlimentacaoControllerTests
{
    private readonly Mock<IAlimentacaoService> _alimentacao_ServiceMock;
    private readonly AlimentacaoController _controller;
    private readonly ILogger<AlimentacaoController> _logger;
    private readonly AppDbContext _context;

    public  AlimentacaoControllerTests()
    {
        _alimentacao_ServiceMock = new Mock<IAlimentacaoService>();
        var loggMock = new Mock<ILogger<AlimentacaoController>>();

        _logger = loggMock.Object;

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);

        _controller = new AlimentacaoController(_context, _alimentacao_ServiceMock.Object, _logger);

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
    public async Task Create_Alimentacao_RetornaCreated()
    {
        // Arrange
        var alimentacao = new Alimentacao
        {
            Id_alimentacao = 1,
            Nm_alimentacao = "teste",
            Tp_alimentacao = TipoAlimentacao.almoco,
            Dt_consumo = new DateOnly(2026, 8, 9),
            Hr_consumo = new TimeOnly(12, 0),
            Vlr_caloria = 450m,
            Ds_preparacao = "teste",
            Id_idoso = 1
        };

        _alimentacao_ServiceMock.Setup(service => service.CreateAlimentacaoAsync(alimentacao))
            .ReturnsAsync(alimentacao);

        // Act
        var resultado = await _controller.PostAlimentacao(alimentacao);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(resultado.Result);
        var alimentacaoRetornada = Assert.IsType<Alimentacao>(createdResult.Value);
        Assert.NotNull(alimentacaoRetornada);
        Assert.Equal(alimentacao.Id_alimentacao, alimentacaoRetornada.Id_alimentacao);
    }

    //Atualização
    [Fact]
    public async Task Update_Alimentacao_RetornaOk()
    {
        // Arrange
        var id_alimentacao = 1;
        var alimentacao = new Alimentacao
        {
            Id_alimentacao = id_alimentacao,
            Nm_alimentacao = " atualizada",
            Tp_alimentacao = TipoAlimentacao.jantar,
            Dt_consumo = new DateOnly(2026, 8, 10),
            Hr_consumo = new TimeOnly(19, 0),
            Vlr_caloria = 500m,
            Ds_preparacao = " atualizada",
            Id_idoso = 1
        };

        _alimentacao_ServiceMock.Setup(service => service.UpdateAlimentacaoAsync(id_alimentacao, alimentacao))
            .ReturnsAsync(alimentacao);

        //Act
        var atualizacaoRealizada = await _controller.PutAlimentacao(id_alimentacao, alimentacao);

        //Assert
        var okResultado = Assert.IsType<OkObjectResult>(atualizacaoRealizada.Result);
        var retornoAlimentacao = Assert.IsType<Alimentacao>(okResultado.Value);
        //Dados atualizados
        Assert.Equal(alimentacao.Id_alimentacao, retornoAlimentacao.Id_alimentacao);
        Assert.Equal(alimentacao.Nm_alimentacao, retornoAlimentacao.Nm_alimentacao);
        Assert.Equal(alimentacao.Tp_alimentacao, retornoAlimentacao.Tp_alimentacao);
        Assert.Equal(alimentacao.Dt_consumo, retornoAlimentacao.Dt_consumo);
        Assert.Equal(alimentacao.Hr_consumo, retornoAlimentacao.Hr_consumo);
        Assert.Equal(alimentacao.Vlr_caloria, retornoAlimentacao.Vlr_caloria);
        Assert.Equal(alimentacao.Ds_preparacao, retornoAlimentacao.Ds_preparacao);
        Assert.Equal(alimentacao.Id_idoso, retornoAlimentacao.Id_idoso);
    }

    [Fact]
    public async Task GetAll_Alimentacao_RetornandoOk()
    {
        // Arrange
        var list = new List<Alimentacao>
        {
            new Alimentacao
            {
                Id_alimentacao = 1,
                Nm_alimentacao = " teste",
                Tp_alimentacao = TipoAlimentacao.almoco,
                Dt_consumo = new DateOnly(2026, 8, 9),
                Hr_consumo = new TimeOnly(12, 0),
                Vlr_caloria = 450m,
                Ds_preparacao = " teste",
                Id_idoso = 1
            }
        };

        _alimentacao_ServiceMock.Setup(service => service.GetAllAlimentacaoAsync())
            .ReturnsAsync(list);

        // Act
        var resultado = await _controller.GetAllAlimentacao();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
        var returnedList = Assert.IsType<List<Alimentacao>>(okResult.Value);

        Assert.NotNull(returnedList);
        Assert.Single(returnedList);
        Assert.Equal(1, returnedList[0].Id_alimentacao);
    }

    [Fact]
    public async Task GetId_Alimentacao_RetornandoOk()
    {
        // Arrange
        var id_alimentacao = 1;
        var alimentacao = new Alimentacao
        {
            Id_alimentacao = id_alimentacao,
            Nm_alimentacao = "teste",
            Tp_alimentacao = TipoAlimentacao.almoco,
            Dt_consumo = new DateOnly(2026, 8, 9),
            Hr_consumo = new TimeOnly(12, 0),
            Vlr_caloria = 450m,
            Ds_preparacao = "teste",
            Id_idoso = 1
        };

        _alimentacao_ServiceMock.Setup(service => service.GetAlimentacaoAsync(id_alimentacao))
            .ReturnsAsync(alimentacao);

        // Act
        var resultado = await _controller.GetAlimentacao(id_alimentacao);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
        var returnedAlimentacao = Assert.IsType<Alimentacao>(okResult.Value);

        Assert.NotNull(returnedAlimentacao);
        Assert.Equal(id_alimentacao, returnedAlimentacao.Id_alimentacao);
    }

    [Fact]
    public async Task Deleta_Alimentacao()
    {
        // Arrange
        var id_alimentacao = 1;
        var alimentacao = new Alimentacao
        {
            Id_alimentacao = id_alimentacao,
            Nm_alimentacao = "teste",
            Tp_alimentacao = TipoAlimentacao.almoco,
            Dt_consumo = new DateOnly(2026, 8, 9),
            Hr_consumo = new TimeOnly(12, 0),
            Vlr_caloria = 450m,
            Ds_preparacao = "teste",
            Id_idoso = 1
        };

        _alimentacao_ServiceMock.Setup(service => service.DeleteAlimentacaoAsync(id_alimentacao))
            .ReturnsAsync(alimentacao);

        //Act
        var resultado = await _controller.DeleteAlimentacao(id_alimentacao);

        //Assert
        Assert.IsType<NoContentResult>(resultado);
    }
}
