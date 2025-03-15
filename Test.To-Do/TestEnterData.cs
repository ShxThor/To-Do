using NSubstitute;
using To_Do.Classes;
using To_Do.InterFaces;
namespace Test.To_Do;

[TestFixture]
public class TestEnterData
{
   
    
    // Test EnterToDo Method
    
    [Test]
    public void TestStringReturnOfEnterToDo()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("Hello World");
        string expected = "Hello World";
        
        Assert.That(enterData.EnterToDo(), Is.EqualTo(expected));
    }

    [Test]
    public void TestEnterToDoMaxStringErrorMessage()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("123456789123456789123456789123456789123456789123456789123456789123456789123456789"," ");
        string expected = "To-Do is bigger than 75 Characters";
        
        enterData.EnterToDo();
        
        writer.Received().WriteLine(expected);
    }

    [Test]
    public void TestEnterToDoNullStringErrorMessage()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns(null, " ");
        string expected = "error!";

        enterData.EnterToDo();
        
        writer.Received().WriteLine(expected);
    }
    
    
    // Test EnterImportance Method
    
    [Test]
    public void TestBoolTrueReturnOfEnterImportance()
    { 
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader,writer);
        reader.ReadLine().Returns("y");
        
        Assert.That(enterData.EnterImportance(), Is.True);
    }

    [Test]
    public void TestBoolFalseReturnOfEnterImportance()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader,writer);
        reader.ReadLine().Returns("n");
        
        Assert.That(enterData.EnterImportance(), Is.False);
    }

    [Test]
    public void TestEnterImportanceErrorMessage()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("false", "y");
        string expected = "error!";

        enterData.EnterImportance();
        
        writer.Received().WriteLine(expected);
    }
    

    // Test EnterDate Method
    
    [Test]
    public void TestDateTimeReturnOfEnterDate()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader,writer);
        reader.ReadLine().Returns("12.12.2012");
        DateTime expected = new DateTime(2012,12,12);
        
        Assert.That(enterData.EnterDate(), Is.EqualTo(expected));
    }
    
    [Test]
    public void TestDateTimeReturnOfInputDayAndMonthOnly()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader,writer);
        reader.ReadLine().Returns("12.01");
        DateTime expected = new DateTime(2025,01,12);
        
        Assert.That(enterData.EnterDate(), Is.EqualTo(expected)); 
    }

    [Test]
    public void TestDateTimeReturnCurrentDate()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader,writer);
        reader.ReadLine().Returns("t");
        DateTime expected = DateTime.Today.Date;
        
        Assert.That(enterData.EnterDate(), Is.EqualTo(expected)); 
    }
    
    [Test]
    public void TestDateTimeMessageIfDateIsRight()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader,writer);
        reader.ReadLine().Returns("12.12.2012");
        string expected = "You entered: 12.12.2012";

        enterData.EnterDate();
        
        writer.Received().WriteLine(expected);
    }

    [Test]
    public void TestDateTimeErrorMessageInputString()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("letters","12.12.2012");
        string expected = "error!";
        enterData.EnterDate();
        
        writer.Received().WriteLine(expected);
    }

    [Test]
    public void TestDateTimeErrorMessageInputDateWithoutSeparation()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("12122012","12.12.2012");
        string expected = "error!";
        enterData.EnterDate();
        
        writer.Received().WriteLine(expected);
    }

    [Test]
    public void TestDateTimeErrorMessageInputNull()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("","12.12.2012");
        string expected = "error!";
        enterData.EnterDate();
        
        writer.Received().WriteLine(expected);
    }
    
    
    // Test EnterTime Method
    
    [Test]
    public void TestTimeOnlyReturnOfEnterTime()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("12:12");
        TimeOnly expected = new TimeOnly(12, 12);
        
        Assert.That(enterData.EnterTime(),Is.EqualTo(expected));
    }

    [Test]
    public void TestTimeOnlyErrorMessageInputTimeWithoutSeparation()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("1212","12:12");
        string expected = "error!";
        
        enterData.EnterTime();
        
        writer.Received().WriteLine(expected);
    }

    [Test]
    public void TestTimeOnlyErrorMessageInputNull()
    {
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns(null,"12:12");
        string expected = "error!";
        
        enterData.EnterTime();
        
        writer.Received().WriteLine(expected);
    }

    
    // Test ChooseToDo Method
    
    [Test]
    public void TestIntReturnOfChooseToDo()
    {
        List<ToDo> toDos = new List<ToDo>()
        {
            new ToDo(false,true,"test")
        };
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("0");
        int expected = 0;
        
        Assert.That(enterData.ChooseToDo(toDos), Is.EqualTo(expected));
    }

    [Test]
    public void TestChooseToDoErrorMessageIfToDoIsOutOfRange()
    {
        List<ToDo> toDos = new List<ToDo>()
        {
            new ToDo(false,true,"test")
        };
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("2","0");
        string expected = "Does not exist!";

        enterData.ChooseToDo(toDos);
        
        writer.Received().WriteLine(expected);
    }
    
    
    // Test ChooseToDoCollector Method

    [Test]
    public void TestChooseToDoCollectorReturnTuple()
    {
        List<ToDo> toDos = new List<ToDo>()
        {
            new ToDoCollector(false, false, "Collection", new List<ToDo>())
        };
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("0");
        (ToDoCollector? toDoCollector, int number) = enterData.ChooseToDoCollector(toDos);
        
        Assert.That(toDoCollector, Is.EqualTo(toDos[0]));
        Assert.That(number,Is.EqualTo(0));
    }

    [Test]
    public void TestChooseToDoCollectorOutOfRangeItem()
    {
        List<ToDo> toDos = new List<ToDo>()
        {
            new ToDoCollector(false, false, "Collection", new List<ToDo>())
        };
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("1","0");
        enterData.ChooseToDoCollector(toDos);
        string expected = "Nr.1 is not a Collection";
        
        writer.Received().WriteLine(expected);
    }

    [Test]
    public void TestChooseToDoCollectionListIsWithoutCollectionReturn()
    {
        List<ToDo> toDos = new List<ToDo>()
        {
            new ToDo(false,false,"toDo")
        };
        IConsoleReader reader = Substitute.For<IConsoleReader>();
        IConsoleWriter writer = Substitute.For<IConsoleWriter>();
        EnterData enterData = new EnterData(reader, writer);
        reader.ReadLine().Returns("0");
        (ToDoCollector? toDoCollector, int number) = enterData.ChooseToDoCollector(toDos);
        
        Assert.That(toDoCollector, Is.EqualTo(null));
        Assert.That(number,Is.EqualTo(0));
    }
}