using CargoSimBackend.DTO_s;

namespace CargoSimBackend.Services.Infrastructure
{
    public interface IOrderService
    {
        Task<string> AppendOrder(Order order);
        Task DeleteOrder(Order order);
        Task CechkOrderToAccept();
        Task<(List<Order>?,string)> GetOrders();
        
    }
}
