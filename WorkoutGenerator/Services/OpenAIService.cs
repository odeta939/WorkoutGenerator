using System.Net.Http.Headers;
using WorkoutGenerator.Configurations;
using WorkoutGenerator.Interfaces;
using WorkoutGenerator.Models;
using WorkoutGenerator.Utils;

namespace WorkoutGenerator.Services;

public class OpenAIService : IOpenAIService
{
     private readonly Utils.HttpClientWrapper _client;

    public OpenAIService(HttpClient client, OpenAIConfiguration configuration)
    {

        _client = new Utils.HttpClientWrapper(client);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.ApiToken);
        client.BaseAddress = new Uri(configuration.BaseAddress);
    }

    public async Task<CompletionResponseMessage> GenerateWorkoutPlanWithStaticPrompt()
    {
        NameTyper nameTyper = new NameTyper();
        nameTyper.Dumbbell();
        nameTyper.NameCursive();
        Console.WriteLine("How many exercises do you want?");
        var amountOfExercises =  Console.ReadLine();
        Console.WriteLine("Which body part do you want to train?");
        var bodyPart = Console.ReadLine();
        Console.WriteLine("What is your level?");
        Console.WriteLine("A: beginner");
        Console.WriteLine("B: intermediate");
        Console.WriteLine("C: advanced");
        
        var input = Console.ReadLine();
        var level = "";
        
        level = input?.ToUpper() switch
        {
            "A" => "beginner",
            "B" => "intermediate",
            "C" => "advanced",
            _ => "beginner"
        };
        
        Console.WriteLine("---- User input at OpenAIService.cs----");
        Console.WriteLine("amountOfExercises: " + amountOfExercises);
        Console.WriteLine("bodyPart: " + bodyPart);
        Console.WriteLine("level: " + level);
        Console.WriteLine("----------------------------------------");
        
        const string api = "v1/chat/completions";
        var requestBody = new CompletionRequestMessage();
        var schema = "";
        var jsonExerciseExample = "";

        foreach (string line in File.ReadLines(@"exerciseSchema.json"))
        {
            schema += line;
        }

        foreach (var line in File.ReadLines(@"jsonExerciseExample.json"))
        {
            jsonExerciseExample += line;
        }
      

        string prompt =
            "Following this schema return a result in json format: ";
   
        requestBody.AddSystemMessage(prompt + jsonExerciseExample);
      //requestBody.AddUserMessage($"Give me list of {amountOfExercises} exercises for {bodyPart} for {level} level. Give me a description for each as well as repetitions, type of weights and kilograms. If it's a body weight exercise say that kilograms is 0");
        requestBody.AddUserMessage($"Give me list of 5 exercises for back for beginner level. Give me a description for each as well as repetitions, type of weights and kilograms. If it's a body weight exercise say that kilograms is 0");

        try
        {
            var response = await _client.PostDeserializedAsync<CompletionResponseMessage>(api, requestBody);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CompletionResponseMessage> GenerateWorkoutPlan()
    {
        const string api = "v1/chat/completions";
        var requestBody = new CompletionRequestMessage();
        // TODO Add user input
        
        var filePath = @"prompt.txt";
        var prompt = "";
        
        // //----------//
        // // Console.WriteLine("How many exercises do you want?");
        // var amountOfExercises = "1";
        // // Console.WriteLine("Which body part do you want to train?");
        // var bodyPart = "my arms";
        // // Console.WriteLine("What is your level?");
        // var level = "beginner";
        // //----------//

  
        
        foreach(string line in File.ReadLines(filePath))
        {
            prompt += line;
        }
        
        
        requestBody.AddSystemMessage(prompt);
        requestBody.AddUserMessage(
            $"""
            Give me list of 3 exercises for my arms for beginner level. Give me a description for each 
            as well as repetitions, type of weights and kilograms. If it's a body weight exercise say 
            that kilograms is 0
            """);

        try
        {
            var response = await _client.PostDeserializedAsync<CompletionResponseMessage>(api, requestBody);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CompletionResponseMessage> GenerateWorkoutPlan(string systemPrompt, string userPrompt)
    {
        const string api = "v1/chat/completions";
        var requestBody = new CompletionRequestMessage();
        requestBody.AddSystemMessage(systemPrompt);
        requestBody.AddUserMessage(userPrompt);

        try
        {
            var response = await _client.PostDeserializedAsync<CompletionResponseMessage>(api, requestBody);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<string> GenerateMealPlan()
    {
        throw new NotImplementedException();
    }
    
}