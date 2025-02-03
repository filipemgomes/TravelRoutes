using Application.UseCases;
using ConsoleApp.Config;
using InfraStructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Domain.Exceptions;

class Program
{
    static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddApplicationServices()
            .BuildServiceProvider();

        try
        {
            bool jsonCreated = DataInitializer.Initialize();
            if (jsonCreated)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✔ Arquivo de rotas inicial criado com sucesso!");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Erro ao inicializar os dados: {ex.Message}");
            Console.ResetColor();
        }

        var addRouteUseCase = serviceProvider.GetRequiredService<AddRouteUseCase>();
        var getBestRouteUseCase = serviceProvider.GetRequiredService<GetBestRouteUseCase>();

        Console.WriteLine("🚀 Bem-vindo ao TravelRouteFinder! Escolha a rota de viagem mais barata.");

        while (true)
        {
            Console.WriteLine("\n============================");
            Console.WriteLine("  📌 Escolha uma opção:");
            Console.WriteLine("  1️⃣  Registrar uma nova rota");
            Console.WriteLine("  2️⃣  Consultar a melhor rota");
            Console.WriteLine("  3️⃣  Sair");
            Console.WriteLine("============================");
            Console.Write("👉 Sua opção: ");

            var option = Console.ReadLine();

            if (option == "3")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n👋 Obrigado por usar o TravelRouteFinder! Até mais.");
                Console.ResetColor();
                break;
            }

            try
            {
                if (option == "1")
                {
                    Console.Write("✍ Digite a nova rota no formato 'Origem,Destino,Valor' (ex: GRU,BRC,10): ");
                    var input = Console.ReadLine();
                    var parts = input?.Split(',');

                    if (parts == null || parts.Length != 3 || !decimal.TryParse(parts[2], out var cost))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Formato inválido! Digite no formato correto: Origem,Destino,Valor (ex: GRU,BRC,10)");
                        Console.ResetColor();
                        continue;
                    }

                    try
                    {
                        addRouteUseCase.Execute(parts[0].Trim(), parts[1].Trim(), cost);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("✔ Rota registrada com sucesso!");
                        Console.ResetColor();
                    }
                    catch (DomainException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"❌ Erro de domínio: {ex.Message}");
                        Console.ResetColor();
                    }
                }
                else if (option == "2")
                {
                    Console.Write("Digite a rota: ");

                    var input = Console.ReadLine();
                    var parts = input?.Split('-');

                    if (parts == null || parts.Length != 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Formato inválido! Digite no formato correto: Origem-Destino (ex: GRU-CDG)");
                        Console.ResetColor();
                        continue;
                    }

                    try
                    {
                        var result = getBestRouteUseCase.Execute(parts[0].Trim(), parts[1].Trim());
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"🛣 {result}");
                        Console.ResetColor();
                    }
                    catch (DomainException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"❌ Erro de domínio: {ex.Message}");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Opção inválida! Escolha uma das opções disponíveis.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Erro inesperado: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
