using To_Do.Classes;

namespace Test.To_Do;

[TestFixture]
public class TestSortToDos
{
    private List<ToDo> ToDos { get; set; }

    
    [SetUp]
    public void SetUp()
    {
        ToDos = new List<ToDo>()
        {
            new ToDo(false,false,"F"),
            new ToDo(true,true,"E"),
            new ToDo(false,true,"D"),
            new ToDo(true,false,"C"),
            new DeadlineToDo(false,false,"B Future",new DateTime(9999,1,1).Date,new TimeOnly(12,12,00)),
            new DeadlineToDo(true,true,"A Past",new DateTime(2012,12,12).Date,new TimeOnly(12,12,00)),
        };
    }

    [TearDown]
    public void TearDown()
    {
        ToDos.Clear();
    }
    
    
    // Test SortByAlphabet Method

    [Test]
    public void TestSortByAlphabetReturnToDoList()
    {
        SortToDos sort = new SortToDos();
        List<ToDo> sortedToDos = sort.SortByAlphabet(ToDos);

        Assert.That(sortedToDos[0], Is.EqualTo(ToDos[5]));
        Assert.That(sortedToDos[1], Is.EqualTo(ToDos[4]));
        Assert.That(sortedToDos[2], Is.EqualTo(ToDos[3]));
        Assert.That(sortedToDos[3], Is.EqualTo(ToDos[2]));
        Assert.That(sortedToDos[4], Is.EqualTo(ToDos[1]));
        Assert.That(sortedToDos[5], Is.EqualTo(ToDos[0]));
    }
    
    // Test SortByImportance Method

    [Test]
    public void TestSortByImportanceReturnToDoList()
    {
        SortToDos sort = new SortToDos();
        List<ToDo> sortedToDos = sort.SortByImportance(ToDos);

        Assert.That(sortedToDos[0], Is.EqualTo(ToDos[1]));
        Assert.That(sortedToDos[1], Is.EqualTo(ToDos[2]));
        Assert.That(sortedToDos[2], Is.EqualTo(ToDos[5]));
        Assert.That(sortedToDos[3], Is.EqualTo(ToDos[0]));
        Assert.That(sortedToDos[4], Is.EqualTo(ToDos[3]));
        Assert.That(sortedToDos[5], Is.EqualTo(ToDos[4]));
    }
    
    // Test SortByDate Method

    [Test]
    public void TestSortByDateReturnToDoList()
    {
        SortToDos sort = new SortToDos();
        List<ToDo> sortedToDos = sort.SortByDate(ToDos);

        Assert.That(sortedToDos[0], Is.EqualTo(ToDos[4]));
        Assert.That(sortedToDos[1], Is.EqualTo(ToDos[5]));
        Assert.That(sortedToDos[2], Is.EqualTo(ToDos[0]));
        Assert.That(sortedToDos[3], Is.EqualTo(ToDos[1]));
        Assert.That(sortedToDos[4], Is.EqualTo(ToDos[2]));
        Assert.That(sortedToDos[5], Is.EqualTo(ToDos[3]));
    }
    
    // Test SortByFinished Method

    [Test]
    public void TestSortByFinishedReturnToDoList()
    {
        SortToDos sort = new SortToDos();
        List<ToDo> sortedToDos = sort.SortByFinished(ToDos);

        Assert.That(sortedToDos[0], Is.EqualTo(ToDos[1]));
        Assert.That(sortedToDos[1], Is.EqualTo(ToDos[3]));
        Assert.That(sortedToDos[2], Is.EqualTo(ToDos[5]));
        Assert.That(sortedToDos[3], Is.EqualTo(ToDos[0]));
        Assert.That(sortedToDos[4], Is.EqualTo(ToDos[2]));
        Assert.That(sortedToDos[5], Is.EqualTo(ToDos[4]));
    }

    [Test]
    public void TestSortByCollectionReturnToDoList()
    {
        SortToDos sort = new SortToDos();
        ToDos.Add(new ToDoCollector(false,false,"text", new List<ToDo>()));
        List<ToDo> sortedToDos = sort.SortByCollection(ToDos);
        
        Assert.That(sortedToDos[0], Is.EqualTo(ToDos[6]));
        Assert.That(sortedToDos[1], Is.EqualTo(ToDos[0]));
        Assert.That(sortedToDos[2], Is.EqualTo(ToDos[1]));
        Assert.That(sortedToDos[3], Is.EqualTo(ToDos[2]));
        Assert.That(sortedToDos[4], Is.EqualTo(ToDos[3]));
        Assert.That(sortedToDos[5], Is.EqualTo(ToDos[4]));
        Assert.That(sortedToDos[6], Is.EqualTo(ToDos[5]));
    }
}