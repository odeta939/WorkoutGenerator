namespace WorkoutGenerator.Models;

public class CompletionResponseMessage
{
    public string id { get; set; }
    public string _object { get; set; }
    public int created { get; set; }
    public string model { get; set; }
    public Choice[] choices { get; set; }
    public Usage usage { get; set; }
}

public class Content
{
    public IEnumerable<Exercise> exercises { get; set; }
}

public class Exercise
{
    public string name { get; set; }
    public string howToPerform { get; set; }
    public string muscleGroup { get; set; }
    public string repetitions { get; set; }
    public string weightType { get; set; }
    public string kilograms { get; set; }
    

}
public class Choice
{
    public int index { get; set; }
    public Message message { get; set; }
}

public class Message
{
    public string role { get; set; }
    public string content { get; set; }
}



public class Usage
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}