using Moq;
using NUnit.Framework;
using TodoApi.Controllers;
using TodoApi.Models;

namespace UnitTests
{
    public class UnitTests
    {
        private readonly Mock<TodoContext> _contextMock;
        private TodoItem _item { get; set; }

        public UnitTests()
        {
            _contextMock = new Mock<TodoContext>();

            _item = new TodoItem
            {
                Id = 1,
                IsComplete = true,
                Name = "FakeName"
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetUp() {}

        [Test]
        public void GetTodoItemTest()
        {
            var mockSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<TodoItem>>();

            //error because TodoContext.DbSet is not virtual
            _contextMock.Setup(a => a.TodoItems)
                .Returns(mockSet.Object);

            //error because there is no no-argument constructor in the TodoContext
            var controller = new TodoController(_contextMock.Object);

            var result = controller.GetTodoItem();

        }
    }
}
