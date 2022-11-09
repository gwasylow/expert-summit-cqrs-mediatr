using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatrCQRS.Logic.Notifications.Handlers
{
    //TODO: #14 Notification sample - concrete handler
    public class CriticalAlarmNotificationHandler : INotificationHandler<AlarmNotification>
    {
        public CriticalAlarmNotificationHandler()
        {

        }

        public Task Handle(AlarmNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Summ-it: *** CriticalAlarmHandler | {DateTime.Now} | raised with the following message: {notification.Message} and id {notification.Id}");
            
            return Task.CompletedTask;
        }

    }
}
