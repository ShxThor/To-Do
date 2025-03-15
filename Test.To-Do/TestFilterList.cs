using To_Do.Classes;

namespace Test.To_Do;

[TestFixture]
public class TestFilterList
{
    private List<ToDo> ToDos { get; set; }

    
    [SetUp]
    public void SetUp()
    {
        ToDos = new List<ToDo>()
        {
            new ToDo(false,false,"False False"),
            new ToDo(true,true,"True True"),
            new ToDo(false,true,"False True"),
            new ToDo(true,false,"True False"),
            new DeadlineToDo(false,false,"Deadline Future",new DateTime(9999,1,1).Date,new TimeOnly(12,12,00)),
            new DeadlineToDo(true,true,"Deadline Past",new DateTime(2012,12,12).Date,new TimeOnly(12,12,00)),
            new ToDoCollector(false,false,"Collection", new List<ToDo>())
        };
    }

    [TearDown]
    public void TearDown()
    {
        ToDos.Clear();
    }
    
    // Test FilerDeadline Method

    [Test]
    public void TestFilterDeadlineReturnFilteredToDos()
    {
        FilterList filter = new FilterList();
        List<ToDo> filteredToDos = filter.FilterDeadline(ToDos);
        
        Assert.That(filteredToDos.OfType<DeadlineToDo>().Count(), Is.EqualTo(2));

        DeadlineToDo deadlineToDo = (DeadlineToDo) filteredToDos[0];
        Assert.That(deadlineToDo, Is.EqualTo(ToDos[4]));
    }
    
    // Test FilterNoDeadline Method

    [Test]
    public void TestFilterNoDeadlineReturnFilteredToDos()
    {
        FilterList filter = new FilterList();
        List<ToDo> filteredToDos = filter.FilterNoDeadline(ToDos);
        
        Assert.That(filteredToDos.OfType<ToDo>().Count(), Is.EqualTo(4));

        ToDo normalToDo = filteredToDos[0];
        Assert.That(normalToDo, Is.EqualTo(ToDos[0]));
    }
    
    // Test FilterCollection Method

    [Test]
    public void TestFilterToDoCollectionReturnFilteredToDoCollection()
    {
        FilterList filter = new FilterList();
        List<ToDo> filteredToDos = filter.FilterToDoCollection(ToDos);
        
        Assert.That(filteredToDos.OfType<ToDoCollector>().Count(), Is.EqualTo(1));

        ToDo collectionToDo = filteredToDos[0];
        Assert.That(collectionToDo, Is.EqualTo(ToDos[6]));
    }
    
    // Test FilterImportant Method

    [Test]
    public void TestFilterImportantReturnFilteredToDos()
    {
        FilterList filter = new FilterList();
        List<ToDo> filteredToDos = filter.FilterImportant(ToDos);
        
        Assert.That(filteredToDos.Count, Is.EqualTo(3));
        Assert.That(filteredToDos.All(t => t.Important));
    }
    
    // Test FilterNormal Method

    [Test]
    public void TestFilterNormalReturnFilteredToDos()
    {
        FilterList filter = new FilterList();
        List<ToDo> filteredToDos = filter.FilterNormal(ToDos);
        
        Assert.That(filteredToDos.Count, Is.EqualTo(4));
        Assert.That(filteredToDos.All(t => !t.Important));
    }
    
    // Test FilterFinished Method

    [Test]
    public void TestFilterFinishedReturnFilteredToDos()
    {
        FilterList filter = new FilterList();
        List<ToDo> filteredToDos = filter.FilterFinished(ToDos);
        
        Assert.That(filteredToDos.Count, Is.EqualTo(3));
        Assert.That(filteredToDos.All(t => t.Finished));
    }
    
    // Test FilterOverDue Method

    [Test]
    public void TestFilterOverDueReturnFilteredToDos()
    {
        FilterList filter = new FilterList();
        List<ToDo> filteredToDos = filter.FilterOverDue(ToDos);
        DeadlineToDo overDueToDo = (DeadlineToDo)filteredToDos[0];
         
        Assert.That(filteredToDos.OfType<DeadlineToDo>().Count(), Is.EqualTo(1));
        Assert.That(overDueToDo.Date < DateTime.Now);
    }
    
    // TestFilterDueSoon Method
    [Test]
    public void TestFilterDueSoonReturnFilteredToDos()
    {
        FilterList filter = new FilterList();
        ToDos.Add(new DeadlineToDo(false,false,"Today",DateTime.Now.Date, new TimeOnly(23,59,59)));
        List<ToDo> filteredToDos = filter.FilterDueSoon(ToDos);
        DeadlineToDo todayToDo = (DeadlineToDo)filteredToDos[0];
        
        Assert.That(filteredToDos.OfType<DeadlineToDo>().Count(), Is.EqualTo(1));
        Assert.That(todayToDo.Date, Is.EqualTo(DateTime.Now.Date));
        Assert.That(todayToDo.Time > new TimeOnly(12,12,12));
    }
}