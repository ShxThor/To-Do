using To_Do.InterFaces;

namespace To_Do.Classes;

public class SortToDos : ISortToDos
{ 
    public List<ToDo> SortByAlphabet(List<ToDo> toDos)
    {
        List<ToDo> sortedToDos = toDos.OrderBy(t => t.Text).ToList();
        return sortedToDos;
    }

    public List<ToDo> SortByImportance(List<ToDo> toDos)
    {
        var sortedList = from toDo in toDos orderby toDo.Important descending select toDo;
        return sortedList.ToList();
    }
    
    public List<ToDo> SortByDate(List<ToDo> toDos)
    {
        List<DeadlineToDo> deadlineToDos = new List<DeadlineToDo>();
        List<ToDo> normalToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (toDo is DeadlineToDo deadlineToDo)
            {
                deadlineToDos.Add(deadlineToDo);
            }
            else
            {
                normalToDos.Add(toDo);
            }
        }

        var sortedToDos = from deadlineToDo in deadlineToDos orderby deadlineToDo.Date descending select deadlineToDo;
        List<ToDo> allToDos = new List<ToDo>();
        allToDos.AddRange(sortedToDos);
        allToDos.AddRange(normalToDos);
            
        return allToDos;
    }
    
    public List<ToDo> SortByFinished(List<ToDo> toDos)
    {
        var sortedToDos = from toDo in toDos orderby toDo.Finished descending select toDo;
        return sortedToDos.ToList();
    }
    
    public List<ToDo> SortByCollection(List<ToDo> toDos)
    {
        List<ToDoCollector> collectedToDos = new List<ToDoCollector>();
        List<ToDo> normalToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (toDo is ToDoCollector collectedToDo)
            {
                collectedToDos.Add(collectedToDo);
            }
            else
            {
                normalToDos.Add(toDo);
            }
        }

        var sortedToDos = from collectedToDo in collectedToDos orderby collectedToDo.CollectedToDos descending select collectedToDo;
        List<ToDo> allToDos = new List<ToDo>();
        allToDos.AddRange(sortedToDos);
        allToDos.AddRange(normalToDos);
            
        return allToDos;
    }
}