using Application.UseCases;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace UnitTests.Application.UseCases
{
    public class GetBestRouteUseCaseTests
    {
        [Fact]
        public void Should_Return_Best_Route()
        {
            // Arrange
            var mockRepo = new Mock<IRouteRepository>();
            mockRepo.Setup(repo => repo.GetAllRoutes()).Returns(new List<Route>
            {
                new Route("GRU", "BRC", 10),
                new Route("BRC", "SCL", 5),
                new Route("SCL", "ORL", 20),
                new Route("ORL", "CDG", 5),
                new Route("GRU", "CDG", 75)
            });

            var useCase = new GetBestRouteUseCase(mockRepo.Object);

            // Act
            var result = useCase.Execute("GRU", "CDG");

            // Assert
            Assert.Equal("Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40", result);
        }

        [Fact]
        public void Should_Return_No_Route_Found()
        {
            // Arrange
            var mockRepo = new Mock<IRouteRepository>();
            mockRepo.Setup(repo => repo.GetAllRoutes()).Returns(new List<Route>());

            var useCase = new GetBestRouteUseCase(mockRepo.Object);

            // Act
            var result = useCase.Execute("GRU", "XYZ");

            // Assert
            Assert.Equal("Nenhuma rota encontrada.", result);
        }

        [Fact]
        public void Should_Throw_Exception_For_Empty_Origin_Or_Destination()
        {
            // Arrange
            var mockRepo = new Mock<IRouteRepository>();
            var useCase = new GetBestRouteUseCase(mockRepo.Object);

            // Act - Assert
            var exception1 = Assert.Throws<ArgumentException>(() => useCase.Execute("", "CDG"));
            Assert.Equal("Origem e destino não podem ser vazios.", exception1.Message);

            var exception2 = Assert.Throws<ArgumentException>(() => useCase.Execute("GRU", ""));
            Assert.Equal("Origem e destino não podem ser vazios.", exception2.Message);
        }
    }
}
