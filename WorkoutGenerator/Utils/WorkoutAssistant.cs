using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WorkoutGenerator.Configurations;
using WorkoutGenerator.Models;
using WorkoutGenerator.Services;

namespace WorkoutGenerator.Utils;

public class WorkoutAssistant
{
     public async Task<Content?> GenerateWorkoutPlanWithStaticPrompt()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();
        var client = new HttpClient();
        var openAIConfiguration = new OpenAIConfiguration(configuration);
        var openAIService = new OpenAIService(client, openAIConfiguration);
        var response = await openAIService.GenerateWorkoutPlanWithStaticPrompt();
        var contentRaw = response.choices[0].message.content;
        
        Console.WriteLine("---------Response from OpenAI in Workout Assistant---------");
        Console.WriteLine("Raw content: "+contentRaw);
        Console.WriteLine("-----------------------------------------------------------");

        var filePath = @"test.txt";
        File.WriteAllText("filePath",   contentRaw);
        try
        {
      
            var content = JsonSerializer.Deserialize<Content>(contentRaw);
            return content;
        }
        catch (Exception e)
        {
            // Console.WriteLine("Error deserializing content.");
            // Console.WriteLine($"You tried to deserialize the following content: {contentRaw} ");
            Console.WriteLine(e);
            
        }

        return null;
    }
    public async Task GenerateWorkoutPlan()
    {
         var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

        var client = new HttpClient();
        var openAIConfiguration = new OpenAIConfiguration(configuration);
        var openAIService = new OpenAIService(client, openAIConfiguration);

        var response = await openAIService.GenerateWorkoutPlan();
        var contentRaw = response.choices[0].message.content;
        Console.WriteLine(contentRaw);

        try
        {
            var content = JsonSerializer.Deserialize<Content>(contentRaw);
            Console.WriteLine($"content: {content.exercises.Count()} ");
            foreach (var exercise in content.exercises)
            {
        
                Console.WriteLine($"Name: {exercise.name}");
                Console.WriteLine($"Description: {exercise.howToPerform}");
                Console.WriteLine($"Muscle group: {exercise.muscleGroup}");
                Console.WriteLine($"Repetitions: {exercise.repetitions}");
                Console.WriteLine($"Type of weights: {exercise.weightType}");
                Console.WriteLine($"Kilograms: {exercise.kilograms}");
                Console.WriteLine();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deserializing content.");
            Console.WriteLine($"You tried to deserialize the following content: {contentRaw} ");
            Console.WriteLine(e);
        }
        
    }
}