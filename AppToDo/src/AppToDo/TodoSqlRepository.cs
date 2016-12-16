using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppToDo.Models;
using AppToDo.Interfaces;
using AppToDo.Models;
using AppToDo;


namespace AppToDo
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null) throw new ArgumentNullException();
            if (_context.TodoItems.Any(p => p.Id.Equals(todoItem.Id)))
                throw new DuplicateTodoItemException();

            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            var item = _context.TodoItems.FirstOrDefault(p => p.Id.Equals(todoId));
            if (item == null) return null;

            if (!item.UserId.Equals(userId))
                throw new FieldAccessException();

            return item;
        }

        public List<TodoItem> GetActive(Guid userId)
        {
          return _context.TodoItems.Where(p => p.UserId.Equals(userId) && !p.IsCompleted).OrderByDescending(p => p.DateCreated).ToList();
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(p => p.UserId.Equals(userId)).OrderByDescending(p => p.DateCreated).ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _context.TodoItems.Where(p => p.UserId.Equals(userId) && p.IsCompleted).OrderByDescending(p => p.DateCreated).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _context.TodoItems.Where(filterFunction)
                .Where(p => p.UserId.Equals(userId))
                .OrderByDescending(p => p.DateCreated).ToList();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            var item = Get(todoId, userId);
            if (item == null) return false;
            item.MarkAsCompleted();
            Update(item, userId);
            return true;
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            var item = Get(todoId, userId);
            if (item == null) return false;
            _context.TodoItems.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (todoItem == null) throw new NullReferenceException();

            var item = Get(todoItem.Id, userId);
            if (item == null)
            {
                Add(todoItem);
            }
            else
            {
                item.Text = todoItem.Text;
                item.IsCompleted = todoItem.IsCompleted;
                item.DateCreated = todoItem.DateCreated;
                item.DateCompleted = todoItem.DateCompleted;
                _context.SaveChanges();
            }

        }
    }
}
