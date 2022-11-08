using System;
using System.Collections.Generic;
using MediatrCQRS.ViewModels;

namespace MediatrCQRS.Interfaces
{
    public interface IToDoRepository
    {
        IList<ToDoViewModel> GetAll();
        ToDoViewModel GetById(int id);
        ToDoViewModel GetByName(string name);
        int SaveNote(ToDoViewModel toDoViewModel);
    }
}
