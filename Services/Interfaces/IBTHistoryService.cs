using DBugr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Services.Interfaces
{
    public interface IBTHistoryService
    {
        Task AddHistory(Ticket oldTicket, Ticket newTicket, string userId);

        Task<List<TicketHistory>> GetProjectTicketsHistories(int projectId);

        Task<List<TicketHistory>> GetCompanyTicketsHistories(int companyId);
    }
}
