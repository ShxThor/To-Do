using To_Do.Classes;

namespace To_Do.InterFaces;

public interface IToDoHandling
{
    ToDo NewSingleToDo();
    ToDo NewCollectorToDo();
    void EditToDo(List<ToDo> toDos);
    void DeleteToDo(List<ToDo> toDos);
    void FinishToDo(List<ToDo> toDos);
    bool YesNoChoice(string message);
}