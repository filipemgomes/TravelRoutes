using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class AddRouteUseCase
    {
        private readonly IRouteRepository _repository;

        public AddRouteUseCase(IRouteRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Execute(string origin, string destination, decimal cost)
        {
            var route = new Route(origin, destination, cost);
            _repository.AddRoute(route);
        }
    }
}
