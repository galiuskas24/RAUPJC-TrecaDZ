using System;
using System.Collections.Generic;
using AppToDo.Models;

namespace AppToDo.Interfaces 
{
    public interface ITodoRepository 
    {
        /// <summary>
        /// Gets TodoItem for a given id
        /// </summary>
        /// <returns>TodoItem if found, null otherwise</returns>
        TodoItem Get(Guid todoId, Guid userId);

        /// <summary>
        /// Adds new TodoItem object in database.
        /// If object with the same id already exists,
        /// method should throw DuplicateTodoItemException with the message "duplicate id: {id}". /// </summary> 
        void Add(TodoItem todoItem);
        
        /// <summary>
        /// Tries to remove a TodoItem with given id from the database.
        /// </summary>
        /// <returns>True if success, false otherwise</returns> 
        bool Remove(Guid todoId, Guid userId);

        /// <summary>
        /// Updates given TodoItem in database.
        /// If TodoItem does not exist, method will add one.
        /// </summary>
        void Update(TodoItem todoItem, Guid userId);
        
        /// <summary>
        /// Tries to mark a TodoItem as completed in database.
        /// </summary>
        /// <returns>True if success, false otherwise</returns>
         bool MarkAsCompleted(Guid todoId, Guid userId);
        
        /// <summary>
        /// Gets all TodoItem objects in database, sorted by date created (descending)
        /// </summary>
        List<TodoItem> GetAll(Guid userId);

        /// <summary>
        /// Gets all incomplete TodoItem objects in database
        /// </summary>
        List<TodoItem> GetActive(Guid userId);

        /// <summary>
        /// Gets all completed TodoItem objects in database
        /// </summary>
        List<TodoItem> GetCompleted(Guid userId);

        /// <summary>
        /// Gets all TodoItem objects in database that apply to the filter
        /// </summary>
        List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId);
    }
}

