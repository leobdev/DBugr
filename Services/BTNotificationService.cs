using DBugr.Models;
using DBugr.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Services
{
    public class BTNotificationService : IBTNotificationService
    {
        public Task AdminsNotificationAsync(Notification notification, int companyId)
        {
            throw new NotImplementedException();
        }

        public Task EmailNotificationAsync(Notification notification, string emailSubject)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetReceivedNotificationsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetSentNotificationsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task MembersNotificationAsync(Notification notification, List<BTUser> members)
        {
            throw new NotImplementedException();
        }

        public Task SaveNotificationAsync(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Task SMSNotificationAsync(string phone, Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
