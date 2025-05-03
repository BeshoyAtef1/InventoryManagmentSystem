using InventoryManagmentSystem.Business.Interfaces;
using InventoryManagmentSystem.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Business.Services
{
    public class NotificationServices : INotificationServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
    }
}
