using To_Do.Classes;

namespace To_Do.InterFaces;

public interface ISortToDos
{
    List<ToDo> SortByAlphabet(List<ToDo> toDos);
    List<ToDo> SortByImportance(List<ToDo> toDos);
    List<ToDo> SortByDate(List<ToDo> toDos);
    List<ToDo> SortByFinished(List<ToDo> toDos);
    List<ToDo> SortByCollection(List<ToDo> toDos);
}