using To_Do.Classes;

namespace To_Do.InterFaces;

public interface IJsonAcces
{
    Task WriteJson(string filePath, List<ToDo> toDos);
    Task<List<ToDo>> ReadJson(string filePath);
}