namespace WorkoutGenerator.Models;

public class CompletionRequestMessage
{
    public string model => "gpt-4o";

    public object response_format => new
    {
        type = "json_object"
    };
    public List<CompletionsRequestMessageItem> messages { get; set; }
    
    public CompletionRequestMessage()
    {
        messages = new List<CompletionsRequestMessageItem>();
    }

    public void AddSystemMessage(string content)
    {
        AddMessage("system", content);
    }
    
    public void AddUserMessage(string content)
    {
        AddMessage("user", content);
    }

    private void AddMessage(string role, string content)
    {
        
        messages.Add(new CompletionsRequestMessageItem()
        {
            role = role,
            content = content
        });
    }
}

public class CompletionsRequestMessageItem
{
    public required string role { get; set; }
    public required string content { get; set; }
}
