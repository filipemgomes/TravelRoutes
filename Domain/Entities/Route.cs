using Domain.Exceptions;

namespace Domain.Entities
{
    public class Route
    {
        public string Origin { get; private set; }
        public string Destination { get; private set; }
        public decimal Cost { get; private set; }

        private Route() { }

        public Route(string origin, string destination, decimal cost)
        {
            if (string.IsNullOrWhiteSpace(origin))
                throw new DomainException("A origem da rota não pode ser vazia.");

            if (string.IsNullOrWhiteSpace(destination))
                throw new DomainException("O destino da rota não pode ser vazio.");

            if (cost < 0)
                throw new DomainException("O custo da rota deve ser maior que zero.");

            Origin = origin.ToUpper();
            Destination = destination.ToUpper();
            Cost = cost;
        }
    }
}
