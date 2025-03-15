using System.Text.Json;
using To_Do.InterFaces;

namespace To_Do.Classes;

public class JsonAcces : IJsonAcces
{
    public async Task WriteJson(string filePath, List<ToDo> toDos)
    {
        string json = JsonSerializer.Serialize(toDos, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }
    
    public async Task <List<ToDo>> ReadJson(string filePath)
    {
        string json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<ToDo>>(json);
    }
}