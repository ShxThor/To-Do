using To_Do.InterFaces;

namespace To_Do.Classes;

public class ConsoleReader : IConsoleReader
{
    public string ReadLine()
    {
        return Console.ReadLine();
    }
}