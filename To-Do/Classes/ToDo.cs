using System.Text.Json.Serialization;

namespace To_Do.Classes;

[JsonDerivedType(typeof(ToDo),typeDiscriminator:"ToDo")]
[JsonDerivedType(typeof(DeadlineToDo),typeDiscriminator:"DeadlineToDo")]
[JsonDerivedType(typeof(ToDoCollector),typeDiscriminator:"ToDoCollector")]
public class ToDo
{
    public ToDo(bool finished,bool important ,string text)
    {
        Finished = finished;
        Important = important;
        Text = text;
    }
   
    public bool Finished { get; set; }
    public bool Important { get; set; }
    public string Text { get; set; }

    public virtual string ConvertToString(int number)
    {
        Console.ForegroundColor = GetColor();
        string line = $"Nr.{number} | {Text.PadRight(75)}|{"|".PadLeft(25)}";
        return line;
    }
    public virtual ConsoleColor GetColor()
    {
        ConsoleColor color;
        if (Finished)
        {
            color = ConsoleColor.DarkGreen;
        }
        else if (Important)
        {
            color = ConsoleColor.DarkYellow;
        }
        else
        {
            color = ConsoleColor.Black;
        }

        return color;
    }
}