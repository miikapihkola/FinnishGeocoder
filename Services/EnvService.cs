using System;
using System.Collections.Generic;
using System.Text;

namespace FinnishGeocoder.Services
{
    public class EnvService
    {
        private const string EnvFileName = ".env";
        private const string ApiKeyName = "DIGITRANSIT_API_KEY";

        private readonly string _envFilePath;

        public EnvService()
        {
            // Always look for .env next to the executable
            _envFilePath = Path.Combine(AppContext.BaseDirectory, EnvFileName);
        }

        public string LoadOrPromptApiKey()
        {
            var key = TryReadKeyFromFile();
            if (!string.IsNullOrWhiteSpace(key))
            {
                return key;
            }

            return PromptAndSaveKey();
        }

        private string? TryReadKeyFromFile()
        {
            if (!File.Exists(_envFilePath))
            {
                return null; 
            }
            foreach (var line in File.ReadLines(_envFilePath))
            {
                // Skip empty lines and comments
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                {
                    continue;
                }

                var parts = line.Split('=', 2);
                if (parts.Length == 2 && parts[0].Trim() == ApiKeyName)
                {
                    var value = parts[1].Trim();
                    return string.IsNullOrWhiteSpace(value) ? null : value;
                }
            }
            return null;
        }

        private string PromptAndSaveKey()
        {
            Console.WriteLine();
            Console.WriteLine("Digitransit API key not found.");
            Console.WriteLine($"You can register and obtain a key at: https://portal-api.digitransit.fi/");
            Console.WriteLine();

            string? key = null;
            while (string.IsNullOrWhiteSpace(key))
            {
                Console.Write("Enter your Digitransit API key: ");
                key = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(key))
                {
                    Console.WriteLine("API key cannot be empty, please try again.");
                }
            }

            SaveKeyToFile(key);
            Console.WriteLine($"API key saved to {EnvFileName}.");
            Console.WriteLine();

            return key;
        }

        private void SaveKeyToFile(string key)
        {
            var lines = new[]
            {
                "# Digitransit API key",
                "# Obtain yours at: https://portal-api.digitransit.fi/",
                $"{ApiKeyName}={key}"
            };

            File.WriteAllLines(_envFilePath, lines);
        }
    }
}
