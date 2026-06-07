using AutoMapper;
using HRMS.Application.Modules.Departments.Commands.CreateDepartment;
using HRMS.Application.Modules.Departments.Mappings;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;
namespace HRMS.UnitTests.Application.Departments;
public class CreateDepartmentHandlerTests
{
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly Mock<IRepository<Department>> _repoMock = new();
    private readonly IMapper _mapper;

    public CreateDepartmentHandlerTests()
    {
        _uowMock.Setup(u => u.Repository<Department>()).Returns(_repoMock.Object);
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DepartmentMappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenDepartmentIsCreated()
    {
        _repoMock.Setup(r => r.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Department, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        _repoMock.Setup(r => r.AddAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _uowMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var handler = new CreateDepartmentCommandHandler(_uowMock.Object, _mapper);
        var result = await handler.Handle(new CreateDepartmentCommand("HR", "Human Resources", null, null, null), CancellationToken.None);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be("Human Resources");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenDepartmentCodeExists()
    {
        _repoMock.Setup(r => r.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Department, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var handler = new CreateDepartmentCommandHandler(_uowMock.Object, _mapper);
        var result = await handler.Handle(new CreateDepartmentCommand("HR", "Human Resources", null, null, null), CancellationToken.None);
        result.IsFailure.Should().BeTrue();
    }
}
