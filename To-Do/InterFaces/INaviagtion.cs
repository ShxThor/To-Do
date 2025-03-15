using To_Do.Classes;

namespace To_Do.InterFaces;

public interface INaviagtion
{
    Task ChooseAction(IJsonAcces json, string filePath);
   
    (List<ToDo>, string) FilterToDos();
    List<ToDo> ChooseArrangement();
    bool RolledOutCollection(bool rolled);

}