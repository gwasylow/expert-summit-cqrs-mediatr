using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatrCQRS.Logic.Notifications.Handlers
{
    public class LowAlarmNotificationHandler : INotificationHandler<AlarmNotification>
    {
        public LowAlarmNotificationHandler()
        {

        }

        public Task Handle(AlarmNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Summ-it: *** LowAlarmHandler | {DateTime.Now} | raised with the following message: {notification.Message} and id {notification.Id}");
            
            return Task.CompletedTask;
        }

    }
}
