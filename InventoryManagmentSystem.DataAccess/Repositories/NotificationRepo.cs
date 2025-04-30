using InventoryManagmentSystem.DataAccess.Data;
using InventoryManagmentSystem.DataAccess.Interfaces;
using InventoryManagmentSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.DataAccess.Repositories
{
    public class NotificationRepo : GenericRepository<Notification> , INotificationRepo
    {
        private readonly AppDbContext appDbContext;

        public NotificationRepo(AppDbContext appDbContext) : base(appDbContext) {

            this.appDbContext = appDbContext;   
        } 
    }
}
