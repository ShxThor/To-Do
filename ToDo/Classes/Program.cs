using To_Do.InterFaces;
namespace To_Do.Classes
{
   class Program
   {
      public static async Task Main()
      {
         string filePath = "Data.json";
         if (!File.Exists(filePath))
         {
            FileStream stream = File.Create(filePath);
            stream.Close();
            await File.WriteAllTextAsync(filePath,"[]");
         }
      
         IConsoleWriter writer = new ConsoleWriter();
         ConsoleReader reader = new ConsoleReader();
         IFilterList filterList = new FilterList();
         ISortToDos sortToDos = new SortToDos();
         IEnterData enterData = new EnterData(reader,writer);
         IToDoHandling handling = new ToDoHandling(enterData,reader,writer);
         JsonAcces json = new JsonAcces();
         List<ToDo> toDos = await json.ReadJson(filePath);
         Navigation navigate = new Navigation(writer,reader,handling,filterList,sortToDos,enterData,toDos);
      
         await navigate.ChooseAction(json, filePath);
         
      }
   }
}
