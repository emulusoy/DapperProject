using DapperProject.Dtos.OrderDtos;

namespace DapperProject.Repositories.Abstract
{
    public interface IOrderService
    {
        Task<List<ResultOrderDto>> GetAllOrderAsync();
        Task CreateOrderAsync(CreateOrderDto createOrderDto);
        Task UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task DeleteOrderAsync(int id);
        Task<GetOrderByIdDto> GetOrderByIdAsync(int id);
    }
}
