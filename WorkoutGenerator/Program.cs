// See https://aka.ms/new-console-template for more information

using WorkoutGenerator.Models;
using WorkoutGenerator.Utils;

WorkoutAssistant workoutAssistant = new WorkoutAssistant();

var contnet = await workoutAssistant.GenerateWorkoutPlanWithStaticPrompt();

foreach (Exercise exercise in contnet.exercises)
{

    Console.WriteLine(exercise.name);
    Console.WriteLine(exercise.repetitions);
    Console.WriteLine(exercise.weightType);
    Console.WriteLine(exercise.kilograms);
    Console.WriteLine(exercise.howToPerform);
    Console.WriteLine();
    Console.WriteLine();
}