using To_Do.InterFaces;

namespace To_Do.Classes;

public class FilterList : IFilterList
{

    public List<ToDo> FilterDeadline(List<ToDo> toDos)
    {
        List<ToDo> filteredToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (toDo is DeadlineToDo)
            {
                filteredToDos.Add(toDo);
            }
        }
        return filteredToDos;
    }

    public List<ToDo> FilterNoDeadline(List<ToDo> toDos)
    {
        List<ToDo> filteredToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (toDo is not DeadlineToDo && toDo is not ToDoCollector)
            {
                filteredToDos.Add(toDo);
            }
        }
        return filteredToDos;
    }

    public List<ToDo> FilterToDoCollection(List<ToDo> toDos)
    {
        List<ToDo> filteredToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (toDo is ToDoCollector)
            {
                filteredToDos.Add(toDo);
            }
        }

        return filteredToDos;
    }

    public List<ToDo> FilterImportant(List<ToDo> toDos)
    {
        List<ToDo> filteredToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (toDo.Important)
            {
                filteredToDos.Add(toDo);
            }
        }
        return filteredToDos;
    }

    public List<ToDo> FilterNormal(List<ToDo> toDos)
    {
        List<ToDo> filteredToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (!toDo.Important)
            {
                filteredToDos.Add(toDo);
            }
        }
        return filteredToDos;
    }

    public List<ToDo> FilterFinished(List<ToDo> toDos)
    {
        List<ToDo> filteredToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (toDo.Finished)
            {
                filteredToDos.Add(toDo);
            }
        }
        return filteredToDos;
    }

    public List<ToDo> FilterOverDue(List<ToDo> toDos)
    {
        List<ToDo> filteredToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (toDo is DeadlineToDo deadlineToDo)
            {
                if (deadlineToDo.Date < DateTime.Now)
                {
                    filteredToDos.Add(toDo);
                }
            }
        }

        return filteredToDos;

    }

    public List<ToDo> FilterDueSoon(List<ToDo> toDos)
    {
        List<ToDo> filteredToDos = new List<ToDo>();
        foreach (var toDo in toDos)
        {
            if (toDo is DeadlineToDo deadlineToDo)
            {
                if (deadlineToDo.Time > TimeOnly.Parse(DateTime.Now.ToShortTimeString()) && deadlineToDo.Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    filteredToDos.Add(toDo);
                }
            }
        }
        return filteredToDos;
    }
}