using To_Do.InterFaces;

namespace To_Do.Classes;

public class EnterData : IEnterData
{
    public EnterData(IConsoleReader reader, IConsoleWriter writer)
    {
        Reader = reader;
        Writer = writer;
    }
    public IConsoleReader Reader { get; set; }
    public IConsoleWriter Writer { get; set; }
    public string EnterToDo()
    {
        bool validText = false;
        string text;
        do
        {
            Writer.WriteLine("Please enter a To-Do");
            text = Reader.ReadLine();
            if (text == null)
            {
                Writer.WriteLine("error!");
            }
            else if (text.Length > 75)
            {
                Writer.WriteLine("To-Do is bigger than 75 Characters");
            }
            else
            {
                validText = true;
            }
            
        } while (!validText);

        return text;
    }

    public bool EnterImportance()
    {
        bool validChoice = false;
        bool important = false;
        
        do
        {
            Writer.ClearConsole();
            Writer.WriteLine("Is the To-Do important ?\nY = Yes | N = No");
            string line = Reader.ReadLine().ToLower();
            if (line is "y")
            {
                important = true;
                validChoice = true;
            }
            else if (line is "n")
            {
                validChoice = true;
            }
            else
            {
                Writer.WriteLine("error!");
            }

        } while (!validChoice);

        return important;
    }

    public DateTime EnterDate()
    {
        DateTime userDateTime;
        bool valid = false;
        do
        {
            Writer.ClearConsole();
            Writer.WriteLine("Please enter a due Date (DD,MM,YYYY) or T = Today");
            string line = Reader.ReadLine().ToLower();
            if(DateTime.TryParse(line, out userDateTime))
            {
                DateTime.TryParse(line, out userDateTime);
                Writer.WriteLine($"You entered: {userDateTime.Date:d}");
                Thread.Sleep(1000);
                valid = true;
            }
            else if (line == "t")
            {
                userDateTime = DateTime.Today.Date;
                Writer.WriteLine($"You entered: {userDateTime.Date:d}");
                Thread.Sleep(1000);
                valid = true;
            }
            else
            {
                Writer.WriteLine("error!");
            }
            
        } while (!valid);

        return userDateTime;
    }

    public TimeOnly EnterTime()
    {
        bool valid = false;
        TimeOnly time;
        do
        {
            Writer.ClearConsole();
            Writer.WriteLine("Now enter a Time (Hour,Min)");
            string line = Reader.ReadLine()+":00";
            if (TimeOnly.TryParse(line,out time))
            {
                TimeOnly.TryParse(line, out time);
                Writer.WriteLine($"You entered: {time}");
                Thread.Sleep(1000);
                valid = true;
            }
            else
            {
                Writer.WriteLine("error!");
            }
        } while (!valid);

        return time;
    }
    
    public int ChooseToDo(List<ToDo> toDos)
    {
        bool valid = false;
        int number;
        do
        {
            Writer.WriteLine("Please enter the Number, you want to Choose:\n");
            string line = Reader.ReadLine();
            
            int.TryParse(line, out number);
            if (number <= toDos.Count)
            {
                return number;
            }
            else
            {
                Writer.WriteLine("Does not exist!");
            }

        } while (!valid);

        return number;
    }

    public (ToDoCollector?, int) ChooseToDoCollector(List<ToDo> toDos)
    {
        ToDoCollector? toDoCollector = null;
        bool valid = false;
        int number = 0;
        do
        {
            if (toDos.OfType<ToDoCollector>().Any())
            {
                Writer.WriteLine("Please enter the Collection, you want to Choose:\n");
                string line = Reader.ReadLine();
                int.TryParse(line, out number);
                foreach (var toDo in toDos)
                {
                    if (toDo is ToDoCollector collector && number == toDos.IndexOf(toDo))
                    {
                        toDoCollector = collector;
                        return (toDoCollector, number);
                    }
                }

                if (toDoCollector == null)
                {
                    Writer.ClearConsole();
                    Writer.PrintList(toDos,"Filter Suppressed",true);
                    Writer.WriteLine($"Nr.{number} is not a Collection");
                }
            }
            else
            {
                valid = true;
            }
            
        } while (!valid);

        return (toDoCollector, number);
    }
}