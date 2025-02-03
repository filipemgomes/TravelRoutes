using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class GetBestRouteUseCase
    {
        private readonly IRouteRepository _repository;

        public GetBestRouteUseCase(IRouteRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public string Execute(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                throw new ArgumentException("Origem e destino não podem ser vazios.");
            }

            var routes = _repository.GetAllRoutes();
            var bestRoute = FindBestRoute(origin.ToUpper(), destination.ToUpper(), routes, new List<Route>(), 0, int.MaxValue, new HashSet<string>(), out decimal bestCost);

            return bestRoute != null
                ? $"Melhor Rota: {string.Join(" - ", bestRoute.Select(r => r.Origin).Append(destination))} ao custo de ${bestCost}"
                : "Nenhuma rota encontrada.";
        }

        private List<Route>? FindBestRoute(
            string current,
            string destination,
            List<Route> allRoutes,
            List<Route> path,
            decimal currentCost,
            decimal bestCost,
            HashSet<string> visited,
            out decimal finalCost)
        {
            finalCost = bestCost;
            if (current == destination)
            {
                finalCost = currentCost;
                return new List<Route>(path);
            }

            if (visited.Contains(current))
            {
                return null;
            }

            visited.Add(current);
            var candidates = allRoutes.Where(r => r.Origin == current && !path.Contains(r)).ToList();
            List<Route>? bestPath = null;

            foreach (var route in candidates)
            {
                var newPath = new List<Route>(path) { route };
                var totalCost = currentCost + route.Cost;

                if (totalCost >= bestCost)
                    continue;

                var possiblePath = FindBestRoute(route.Destination, destination, allRoutes, newPath, totalCost, bestCost, new HashSet<string>(visited), out decimal newCost);
                if (possiblePath != null && newCost < bestCost)
                {
                    bestPath = possiblePath;
                    bestCost = newCost;
                }
            }

            finalCost = bestCost;
            return bestPath;
        }
    }
}
