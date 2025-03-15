using To_Do.InterFaces;

namespace To_Do.Classes;

public class ToDoHandling : IToDoHandling
{
    public ToDoHandling(IEnterData enterData, IConsoleReader reader, IConsoleWriter writer)
    {
        EnterData = enterData;
        Reader = reader;
        Writer = writer;
    }
    public IEnterData EnterData { get; set; }
    public IConsoleReader Reader { get; set; }
    public IConsoleWriter Writer { get; set; }
     public ToDo NewSingleToDo()
    {
        string text = EnterData.EnterToDo();
        bool important = EnterData.EnterImportance();

        TimeOnly time;
        DateTime date;
        string message = $"Do you want to add a Date and Time ?";
        bool choice = YesNoChoice(message);
        if (choice)
        {
            date = EnterData.EnterDate();
            time = EnterData.EnterTime();
            ToDo deadlineToDo = new DeadlineToDo(false, important, text, date, time);
            return deadlineToDo; 
        }

        ToDo toDo = new ToDo(false, important, text);
        
        return toDo;        
    }

    public ToDo NewCollectorToDo()
    {
        string text = EnterData.EnterToDo();
        bool important = EnterData.EnterImportance();
        List<ToDo> toDos = new List<ToDo>();
        bool valid;
        do
        {
            Writer.WriteLine("You now have to add your Collection of entrys");
            ToDo toDo = NewSingleToDo();
            toDos.Add(toDo);
            string message = "Do you want to add another one ?";
            valid = YesNoChoice(message);

        } while (valid);

        return new ToDoCollector(false, important, text, toDos);
    }

    public void EditToDo(List<ToDo> toDos)
    {
        Writer.ClearConsole();
        Writer.PrintList(toDos,"Filter Suppressed",true);
       int number = EnterData.ChooseToDo(toDos);
       bool valid = false;
       
       Writer.ClearConsole();
       Writer.WriteLine($"You chose:\n");
       do
       {
           Writer.PrintToDo(toDos[number],number);
           Writer.WriteLine("\nD = Deadline | T = text | I = importance");
           string line = Reader.ReadLine().ToLower();
           if (line == "d")
           {
               if (toDos[number] is not DeadlineToDo)
               {
                   DateTime date = EnterData.EnterDate();
                   TimeOnly time = EnterData.EnterTime();
                   DeadlineToDo iToDo = new DeadlineToDo(toDos[number].Finished,toDos[number].Important ,toDos[number].Text, date, time);
                   toDos.RemoveAt(number);
                   toDos.Insert(number,iToDo);
                   
               }
               else if (toDos[number] is DeadlineToDo)
               {
                   string text = "Do you want to delete the Date ?";
                   bool delete = YesNoChoice(text);
                   if (delete)
                   {
                       ToDo toDo = new ToDo(toDos[number].Finished, toDos[number].Important, toDos[number].Text);
                       toDos.RemoveAt(number);
                       toDos.Insert(number,toDo);
                   }
               }
           }
           else if (line == "t")
           {
               toDos[number].Text = EnterData.EnterToDo();
           }
           else if (line == "i")
           {
               toDos[number].Important = EnterData.EnterImportance();
           }
           else
           {
               Writer.WriteLine("error!");
               continue;
           }
           Writer.ClearConsole();
           Writer.PrintToDo(toDos[number],number);
           
           string message = "\nDo you want to make any other Changes";
           bool choice = YesNoChoice(message);
           if (!choice)
           {
               valid = true;
           }

       } while (!valid);
    }
    
    public void DeleteToDo(List<ToDo> toDos)
    {
        Writer.ClearConsole();
        Writer.PrintList(toDos,"Filter Suppressed",true);
        int number = EnterData.ChooseToDo(toDos);
        string message = $"Are you shure you want to delete:\n{toDos[number].ConvertToString(number)}";
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        bool choice = YesNoChoice(message);
        if (choice)
        {
            toDos.RemoveAt(number);
        }
    }

    public void FinishToDo(List<ToDo> toDos)
    {
        Writer.ClearConsole();
        Writer.PrintList(toDos,"Filter Suppressed",true);
        int number = EnterData.ChooseToDo(toDos);
        if (!toDos[number].Finished)
        {
            toDos[number].Finished = true;
        }
        else
        {
            toDos[number].Finished = false;
        }
    }
    public bool YesNoChoice(string message)
    {
        bool valid = false;
        bool chocie = false;
        do
        {
            Writer.ClearConsole();
            Writer.WriteLine(message);
            Writer.WriteLine("Y = Yes | N = No");
            string line = Reader.ReadLine().ToLower();
            if (line == "y")
            {
                chocie = true;
                valid = true;
            }
            else if (line == "n")
            {
                valid = true;
            }
            else
            {
                Writer.WriteLine("error!");
            }
        } while (!valid);

        return chocie;
    }
}