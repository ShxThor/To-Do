using To_Do.Classes;

namespace To_Do.InterFaces;

public interface IConsoleWriter
{
    void PrintList(List<ToDo> toDos,string filter,bool rolledOut);
    void PrintToDoCollection(ToDoCollector toDoCollector);
    void PrintColorCode();
    void PrintToDo(ToDo toDo, int number);
    void WriteLine(string str);
    void Write(string str);
    void ClearConsole();
}