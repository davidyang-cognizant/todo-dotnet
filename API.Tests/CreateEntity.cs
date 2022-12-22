using System.Threading;

namespace API.Tests;
using API.Entities;

[TestClass]
public class CreateEntity
{
    private Todo _todo;

    public CreateEntity()
    {
        _todo = new Todo();
    }

    [TestMethod]
    public void ValidID()
    {
        _todo.Id = 1;
        Assert.AreEqual(_todo.Id, 1);
    }
    
    [TestMethod]
    public void ValidTask()
    {
        _todo.Task = "Go on a walk";
        Assert.AreEqual(_todo.Task, "Go on a walk");
    }
}