using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace UnitTests
{
    public class ContextTests
    {
        //private readonly DbContextOptions<TodoContext> _contextOptions;
        private readonly TodoContext _context;
        private TodoItem _item { get; set; }

        public ContextTests()
        {
            //    _contextOptions = new DbContextOptionsBuilder<TodoContext>()
            //.UseInMemoryDatabase("TodoTests")
            //.Options;

            _context = new TodoContext(
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("TodoTests")
                .Options);

            _item = new TodoItem
            {
                Id = 1,
                IsComplete = true,
                Name = "FakeName"
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //using var context = new TodoContext(_contextOptions);
            _context.Add(_item);
            _context.SaveChanges();
        }

        //_context.Entry(todoItem).State = EntityState.Modified;
        //await _context.TodoItems.ToListAsync()
        //await _context.SaveChangesAsync();
        //await _context.TodoItems.FindAsync(id);


        //_context.TodoItems.Add(todoItem);
        //_context.SaveChanges();
        [Test]
        public void AddSaveItemToContext()
        {
            var item = _context.TodoItems.First<TodoItem>();

            Assert.Multiple(() =>
            {
                Assert.That(_item.Id, Is.EqualTo(item.Id));
                Assert.That(_item.Name, Is.EqualTo(item.Name));
                Assert.That(_item.IsComplete, Is.EqualTo(item.IsComplete));
            });
        }

        //_context.TodoItems.Count() == 0
        //_context.TodoItems.Remove(todoItem);
        [TestCase()]
        public void RemoveFromContext(int id = 123)
        {
            var item = new TodoItem
            {
                Id = id,
                IsComplete = true,
                Name = "Test123"
            };

            _context.Add(item);
            _context.SaveChanges();

            _context.Remove(item);
            _context.SaveChanges();

            Assert.True(_context.TodoItems.Any(e => e.Id == id));
        }

        //_context.TodoItems.Any(e => e.Id == id);
        [Test]
        public void GetAnyFromContext()
        {
            var item = _context.TodoItems.Any(e => e.Id == _item.Id);

            Assert.IsTrue(item);
        }


        //_context.Entry(todoItem).State = EntityState.Modified;
        //await _context.TodoItems.ToListAsync()
        //await _context.SaveChangesAsync();
        //await _context.TodoItems.FindAsync(id);

        //have to implement IDbAsyncQueryProvider
        [Test]
        public async Task TodoItemsToListAsync()
        {
            var count = _context.TodoItems.Count();
            var items = _context.TodoItems;
            var itemsList = await items.ToListAsync();
            var item = itemsList.First();

            Assert.Multiple(() =>
            {
                Assert.That(_item.Id, Is.EqualTo(item.Id));
                Assert.That(_item.Name, Is.EqualTo(item.Name));
                Assert.That(_item.IsComplete, Is.EqualTo(item.IsComplete));
            });            
        }
    }
}