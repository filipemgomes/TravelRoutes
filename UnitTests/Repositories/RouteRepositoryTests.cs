using InfraStructure.Persistence;
using System.Text;
using System.Text.Json;

namespace UnitTests.Repositories
{
    public class RouteRepositoryTests
    {
        [Fact]
        public void Should_Throw_Exception_When_Json_Is_Corrupted()
        {
            // Arrange
            var corruptedJson = "{Invalid Json Format}";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(corruptedJson));
            var reader = new StreamReader(stream);

            // Act - Assert
            Assert.Throws<JsonException>(() =>
            {
                var json = reader.ReadToEnd();
                JsonSerializer.Deserialize<List<RouteDTO>>(json);
            });
        }

        [Fact]
        public void Should_Load_Routes_From_Json_InMemory()
        {
            // Arrange
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var validJson = "[{\"Origin\":\"GRU\",\"Destination\":\"BRC\",\"Cost\":10}]";
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(validJson));
            
            using var reader = new StreamReader(stream);

            // Act
            var json = reader.ReadToEnd();
            var routes = JsonSerializer.Deserialize<List<RouteDTO>>(json, options);

            // Assert
            Assert.NotNull(routes);
            Assert.NotEmpty(routes);
            Assert.Single(routes);
            Assert.NotNull(routes[0].Origin);
            Assert.Equal("GRU", routes[0].Origin);
            Assert.Equal("BRC", routes[0].Destination);
            Assert.Equal(10, routes[0].Cost);
        }


    }
}
