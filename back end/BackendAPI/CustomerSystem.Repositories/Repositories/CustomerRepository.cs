using CustomerSystem.Common.Entities.Dtos;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace CustomerSystem.Repositories.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly IConfiguration? _configuration;

        public CustomerRepository(IConfiguration? configuration)
        {

            _configuration = configuration;
        }

        public List<CustomerModel> GetCustomers()
        {
            List<CustomerModel> customers = new List<CustomerModel>();


            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select * from tblCustomer", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CustomerModel obj = new CustomerModel();
                obj.UserId = (Guid)dt.Rows[i]["UserId"];
                obj.Username = dt.Rows[i]["Username"].ToString();
                obj.Email = dt.Rows[i]["Email"].ToString();
                obj.FirstName = dt.Rows[i]["FirstName"].ToString();
                obj.LastName = dt.Rows[i]["LastName"].ToString();
                obj.CreatedOn = (DateTime)dt.Rows[i]["CreatedOn"];
                obj.IsActive = (bool)dt.Rows[i]["IsActive"];

                customers.Add(obj);
            }

            return customers;
        }

        public CustomerModel GetCustomerById(Guid userId)
        {
            CustomerModel customer = new CustomerModel();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select * from tblCustomer where UserId = '" + userId + "'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            customer.UserId = (Guid)dt.Rows[0]["UserId"];
            customer.Username = dt.Rows[0]["Username"].ToString();
            customer.Email = dt.Rows[0]["Email"].ToString();
            customer.FirstName = dt.Rows[0]["FirstName"].ToString();
            customer.LastName = dt.Rows[0]["LastName"].ToString();
            customer.CreatedOn = (DateTime)dt.Rows[0]["CreatedOn"];
            customer.IsActive = (bool)dt.Rows[0]["IsActive"];

            return customer;
        }

        public List<OrderViewModel> ActiveOrdersbyCustomer(Guid userId)
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("GetActiveOrdersByCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Customer_ID", userId);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OrderViewModel obj = new OrderViewModel();

                obj.OrderId = (Guid)dt.Rows[i]["OrderId"];
                obj.ProductId = (Guid)dt.Rows[i]["ProductId"];
                obj.OrderStatus = (int?)dt.Rows[i]["OrderStatus"];
                obj.OrderType = (int?)dt.Rows[i]["OrderType"];
                obj.OrderBy = (Guid)dt.Rows[i]["OrderBy"];
                obj.OrderedOn = (DateTime)dt.Rows[i]["OrderedOn"];
                obj.ShippedOn = (DateTime)dt.Rows[i]["ShippedOn"];
                obj.IsActive = (bool)dt.Rows[i]["IsActive"];
                obj.Username = dt.Rows[i]["Username"].ToString();
                obj.ProductName = dt.Rows[i]["ProductName"].ToString();

                orders.Add(obj);
            }

            return orders;
        }

        public string SaveCustomer(CustomerModel obj)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string query = "INSERT INTO tblCustomer (Username, Email, FirstName, LastName, CreatedOn, IsActive) " +
               "VALUES (@Username, @Email, @FirstName, @LastName, @CreatedOn, @IsActive) ";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = obj.Username;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = obj.Email;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = obj.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = obj.LastName;
            cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = obj.IsActive;

            con.Open();

            try
            {
                cmd.ExecuteNonQuery();
                return "Customer Saved Successully";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public string UpdateCustomer(Guid userId, CustomerModel obj)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string query = "UPDATE tblCustomer SET Username= @Username, Email= @Email ,FirstName= @FirstName,LastName= @LastName,CreatedOn= @CreatedOn,IsActive= @IsActive  where UserId = @UserId";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userId;
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = obj.Username;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = obj.Email;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = obj.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = obj.LastName;
            cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = obj.IsActive;
            con.Open();

            try
            {
                cmd.ExecuteNonQuery();
                return "Customer Updated Successully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public string DeleteCustomer(Guid userId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Delete from tblCustomer where UserId = '" + userId + "'", con);
            con.Open();

            try
            {
                cmd.ExecuteNonQuery();
                return "Customer Deleted Successully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }


    }
}