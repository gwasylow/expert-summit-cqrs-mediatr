using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatrCQRS.Logic.Notifications
{
    //public record AlarmNotification(int Id, string Message) : INotification;

    public class AlarmNotification : INotification
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public AlarmNotification(int id)
        {
            Id = id;
        }
    }
}
