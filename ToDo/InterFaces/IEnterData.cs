using To_Do.Classes;

namespace To_Do.InterFaces;

public interface IEnterData
{
    string EnterToDo();
    bool EnterImportance();
    DateTime EnterDate();
    TimeOnly EnterTime();
    int ChooseToDo(List<ToDo> toDos);
    (ToDoCollector?, int) ChooseToDoCollector(List<ToDo> toDos);
}