using Dapper;
using DapperProject.Context;
using DapperProject.Dtos.CustomerDtos;
using DapperProject.Repositories.Abstract;

namespace DapperProject.Repositories.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly DapperContext _context;

        public CustomerService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            string query = "INSERT INTO Customers (CustomerName,CustomerSurname,CustomerBalance) values (@customerName,@customerSurname,@customerBalance)";
            var parameters = new DynamicParameters();
            
            parameters.Add("@customerName", createCustomerDto.CustomerName);
            parameters.Add("@customerSurname", createCustomerDto.CustomerSurname);
            parameters.Add("@customerBalance", createCustomerDto.CustomerBalance);
            var connection =_context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);

        }

        public async Task DeleteCustomer(int customerId)
        {
            string query = "DELETE FROM Customers WHERE CustomerId = @customerId";
            var parameters = new DynamicParameters();
            parameters.Add("@customerId", customerId);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task<List<ResultCustomerDto>> GetAllCustomerAsync()
        {
            string query = "SELECT * FROM Customers";
            var connection = _context.CreateConnection();
            var values=await connection.QueryAsync<ResultCustomerDto>(query);
            return values.ToList();
        }

        public async Task<GetCustomerByIdDto> GetCustomerById(int id)
        {

            string query = "SELECT * FROM Customers WHERE CustomerId = @customerId";
            var parameters = new DynamicParameters();
            parameters.Add("@customerId", id);
            var connection = _context.CreateConnection();
            var values= await connection.QueryFirstAsync<GetCustomerByIdDto>(query);
            return values;
        }

        public async Task UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            string query = "UPDATE Customers SET CustomerName = @p1, CustomerSurname = @p2, CustomerBalance = @p3 WHERE CustomerId = @p4";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", updateCustomerDto.CustomerName);
            parameters.Add("@p2", updateCustomerDto.CustomerSurname);
            parameters.Add("@p3", updateCustomerDto.CustomerBalance);
            parameters.Add("@p4", updateCustomerDto.CustomerId);
            var connection = _context.CreateConnection();
            var values= await connection.ExecuteAsync(query, parameters);



        }
    }
}
