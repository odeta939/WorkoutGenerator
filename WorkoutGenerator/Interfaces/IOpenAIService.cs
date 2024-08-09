using WorkoutGenerator.Models;

namespace WorkoutGenerator.Interfaces;

public interface IOpenAIService
{
    Task<CompletionResponseMessage> GenerateWorkoutPlan();
    
    Task<CompletionResponseMessage> GenerateWorkoutPlanWithStaticPrompt();

    Task<string> GenerateMealPlan();
}