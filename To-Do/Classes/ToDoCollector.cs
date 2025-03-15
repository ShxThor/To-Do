namespace To_Do.Classes;

public class ToDoCollector : ToDo
{
    public ToDoCollector(bool finished,bool important ,string text, List<ToDo> collectedToDos)
        :base (finished,important,text)
    {
        CollectedToDos = collectedToDos;
    }
    public List<ToDo> CollectedToDos { get; set; }
    
    public override string ConvertToString(int number)
    {
        Console.ForegroundColor = GetColor();
        string line = $"Nr.{number} | {Text.PadRight(75)}|{"       Collection".PadRight(24)}|";
        return line;
    }
}
