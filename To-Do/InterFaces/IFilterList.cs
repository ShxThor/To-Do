using To_Do.Classes;

namespace To_Do.InterFaces;

public interface IFilterList
{
  
    List<ToDo> FilterDeadline(List<ToDo> toDos);
    List<ToDo> FilterNoDeadline(List<ToDo> toDos);
    List<ToDo> FilterToDoCollection(List<ToDo> toDos);
    List<ToDo> FilterImportant(List<ToDo> toDos);
    List<ToDo> FilterNormal(List<ToDo> toDos);
    List<ToDo> FilterFinished(List<ToDo> toDos);
    List<ToDo> FilterOverDue(List<ToDo> toDos);
    List<ToDo> FilterDueSoon(List<ToDo> toDos);

}