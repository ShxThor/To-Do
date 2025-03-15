using To_Do.InterFaces;

namespace To_Do.Classes;

public class Navigation : INaviagtion
{
    public Navigation(IConsoleWriter writer, IConsoleReader reader, IToDoHandling handling, IFilterList filterList, ISortToDos sortToDos, IEnterData enterData, List<ToDo> toDos)
    {
        Writer = writer;
        Reader = reader;
        Handling = handling;
        FilterList = filterList;
        SortToDos = sortToDos;
        EnterData = enterData;
        ToDos = toDos;
    }

    private IConsoleWriter Writer { get; }
    private IConsoleReader Reader { get; }
    private IToDoHandling Handling { get; }
    private IFilterList FilterList { get; }
    private ISortToDos SortToDos { get; }
    private IEnterData EnterData { get; }
    public List<ToDo> ToDos { get; set; }
    public async Task ChooseAction(IJsonAcces json,string filePath)
    {
        List<ToDo> filterdToDOs = ToDos;
        string filter = "No Filter";
        bool validChoice = false;
        bool toDoCollectionRollout = true;
        do
        {
            if (filter == "No Filter")
            {
                filterdToDOs = ToDos;
            }
            Writer.ClearConsole();
            Writer.PrintList(filterdToDOs,filter,toDoCollectionRollout);
            Writer.WriteLine("\nO = Open Collection | R = Roll Collections | N = New | D = Delete | E = Edit | F = Finished | C = Filter | S = Sort | Q = Quit Program");
            string line = Reader.ReadLine().ToLower();
            if (line == "o")
            {
                await NavigateToDoCollection();
            }
            else if (line == "n")
            {
               ChooseToDoType();
               await json.WriteJson(filePath, ToDos);
            }
            else if (line == "d")
            {
                Handling.DeleteToDo(ToDos);
                await json.WriteJson(filePath, ToDos);
            }
            else if (line == "e")
            { 
                Handling.EditToDo(ToDos); 
                await json.WriteJson(filePath, ToDos);
            }
            else if (line == "f")
            {
                Handling.FinishToDo(ToDos);
                await json.WriteJson(filePath, ToDos);
            }
            else if (line == "q")
            {
                await json.WriteJson(filePath, ToDos);
                validChoice = true;
            }
            else if (line ==  "c")
            {
               (filterdToDOs,filter) = FilterToDos();
            }
            else if (line == "s")
            {
                ToDos = ChooseArrangement();
                await json.WriteJson(filePath, ToDos);
            }
            else if (line == "r")
            {
                toDoCollectionRollout = RolledOutCollection(toDoCollectionRollout);
            }
            
            else 
            {
                Writer.WriteLine("error!");
                Thread.Sleep(100);
                Writer.ClearConsole();
            }
        } while (!validChoice);
        
    }
    public async Task  NavigateToDoCollection()
    {
        (ToDoCollector? toDo, int number) = EnterData.ChooseToDoCollector(ToDos);
        if(toDo is null)
        {
            Writer.ClearConsole();
            Writer.WriteLine("Nothing there");
            await Task.Delay(2000);
        }
        else
        {
            bool valid = false;
            do
            {
                Writer.ClearConsole();
                Writer.PrintToDoCollection(toDo);
                Writer.WriteLine("\nN = Insert new ToDo | E = Edit existing ToDo | D = Delete ToDo | F = Finish Single ToDo | B = Back to main menu");
                string line = Reader.ReadLine().ToLower();
                if (line == "n")
                {
                    toDo.CollectedToDos.Add(Handling.NewSingleToDo());
                }
                else if (line == "e")
                {
                   Handling.EditToDo(toDo.CollectedToDos);
                }
                else if (line == "d")
                {
                    Handling.DeleteToDo(toDo.CollectedToDos);
                }
                else if (line == "f")
                {
                    Handling.FinishToDo(toDo.CollectedToDos);
                }
                else if (line == "b")
                {
                    valid = true;
                }
                else
                {
                    Writer.WriteLine("error!");
                }

            } while (!valid);

            ToDos[number] = toDo;
        }
    }

    public void ChooseToDoType()
    {
        bool valid = false;
        do
        {
            Writer.ClearConsole();
            Writer.WriteLine("What type of ToDo do you want to Add ?");
            Writer.WriteLine("C = Collection of ToDos | S = Single ToDo");
            string line = Reader.ReadLine().ToLower();
            if (line == "c")
            {
                ToDo toDo = Handling.NewCollectorToDo();
                ToDos.Add(toDo);
                valid = true;
            }
            else if (line == "s")
            {
                ToDo toDo = Handling.NewSingleToDo();
                ToDos.Add(toDo);
                valid = true;
            }
            else
            {
                Writer.WriteLine("error!");
            }

        } while (!valid);
    }
   
    public (List<ToDo>, string) FilterToDos()
    {
        string filter = "No Filter";
        List<ToDo> filterdToDos = new List<ToDo>();
        bool valid = false;
        do
        {
            Writer.ClearConsole();
            Writer.WriteLine("Filter by:\n0 = Clear Filter | 1 = Deadline | 2 = Normal | 3 = Collection | 4 = Important | 5 = Normal | 6 = Finished | 7 = Overdue | 8 = Due soon");
            string line = Reader.ReadLine();
            if (line == "0")
            {
                filterdToDos = ToDos;
                filter = "No Filter";
                valid = true;
            }
            else if (line == "1")
            {
               filterdToDos = FilterList.FilterDeadline(ToDos);
               filter = "by Deadline";
               valid = true;
            }
            else if (line == "2")
            {
                filterdToDos = FilterList.FilterNoDeadline(ToDos);
                filter = "by Normal";
                valid = true;
            }
            else if (line == "3")
            {
                filterdToDos = FilterList.FilterToDoCollection(ToDos);
                filter = "by Collection";
                valid = true;
            }
            else if (line == "4")
            {
                filterdToDos = FilterList.FilterImportant(ToDos);
                filter = "by Important ToDos";
                valid = true;
            }
            else if (line == "5")
            {
                filterdToDos = FilterList.FilterNormal(ToDos);
                filter = "by Normal ToDos";
                valid = true;
            }
            else if (line == "6")
            {
                filterdToDos = FilterList.FilterFinished(ToDos);
                filter = "by Finished ToDos";
                valid = true;
            }
            else if (line == "7")
            {
                filterdToDos = FilterList.FilterOverDue(ToDos);
                filter = "by Overdue ToDos";
                valid = true;
            }
            else if (line == "8")
            {
                filterdToDos = FilterList.FilterDueSoon(ToDos);
                filter = "by ToDos that are due soon";
                valid = true;
            }
            else
            {
                Writer.WriteLine("error!");
            }

        } while (!valid);

        return (filterdToDos, filter);
    }
    public List<ToDo> ChooseArrangement()
    {
        List<ToDo> sortedToDos = new List<ToDo>();
        bool valid = false;
        do
        {
            Writer.WriteLine("Sort by:");
            Writer.WriteLine("A = Alphabet | I = Importance | D = Date | F = Finished first | C = Collection");
            string line = Reader.ReadLine().ToLower();
            
            if (line == "a")
            {
                sortedToDos = SortToDos.SortByAlphabet(ToDos);
                valid = true;
            }
            else if (line == "i")
            {
                sortedToDos = SortToDos.SortByImportance(ToDos);
                valid = true;
            }
            else if (line == "d")
            {
              sortedToDos = SortToDos.SortByDate(ToDos);
              valid = true;
            }
            else if (line == "f")
            {
                sortedToDos = SortToDos.SortByFinished(ToDos);
                valid = true;
            }
            else if (line == "c")
            {
                sortedToDos = SortToDos.SortByCollection(ToDos);
                valid = true;
            }
            else
            {
                Writer.WriteLine("error!");  
            }
            
        } while (!valid);

        return sortedToDos;
    }

    public bool RolledOutCollection(bool rolled)
    {
        if (rolled)
        {
            rolled = false;
        }
        else
        {
            rolled = true;
        }

        return rolled;
    }
}