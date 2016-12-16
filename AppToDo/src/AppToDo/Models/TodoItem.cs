using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace AppToDo.Models
{



    public class TodoItem
    {

        //public Guid Id { get; set; }
        public Guid Id { get; set; }


        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }

        public TodoItem()
        {
            
        }

        
        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid(); 
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now;
            UserId = userId;
        }


        public void MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                DateCompleted = DateTime.Now;
            }

        }
    
        public override bool Equals(object obj)
        {
            var newObj = obj as TodoItem;

            if (newObj == null)
            {
                return false;
            }
            else
            {
                return Id.Equals(newObj.Id);
            }
            
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
