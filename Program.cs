using FinnishGeocoder.Services;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

// Load appsettings.json
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

// Resolve folders
var inputFolder = config["InputFolder"] ?? "input";
var outputFolder = config["OutputFolder"] ?? "output";

Directory.CreateDirectory(inputFolder);
Directory.CreateDirectory(outputFolder);

// Load or prompt for API key
var envService = new EnvService();
var apiKey = envService.LoadOrPromptApiKey();

// Main menu loop
while (true)
{
    Console.WriteLine("=== Finnish Address Geocoder ===");
    Console.WriteLine("1 - Step 1: Geocode addresses from CSV");
    Console.WriteLine("2 - Step 2: Calculate averages from geocoded CSV");
    Console.WriteLine("0 - Exit");
    Console.WriteLine();
    Console.Write("Choose: ");

    var choice = Console.ReadLine()?.Trim();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            await RunGeocodingStep(config, apiKey, inputFolder, outputFolder); 
            break;
        case "2":
            await RunAveragesStep(config, outputFolder);
            break;
        case "0":
            Console.WriteLine("Exitting...");
            return;
        default:
            Console.WriteLine("Invalid choice, please try again.");
            break;
    }

    Console.WriteLine();
}

// Step implementations

static async Task RunGeocodingStep(IConfiguration config, string apiKey, string inputFolder, string outputFolder)
{
    // List available CSV files in input folder
    var files = Directory.GetFiles(inputFolder, "*.csv");
    if (files.Length == 0)
    {
        Console.WriteLine($"No CSV files found in '{inputFolder}' folder.");
        return;
    }

    Console.WriteLine("Available CSV files:");
    for (int i = 0; i < files.Length; i++)
    {
        Console.WriteLine($"  {i + 1} - {Path.GetFileName(files[i])}");
    }
    Console.WriteLine();
    Console.Write("Choose file: ");
    var input = Console.ReadLine()?.Trim();

    if (!int.TryParse(input, out int fileIndex) || fileIndex < 1 || fileIndex > files.Length)
    {
        Console.WriteLine("Invalid selection.");
        return;
    }

    var selectedFile = files[fileIndex - 1];
    Console.WriteLine($"Selected: {Path.GetFileName(selectedFile)}");
    Console.WriteLine();

    // TODO wire up geocoding service

    await Task.CompletedTask;
}

static async Task RunAveragesStep(IConfiguration config, string outputFolder)
{
    // List available geocoded CSVs in output folder
    var files = Directory.GetFiles(outputFolder, "geocoded_*.csv");

    if (files.Length == 0)
    {
        Console.WriteLine($"No geocoded CSV files found in '{outputFolder}' folder.");
        return;
    }

    Console.WriteLine("Available geocoded files:");
    for (int i = 0; i < files.Length; i++)
    {
        Console.WriteLine($"  {i + 1} - {Path.GetFileName(files[i])}");
    }

    Console.WriteLine();
    Console.Write("Choose file: ");
    var input = Console.ReadLine()?.Trim();

    if (!int.TryParse(input, out int fileIndex) || fileIndex < 1 || fileIndex > files.Length)
    {
        Console.WriteLine("Invalid selection.");
        return;
    }

    var selectedFile = files[fileIndex - 1];
    Console.WriteLine($"Selected: {Path.GetFileName(selectedFile)}");
    Console.WriteLine();

    // TODO wire up average service

    await Task.CompletedTask;
}