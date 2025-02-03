
using System.Text.Json;

namespace InfraStructure.Persistence
{
    public static class DataInitializer
    {
        private static readonly string DataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static readonly string FilePath = Path.Combine(DataDirectory, "routes.json");

        public static bool Initialize()
        {
            try
            {
                if (!Directory.Exists(DataDirectory))
                    Directory.CreateDirectory(DataDirectory);

                if (!File.Exists(FilePath))
                {
                    var initialRoutes = new[]
                    {
                        new { origin = "GRU", destination = "BRC", cost = 10 },
                        new { origin = "BRC", destination = "SCL", cost = 5 },
                        new { origin = "GRU", destination = "CDG", cost = 75 },
                        new { origin = "GRU", destination = "SCL", cost = 20 },
                        new { origin = "GRU", destination = "ORL", cost = 56 },
                        new { origin = "ORL", destination = "CDG", cost = 5 },
                        new { origin = "SCL", destination = "ORL", cost = 20 }
                    };

                    foreach (var route in initialRoutes)
                    {
                        if (string.IsNullOrWhiteSpace(route.origin) || string.IsNullOrWhiteSpace(route.destination))
                        {
                            throw new Exception("O arquivo de rotas contém entradas inválidas.");
                        }
                    }

                    var json = JsonSerializer.Serialize(initialRoutes, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(FilePath, json);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inicializar o arquivo de rotas: {ex.Message}", ex);
            }
        }

        public static string GetFilePath() => FilePath;
    }

}