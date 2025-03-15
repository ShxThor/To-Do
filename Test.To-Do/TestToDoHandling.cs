using NSubstitute;
using To_Do.Classes;
using To_Do.InterFaces;
namespace Test.To_Do;

[TestFixture]
public class TestToDoHandling
{
    private List<ToDo> ToDos { get; set; }
    private IConsoleWriter Writer { get; set; }
    private IConsoleReader Reader { get; set; }
    private IEnterData EnterData { get; set; }

    [SetUp]
    public void SetUp()
    {
        ToDos = new List<ToDo>();
        Writer = Substitute.For<IConsoleWriter>();
        Reader = Substitute.For<IConsoleReader>();
        EnterData = Substitute.For<IEnterData>();
    }

    [TearDown]
    public void TearDown()
    {
        ToDos.Clear();
        Writer = null;
        Reader= null;
        EnterData = null;
    }

    // Test NewSingleToDo Method
    
    [Test]
    public void TestNewToDoReturnedNormalToDo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        EnterData.EnterToDo().Returns("Test");
        EnterData.EnterImportance().Returns(false);
        Reader.ReadLine().Returns("n");

        ToDo newToDo = handling.NewSingleToDo();
        
        Assert.That(newToDo.Finished, Is.False);
        Assert.That(newToDo.Important, Is.False);
        Assert.That(newToDo.Text, Is.EqualTo("Test"));
    }

    [Test]
    public void TestNewToDoReturnedDeadlineToDo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        EnterData.EnterToDo().Returns("Test");
        EnterData.EnterImportance().Returns(false);
        Reader.ReadLine().Returns("y");
        EnterData.EnterDate().Returns(new DateTime(2012,12,12).Date);
        EnterData.EnterTime().Returns(new TimeOnly(12,12,00));

        DeadlineToDo newDeadlineToDo = (DeadlineToDo) handling.NewSingleToDo();
        
        Assert.That(newDeadlineToDo.Finished, Is.False);
        Assert.That(newDeadlineToDo.Important, Is.False);
        Assert.That(newDeadlineToDo.Text, Is.EqualTo("Test"));
        Assert.That(newDeadlineToDo.Date, Is.EqualTo(new DateTime(2012,12,12).Date));
        Assert.That(newDeadlineToDo.Time, Is.EqualTo(new TimeOnly(12,12,00)));
    }
    
    
    // Test NewCollectorToDo Method

    [Test]
    public void TestNewCollectorToDoReturnedToDoCollector()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        EnterData.EnterToDo().Returns("Test");
        EnterData.EnterImportance().Returns(false);
        Reader.ReadLine().Returns("n");

        ToDoCollector newCollector = (ToDoCollector) handling.NewCollectorToDo();
        
        Assert.That(newCollector.Finished , Is.False);
        Assert.That(newCollector.Important , Is.False);
        Assert.That(newCollector.Text , Is.EqualTo("Test"));
        Assert.That(newCollector.CollectedToDos[0] , Is.TypeOf<ToDo>());
        Assert.That(newCollector.CollectedToDos[0].Finished, Is.False);
        Assert.That(newCollector.CollectedToDos[0].Important, Is.False);
        Assert.That(newCollector.CollectedToDos[0].Text, Is.EqualTo("Test"));
    }
    
    
    // Test EditToDo Method

    [Test]
    public void TestEditToDoAddDeadlineToToDo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        ToDos.Add(new ToDo(false,false,"Test"));
        EnterData.ChooseToDo(ToDos).Returns(0);
        Reader.ReadLine().Returns("d","n");
        EnterData.EnterDate().Returns(new DateTime(2012,12,12).Date);
        EnterData.EnterTime().Returns(new TimeOnly(12,12,00));
        
        handling.EditToDo(ToDos);
        Assert.That(ToDos[0], Is.InstanceOf<DeadlineToDo>());

        DeadlineToDo deadlineToDo = (DeadlineToDo) ToDos[0];
        
        Assert.That(deadlineToDo.Finished, Is.False);
        Assert.That(deadlineToDo.Important, Is.False);
        Assert.That(deadlineToDo.Text, Is.EqualTo("Test"));
        Assert.That(deadlineToDo.Date , Is.EqualTo(new DateTime(2012,12,12).Date));
        Assert.That(deadlineToDo.Time, Is.EqualTo(new TimeOnly(12,12,00)));
    }

    [Test]
    public void TestEditToDoRemoveDeadlineFromToDo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        ToDos.Add(new DeadlineToDo(false,false,"Test",new DateTime(2012,12,12).Date,new TimeOnly(12,12,00)));
        EnterData.ChooseToDo(ToDos).Returns(0);
        Reader.ReadLine().Returns("d", "y", "n");
        
        handling.EditToDo(ToDos);
        Assert.That(ToDos[0], Is.InstanceOf<ToDo>());
        
        ToDo toDo = ToDos[0];
        
        Assert.That(toDo.Finished, Is.False);
        Assert.That(toDo.Important, Is.False);
        Assert.That(toDo.Text, Is.EqualTo("Test"));
    }

    [Test]
    public void TestEditToDoChangeFromNormalToImportantToDo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        ToDos.Add(new ToDo(false,false,"Test"));
        EnterData.ChooseToDo(ToDos).Returns(0);
        Reader.ReadLine().Returns("i", "n");
        EnterData.EnterImportance().Returns(true);
        
        handling.EditToDo(ToDos);
        
        Assert.That(ToDos[0].Important, Is.True);
    }

    [Test]
    public void TestEditToDoChangeTextOfToDo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        ToDos.Add(new ToDo(false,false,"FalseText"));
        EnterData.ChooseToDo(ToDos).Returns(0);
        Reader.ReadLine().Returns("t", "n");
        EnterData.EnterToDo().Returns("RightText");
        string expected = "RightText";
        
        handling.EditToDo(ToDos);
        
        Assert.That(ToDos[0].Text, Is.EqualTo(expected));
    }

    [Test]
    public void TestEditToDoErrorMessage()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        ToDos.Add(new ToDo(false,false,"FalseText"));
        EnterData.ChooseToDo(ToDos).Returns(0);

        Reader.ReadLine().Returns("false", "t", "n");

        EnterData.EnterToDo().Returns("nothing");
        string expected = "error!";
        handling.EditToDo(ToDos);
        
        Writer.Received().WriteLine(expected);
    }


    // Test DeleteToDo Method

    [Test]
    public void TestDeleteToDo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        ToDos.Add(new ToDo(false,false,"FalseText"));
        EnterData.ChooseToDo(ToDos).Returns(0);
        Reader.ReadLine().Returns("y");
        
        handling.DeleteToDo(ToDos);
        
        Assert.That(ToDos , Is.Empty);
    }


    // Test FinishToDo Method

    [Test]
    public void TestFinishUnfinishedToDo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        ToDos.Add(new ToDo(false,false,"FalseText"));
        EnterData.ChooseToDo(ToDos).Returns(0);
        
        handling.FinishToDo(ToDos);
        Assert.That(ToDos[0].Finished , Is.True);
    }

    [Test]
    public void TestUnfinishFinishedToDo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        ToDos.Add(new ToDo(true,false,"FalseText"));
        EnterData.ChooseToDo(ToDos).Returns(0);
        
        handling.FinishToDo(ToDos);
        Assert.That(ToDos[0].Finished , Is.False);
    }


    // Test YesNoChoice Method

    [Test]
    public void TestYesNoChoiceInputYes()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        Reader.ReadLine().Returns("y");
        
        Assert.That(handling.YesNoChoice(""), Is.True);
    }

    [Test]
    public void TestYesNoChoiceInputNo()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        Reader.ReadLine().Returns("n");
        
        Assert.That(handling.YesNoChoice(""), Is.False);
    }

    [Test]
    public void TestYesNoChoiceFaultyInput()
    {
        ToDoHandling handling = new ToDoHandling(EnterData, Reader,Writer);
        Reader.ReadLine().Returns("wrong","y");
        handling.YesNoChoice( "");
        
        Writer.Received().WriteLine("error!");
    }
}