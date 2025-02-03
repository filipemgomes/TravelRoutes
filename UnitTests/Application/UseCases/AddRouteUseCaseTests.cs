using Application.UseCases;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Moq;

namespace UnitTests.Application.UseCases
{
    public class AddRouteUseCaseTests
    {
        [Fact]
        public void Should_Add_Route_Correctly()
        {
            // Arrange
            var mockRepo = new Mock<IRouteRepository>();
            var useCase = new AddRouteUseCase(mockRepo.Object);

            // Act
            useCase.Execute("GRU", "BRC", 10);

            // Assert
            mockRepo.Verify(repo => repo.AddRoute(It.Is<Route>(r =>
                r.Origin == "GRU" &&
                r.Destination == "BRC" &&
                r.Cost == 10
            )), Times.Once);
        }

        [Fact]
        public void Should_Throw_Exception_When_Origin_Is_Empty()
        {
            // Arrange
            var mockRepo = new Mock<IRouteRepository>();
            var useCase = new AddRouteUseCase(mockRepo.Object);

            // Act - Assert
            var exception = Assert.Throws<DomainException>(() => useCase.Execute("", "BRC", 10));
            Assert.Equal("A origem da rota não pode ser vazia.", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Destination_Is_Empty()
        {
            // Arrange
            var mockRepo = new Mock<IRouteRepository>();
            var useCase = new AddRouteUseCase(mockRepo.Object);

            // Act - Assert
            var exception = Assert.Throws<DomainException>(() => useCase.Execute("GRU", "", 10));
            Assert.Equal("O destino da rota não pode ser vazio.", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_When_Cost_Is_Negative()
        {
            // Arrange
            var mockRepo = new Mock<IRouteRepository>();
            var useCase = new AddRouteUseCase(mockRepo.Object);

            // Act - Assert
            var exception = Assert.Throws<DomainException>(() => useCase.Execute("GRU", "BRC", -5));
            Assert.Equal("O custo da rota deve ser maior que zero.", exception.Message);
        }
    }
}
