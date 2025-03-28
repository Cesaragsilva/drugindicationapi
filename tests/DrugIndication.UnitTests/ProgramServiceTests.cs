using DrugIndication.Application.Interfaces.Repositories;
using DrugIndication.Application.Services;
using DrugIndication.Domain.Constants;
using DrugIndication.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DrugIndication.UnitTests
{
    public class ProgramServiceTests
    {
        private readonly Mock<IProgramRepository> _repositoryMock;
        private readonly ProgramService _service;

        public ProgramServiceTests()
        {
            _repositoryMock = new Mock<IProgramRepository>();
            _service = new ProgramService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetProgramByIdAsync_ShouldReturnProgram_WhenExists()
        {
            var program = new Program { Id = "1", ProgramName = "Test" };
            _repositoryMock.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(program);

            var result = await _service.GetProgramByIdAsync("1", CancellationToken.None);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(program);
        }

        [Fact]
        public async Task GetProgramByIdAsync_ShouldFail_WhenNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync("99", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(value: null);

            var result = await _service.GetProgramByIdAsync("99", CancellationToken.None);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task GetAllProgramAsync_ShouldReturnAll()
        {
            var list = new List<Program>
            {
                new() { Id = "1", ProgramName = "Test1" },
                new() { Id = "2", ProgramName = "Test2" }
            };

            _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(list);

            var result = await _service.GetAllProgramAsync(CancellationToken.None);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task CreateProgramAsync_ShouldFail_WhenProgramAlreadyExists()
        {
            var program = new Program { Id = "1", ProgramName = "Test" };
            _repositoryMock.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(program);

            var result = await _service.CreateProgramAsync(program, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task CreateProgramAsync_ShouldCreate_WhenNotExists()
        {
            var program = new Program { Id = "2", ProgramName = "New Program" };
            _repositoryMock.Setup(r => r.GetByIdAsync("2", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(value: null); 

            var result = await _service.CreateProgramAsync(program, CancellationToken.None);

            _repositoryMock.Verify(r => r.CreateAsync(program, It.IsAny<CancellationToken>()), Times.Once);
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(program);
        }

        [Fact]
        public async Task UpdateProgramAsync_ShouldUpdate_WhenFound()
        {
            var updated = new Program { Id = "1", ProgramName = "Updated" };
            _repositoryMock.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new Program { Id = "1", ProgramName = "Old" });

            var result = await _service.UpdateProgramAsync("1", updated, CancellationToken.None);

            _repositoryMock.Verify(r => r.UpdateAsync(updated, It.IsAny<CancellationToken>()), Times.Once);
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateProgramAsync_ShouldFail_WhenNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync("404", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(value: null);

            var result = await _service.UpdateProgramAsync("404", new Program(), CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteProgramAsync_ShouldDelete_WhenExists()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new Program { Id = "1" });

            var result = await _service.DeleteProgramAsync("1", CancellationToken.None);

            _repositoryMock.Verify(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()), Times.Once);
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteProgramAsync_ShouldFail_WhenNotFound()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync("404", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(value: null);

            var result = await _service.DeleteProgramAsync("404", CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
        }
    }
}
