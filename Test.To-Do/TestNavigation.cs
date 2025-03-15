using NSubstitute;
using To_Do.Classes;
using To_Do.InterFaces;
namespace Test.To_Do;

[TestFixture]
public class TestNavigation
{
    private List<ToDo> ToDos { get; set; }
    private IJsonAcces Acces { get; set; }
    private IConsoleWriter Writer { get; set; }
    private IConsoleReader Reader { get; set; }
    private IToDoHandling Handling { get; set; }
    private IFilterList FilterList { get; set; }
    private ISortToDos SortToDos { get; set; }
    private IEnterData EnterData { get; set; }
    
    [SetUp]
    public void SetUp()
    {
        ToDos = new List<ToDo>();
        Acces = Substitute.For<IJsonAcces>();
        Writer = Substitute.For<IConsoleWriter>();
        Reader = Substitute.For<IConsoleReader>();
        Handling = Substitute.For<IToDoHandling>();
        FilterList = Substitute.For<IFilterList>();
        SortToDos = Substitute.For<ISortToDos>();
        EnterData = Substitute.For<IEnterData>();
    }

    [TearDown]
    public void TearDown()
    {
        ToDos.Clear();
        Acces = null;
        Writer = null;
        Reader = null;
        Handling = null;
        FilterList = null;
        SortToDos = null;
        EnterData = null;
    }
    
    //Test ChooseAction Method
    
    [Test]
    public async Task TestChooseActionNewToDo()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("n","s","q");
        await navigation.ChooseAction(Acces,"");

        Handling.Received().NewSingleToDo();
    }
    
    [Test]
    public async Task TestChooseActionDeleteToDo()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("d","q");
        await navigation.ChooseAction(Acces,"");
        
        Handling.Received().DeleteToDo(ToDos);
    }

    [Test]
    public async Task TestChooseActionEditToDo()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("e","q");
        await navigation.ChooseAction(Acces,"");
        
        Handling.Received().EditToDo(ToDos);
    }

    [Test]
    public async Task TestChooseActionFinishToDo()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("f","q");
        await navigation.ChooseAction(Acces,"");
        
        Handling.Received().FinishToDo(ToDos);
    }
    
    [Test]
    public async Task TestChooseActionErrorMessageIfInputIsFaulty()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("faulty","q");
        await navigation.ChooseAction(Acces,"");
        string expected = "error!";
        
        Writer.Received().WriteLine(expected);
    }
    
    
    // Test NavigateToDoCollection Method 

    [Test]
    public async Task TestNavigateToDoCollectionNewToDo()
    {
        ToDos.Add(new ToDoCollector(false,false,"t",new List<ToDo>(){new ToDo(false,false,"test")}));
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        EnterData.ChooseToDoCollector(ToDos).Returns((new ToDoCollector(false,false,"t",new List<ToDo>(){new ToDo(false,false,"test")}), 0));
        
        Reader.ReadLine().Returns("n","b");
        await navigation.NavigateToDoCollection();

        Handling.Received().NewSingleToDo();
    }
    
    [Test]
    public async Task TestNavigateToDoCollectionEditToDo()
    {
        ToDos.Add(new ToDoCollector(false,false,"t",new List<ToDo>(){new ToDo(false,false,"test")}));
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        EnterData.ChooseToDoCollector(ToDos).Returns((new ToDoCollector(false,false,"t",new List<ToDo>(){new ToDo(false,false,"test")}), 0));
       
        Reader.ReadLine().Returns("e","b");
        await navigation.NavigateToDoCollection();

        Handling.Received().EditToDo(Arg.Any<List<ToDo>>());
    }

    [Test]
    public async Task TestNavigateToDoCollectionDeleteToDo()
    {
        ToDos.Add(new ToDoCollector(false,false,"test",new List<ToDo>(){new ToDo(false,false,"test")}));
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        EnterData.ChooseToDoCollector(ToDos).Returns((new ToDoCollector(false,false,"test",new List<ToDo>(){new ToDo(false,false,"test")}), 0));
       
        Reader.ReadLine().Returns("d","b");
        await navigation.NavigateToDoCollection();

        Handling.Received().DeleteToDo(Arg.Any<List<ToDo>>());
    }

    [Test]
    public async Task TestNavigateToDoCollectionFinishToDo()
    {
        ToDos.Add(new ToDoCollector(false,false,"t",new List<ToDo>(){new ToDo(false,false,"test")}));
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        EnterData.ChooseToDoCollector(ToDos).Returns((new ToDoCollector(false,false,"t",new List<ToDo>(){new ToDo(false,false,"test")}), 0));
        
        Reader.ReadLine().Returns("f","b");
        await navigation.NavigateToDoCollection();

        Handling.Received().FinishToDo(Arg.Any<List<ToDo>>());    
    }

    [Test]
    public async Task TestNavigateToDoCollectionErrorMessage()
    {
        ToDos.Add(new ToDoCollector(false,false,"t",new List<ToDo>(){new ToDo(false,false,"test")}));
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        EnterData.ChooseToDoCollector(ToDos).Returns((new ToDoCollector(false,false,"t",new List<ToDo>(){new ToDo(false,false,"test")}), 0));
        
        Reader.ReadLine().Returns("False","b");
        await navigation.NavigateToDoCollection();
        string expected = "error!";
        
        Writer.Received().WriteLine(expected);
    }
    
    
    // Test ChooseType Method

    [Test]
    public void TestChooseTypeEnteredSingleType()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("s");
        navigation.ChooseToDoType();
        
        Handling.Received().NewSingleToDo();
    }

    [Test]
    public void TestChooseTypeEnteredCollectorToDo()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("c");
        navigation.ChooseToDoType();
        
        Handling.Received().NewCollectorToDo();
    }

    [Test]
    public void TestChooseTypeEnteredInvalidData()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("faulty", "c");
        navigation.ChooseToDoType();
        
        string expected = "error!";
        
        Writer.Received().WriteLine(expected);
    }
    
    
    // Test FilterToDos Method

    [Test]
    public void TestFilterToDosFilterByDeadline()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("1");
        navigation.FilterToDos();
        
        FilterList.Received().FilterDeadline(ToDos);
    }

    [Test]
    public void TestFilterToDosFilterByNoDeadLine()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("2");
        navigation.FilterToDos();
        
        FilterList.Received().FilterNoDeadline(ToDos);
    }

    [Test]
    public void TestFilterToDosFilterByCollection()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("3");
        navigation.FilterToDos();
        
        FilterList.Received().FilterToDoCollection(ToDos);
    }

    [Test]
    public void TestFilterToDosFilteredImportant()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("4");
        navigation.FilterToDos();
        
        FilterList.Received().FilterImportant(ToDos);
    }

    [Test]
    public void TestFilterToDosFilterNormal()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("5");
        navigation.FilterToDos();
        
        FilterList.Received().FilterNormal(ToDos);
    }

    [Test]
    public void TestFilterToDosFilterFinished()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("6");
        navigation.FilterToDos();
        
        FilterList.Received().FilterFinished(ToDos);
    }

    [Test]
    public void TestFilterToDosFilterOverDue()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("7");
        navigation.FilterToDos();
        
        FilterList.Received().FilterOverDue(ToDos);
    }

    [Test]
    public void TestFilterToDosFilterDueSoon()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("8");
        navigation.FilterToDos();
        
        FilterList.Received().FilterDueSoon(ToDos);
    }

    [Test]
    public void TestFilterToDoDosErrorMessageIfInputIsFaulty()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("faulty","1");
        string expected = "error!";
        navigation.FilterToDos();
        
        Writer.Received().WriteLine(expected);
    }
    
    
    // Test ChooseArrangement Method

    [Test]
    public void TestChooseArrangementRearrangeInAlphabeticalOrder()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("a");
        navigation.ChooseArrangement();

        SortToDos.Received().SortByAlphabet(ToDos);
    }
    [Test]
    public void TestChooseArrangementRearrangeInImportantOrder()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("i");
        navigation.ChooseArrangement();

        SortToDos.Received().SortByImportance(ToDos);
    }
    [Test]
    public void TestChooseArrangementRearrangeByDateOrder()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("d");
        navigation.ChooseArrangement();

        SortToDos.Received().SortByDate(ToDos);
    }
    [Test]
    public void TestChooseArrangementRearrangeInFinishedOrder()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("f");
        navigation.ChooseArrangement();

        SortToDos.Received().SortByFinished(ToDos);
    }

    [Test]
    public void TestChooseArrangementRearrangeByCollection()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Reader.ReadLine().Returns("c");
        navigation.ChooseArrangement();

        SortToDos.Received().SortByCollection(ToDos);
    }
    
    
    // Test RolledOutCollection Method

    [Test]
    public void TestRollOutCollectionConvertTrueToFalse()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Assert.That(navigation.RolledOutCollection(true), Is.False);
    }

    [Test]
    public void TestRollOutCollectionConvertFalseToTrue()
    {
        Navigation navigation = new Navigation(Writer,Reader,Handling,FilterList,SortToDos,EnterData,ToDos);
        Assert.That(navigation.RolledOutCollection(false), Is.True);
    }
}