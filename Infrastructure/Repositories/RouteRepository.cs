using Domain.Entities;
using Domain.Repositories;
using InfraStructure.Persistence;
using System.Text.Json;

namespace InfraStructure.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly string _filePath;
        private List<Route> _routes;

        public RouteRepository()
        {
            _filePath = DataInitializer.GetFilePath();
            _routes = LoadRoutes();
        }

        public void AddRoute(Route route)
        {
            _routes.Add(route);
            SaveRoutes();
        }

        public List<Route> GetAllRoutes()
        {
            _routes = LoadRoutes();
            return _routes;
        }

        private List<Route> LoadRoutes()
        {
            if (!File.Exists(_filePath))
                return new List<Route>();

            try
            {
                var json = File.ReadAllText(_filePath);
                var routesDTO = JsonSerializer.Deserialize<List<RouteDTO>>(json) ?? new List<RouteDTO>();

                var validRoutes = new List<Route>();
                foreach (var routeDto in routesDTO)
                {
                    if (!string.IsNullOrWhiteSpace(routeDto.Origin) && !string.IsNullOrWhiteSpace(routeDto.Destination))
                    {
                        validRoutes.Add(new Route(routeDto.Origin, routeDto.Destination, routeDto.Cost));
                    }
                }

                return validRoutes;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Erro ao desserializar o JSON de rotas: {ex.Message}", ex);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException($"Erro ao ler o arquivo de rotas: {ex.Message}", ex);
            }
        }

        private void SaveRoutes()
        {
            try
            {
                var routesDTO = _routes.ConvertAll(r => new RouteDTO
                {
                    Origin = r.Origin,
                    Destination = r.Destination,
                    Cost = r.Cost
                });

                var json = JsonSerializer.Serialize(routesDTO, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException("Erro ao salvar as rotas no arquivo JSON.", ex);
            }
        }
    }
}
