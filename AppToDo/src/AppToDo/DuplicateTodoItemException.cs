using System;

namespace AppToDo
{
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException() : base("Item with the same ID already exist!")
        {
        }

    }
}