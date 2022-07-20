using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient; 
using Microsoft.Extensions.Configuration;
using CustomerSystem.Common.Entities.Dtos;
using CustomerSystem.Services;

namespace BackendAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerService _ServceRepo;

        public CustomersController(ICustomerService serrepo)
        {
            _ServceRepo = serrepo;
        }

        [HttpGet]
        [Route("GetCustomers")]
        public List<CustomerModel> GetCustomers()
        {
            return _ServceRepo.GetCustomers();
        }

        [HttpGet]
        [Route("GetCustomerById")]
        public CustomerModel GetCustomerById(Guid userId)
        {
            CustomerModel customer = new CustomerModel();

            try
            {
                customer = _ServceRepo.GetCustomerById(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customer ?? new CustomerModel();
        }
         

        [HttpGet]
        [Route("ActiveOrdersbyCustomer")]
        public List<OrderViewModel> ActiveOrdersbyCustomer(Guid userId)
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            try
            {
                orders = _ServceRepo.ActiveOrdersbyCustomer(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return orders;
        }

        [HttpPost]
        [Route("SaveCustomer")]
        public IActionResult SaveCustomer(CustomerModel obj)
        {
            try
            {
                return Ok(_ServceRepo.SaveCustomer(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer(Guid userId, CustomerModel obj)
        {
            try
            {
                return Ok(_ServceRepo.UpdateCustomer(userId, obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public IActionResult DeleteCustomer(Guid userId)
        {
            try
            {
                return Ok(_ServceRepo.DeleteCustomer(userId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
