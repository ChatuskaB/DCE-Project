using CustomerSystem.Common.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSystem.Services
{
    public interface ICustomerService
    {

        public CustomerModel GetCustomerById(Guid userId);
        public List<CustomerModel> GetCustomers();
        public List<OrderViewModel> ActiveOrdersbyCustomer(Guid userId);
        public string SaveCustomer(CustomerModel obj);
        public string UpdateCustomer(Guid userId, CustomerModel obj);
        public string DeleteCustomer(Guid userId);
    }
}
