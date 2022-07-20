using CustomerSystem.Repositories.Repositories;
using System;
using CustomerSystem.Common.Entities.Dtos;


namespace CustomerSystem.Services
{
    public class CustomerService: ICustomerService
    {

        private readonly ICustomerRepository _CustomerRepo;

        public CustomerService(ICustomerRepository repo)
        {
            _CustomerRepo = repo;   
            
        }

        public CustomerModel GetCustomerById(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new Exception("UserId Cannont be  empty");

            return _CustomerRepo.GetCustomerById(userId);
        }

        public List<CustomerModel> GetCustomers()
        {

            return _CustomerRepo.GetCustomers();
        }

        public List<OrderViewModel> ActiveOrdersbyCustomer(Guid userId)
        {

            return _CustomerRepo.ActiveOrdersbyCustomer(userId);
        }

        public string SaveCustomer(CustomerModel obj)
        {
            if (string.IsNullOrEmpty(obj.Username))
                throw new Exception("User Name is empty");
            if (string.IsNullOrEmpty(obj.Email))
                throw new Exception("Email is empty");
            if (string.IsNullOrEmpty(obj.FirstName))
                throw new Exception("First Name is empty");
            if (string.IsNullOrEmpty(obj.LastName))
                throw new Exception("Last Name Name is empty");

            return _CustomerRepo.SaveCustomer(obj);
            

        }
        public string UpdateCustomer(Guid userId, CustomerModel obj)
        {
            if (userId == Guid.Empty)
                throw new Exception("UserId Cannont be  empty");
            if (string.IsNullOrEmpty(obj.Username))
                throw new Exception("User Name is empty");
            if (string.IsNullOrEmpty(obj.Email))
                throw new Exception("Email is empty");
            if (string.IsNullOrEmpty(obj.FirstName))
                throw new Exception("First Name is empty");
            if (string.IsNullOrEmpty(obj.LastName))
                throw new Exception("LastName Name is empty");

            return _CustomerRepo.UpdateCustomer(userId,obj);

        }
        public string DeleteCustomer(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new Exception("UserId Cannont be  empty");

            return _CustomerRepo.DeleteCustomer(userId);


        }
    }
}