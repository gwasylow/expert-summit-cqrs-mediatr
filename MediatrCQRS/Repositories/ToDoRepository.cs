using System;
using System.Collections.Generic;
using System.Linq;
using MediatrCQRS.Interfaces;
using MediatrCQRS.ViewModels;

namespace MediatrCQRS.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private IList<ToDoViewModel> _todosDatabase = new List<ToDoViewModel>();

        public ToDoRepository()
        {
            _todosDatabase.Add(new ToDoViewModel { Id = 1, Name = "Moje todo #1", Completed = false });
            _todosDatabase.Add(new ToDoViewModel { Id = 2, Name = "Moje todo #2", Completed = false });
            _todosDatabase.Add(new ToDoViewModel { Id = 3, Name = "Moje todo #3", Completed = false });
            _todosDatabase.Add(new ToDoViewModel { Id = 4, Name = "Moje todo #4", Completed = false });
            _todosDatabase.Add(new ToDoViewModel { Id = 5, Name = "Moje todo #5", Completed = false });
        }

        public IList<ToDoViewModel> GetAll()
        {
            return _todosDatabase;
        }

        public ToDoViewModel GetById(int id)
        {
            return _todosDatabase.FirstOrDefault(x => x.Id == id);
        }

        public ToDoViewModel GetByName(string name)
        {
            return _todosDatabase.FirstOrDefault(x => x.Name == name);
        }

        public int SaveNote(ToDoViewModel toDoViewModel)
        {
            var nextId = _todosDatabase.Count + 1;

            //add to our tempdb
            _todosDatabase.Add(new ToDoViewModel { Id = nextId, Name = toDoViewModel.Name, Completed = toDoViewModel.Completed });

            return nextId;
        }
    }
}
