using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatrCQRS.Logic.Notifications.Handlers
{
    public class MediumAlarmNotificationHandler : INotificationHandler<AlarmNotification>
    {
        public MediumAlarmNotificationHandler()
        {

        }

        public Task Handle(AlarmNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Summ-it: *** MediumAlarmHandler | {DateTime.Now} | raised with the following message: {notification.Message} and id {notification.Id}");
            
            return Task.CompletedTask;
        }

    }
}
