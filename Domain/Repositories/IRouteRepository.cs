using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRouteRepository
    {
        void AddRoute(Route route);
        List<Route> GetAllRoutes();



    }
}
