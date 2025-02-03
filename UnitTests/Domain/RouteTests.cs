using Domain.Entities;
using Domain.Exceptions;
using Xunit;

namespace UnitTests.Domain
{
    public class RouteTests
    {
        [Fact]
        public void Should_Create_Valid_Route()
        {
            var route = new Route("GRU", "BRC", 10);

            Assert.Equal("GRU", route.Origin);
            Assert.Equal("BRC", route.Destination);
            Assert.Equal(10, route.Cost);
        }

        [Fact]
        public void Should_Throw_Exception_For_Empty_Origin()
        {
            var exception = Assert.Throws<DomainException>(() => new Route("", "BRC", 10));
            Assert.Equal("A origem da rota não pode ser vazia.", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_For_Empty_Destination()
        {
            var exception = Assert.Throws<DomainException>(() => new Route("GRU", "", 10));
            Assert.Equal("O destino da rota não pode ser vazio.", exception.Message);
        }

        [Fact]
        public void Should_Throw_Exception_For_Negative_Cost()
        {
            var exception = Assert.Throws<DomainException>(() => new Route("GRU", "BRC", -5));
            Assert.Equal("O custo da rota deve ser maior que zero.", exception.Message);
        }
    }
}
