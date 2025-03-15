using To_Do.InterFaces;

namespace To_Do.Classes
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void PrintList(List<ToDo> toDos,string filter, bool rolledOut)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            
            foreach (var toDo in toDos)
            {
                if (toDo is not ToDoCollector)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    WriteLine(toDo.ConvertToString(toDos.IndexOf(toDo)));
                }
                else if (toDo is ToDoCollector collector)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    WriteLine(toDo.ConvertToString(toDos.IndexOf(toDo)));
                    if (rolledOut)
                    {
                        foreach (var collected in collector.CollectedToDos)
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Write("".PadRight(5));
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            WriteLine($"| {collected.Text.PadRight(75)}|{"|".PadLeft(25)}");
                        }
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            WriteLine($"\nCurrent Filter: {filter}");
            PrintColorCode();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            WriteLine($"          Current Date: {DateTime.Now}".PadRight(53));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void PrintToDoCollection(ToDoCollector toDoCollector)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            WriteLine($"Collection Title:  {toDoCollector.Text}");
            foreach (var toDo in toDoCollector.CollectedToDos)
            {
                Console.BackgroundColor = ConsoleColor.White;
                WriteLine(toDo.ConvertToString(toDoCollector.CollectedToDos.IndexOf(toDo)));
            }
            PrintColorCode();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            WriteLine($"          Current Date: {DateTime.Now}".PadRight(53));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void PrintColorCode()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Write("| Normal |");
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Write(" Important |");
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Write(" Finished |");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Write(" Overdue |");
            Console.BackgroundColor = ConsoleColor.Magenta;
            Write(" Due today |");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        
        public void PrintToDo(ToDo toDo,int number)
        {
            Console.BackgroundColor = ConsoleColor.White;
            WriteLine(toDo.ConvertToString(number));
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }

        public void Write(string str)
        {
            Console.Write(str);
        }
        public void ClearConsole()
        {
            Console.Clear();
        }
    }
}
