using Dapper;
using DapperProject.Context;
using DapperProject.Dtos.OrderDtos;
using DapperProject.Repositories.Abstract;

namespace DapperProject.Repositories.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly DapperContext _context;

        public OrderService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            string query = "INSERT INTO Orders (ProductName, ProductPrice, ProductCount, CustomerId) VALUES (@ProductName, @ProductPrice, @ProductCount, @CustomerId)";
            var parameters = new DynamicParameters();
            parameters.Add("@ProductName", createOrderDto.ProductName);
            parameters.Add("@ProductPrice", createOrderDto.ProductPrice);
            parameters.Add("@ProductCount", createOrderDto.ProductCount);
            parameters.Add("@CustomerId", createOrderDto.CustomerId);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);

        }

        public Task DeleteOrderAsync(int id)
        {
            string query = "DELETE FROM Orders WHERE OrderId = @OrderId";
            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", id);
            var connection = _context.CreateConnection();
            return connection.ExecuteAsync(query, parameters);
        }

        public async Task<List<ResultOrderDto>> GetAllOrderAsync()
        {
            string query = "SELECT * FROM Orders";
            var connection = _context.CreateConnection();
            var values=await connection.QueryAsync<ResultOrderDto>(query);
            return values.ToList();
        }

        public async Task<GetOrderByIdDto> GetOrderByIdAsync(int id)
        {
            string query = "SELECT * FROM Orders WHERE OrderId = @OrderId";
            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", id);
            var connection = _context.CreateConnection();
            var values = await connection.QueryFirstAsync<GetOrderByIdDto>(query,parameters);
            return values;

        }

        public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            string query = "UPDATE Orders SET ProductName = @ProductName, ProductPrice = @ProductPrice, ProductCount = @ProductCount, CustomerId = @CustomerId WHERE OrderId = @OrderId";
            var parameters = new DynamicParameters();
            parameters.Add("@ProductName", updateOrderDto.ProductName);
            parameters.Add("@ProductPrice", updateOrderDto.ProductPrice);
            parameters.Add("@ProductCount", updateOrderDto.ProductCount);
            parameters.Add("@CustomerId", updateOrderDto.CustomerId);
            parameters.Add("@OrderId", updateOrderDto.OrderId);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query,parameters);

        }
    }
}
