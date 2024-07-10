using CargoSimBackend.DTO_s;
using CargoSimBackend.Services.Infrastructure;
using System.Collections.Concurrent;
using System.Reflection.Metadata.Ecma335;

namespace CargoSimBackend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGridService _gridService;
        private static ConcurrentQueue<Order> _orders = new ConcurrentQueue<Order>();
        public OrderService(IGridService gridService)
        {
            this._gridService = gridService;
        }

        public async Task<(List<Order>?, string)> GetOrders()
        {
            try
            {
                return (await Task.FromResult<List<Order>>(OrderService._orders.ToList()), string.Empty);
            }
            catch (Exception ex)
            {
                return (null, await Task.FromException<string>(ex));
            }
        }
        public async Task<string> AppendOrder(Order order)
        {
            try
            {
                OrderService._orders.Enqueue(order);
                return await Task.FromResult(string.Empty);
            }
            catch (Exception ex)
            {
                return await Task.FromException<string>(ex);
            }
        }

        public async Task CechkOrderToAccept()
        {
            try{
                if (this._gridService.GetGrid() == null)
                {
                    return;
                }
                this.DeleteExpiredOrders();
                List<Order> OrdersWithValid = OrderService._orders.AsParallel()
                .Where(order => this._gridService.CheckConnection(order.OriginNodeId, order.TargetNodeId)).OrderByDescending(o=>o.Value).ToList();
                    Console.WriteLine(OrdersWithValid.Count);
                OrdersWithValid=OrdersWithValid.AsParallel().Select((order)=>
                {
                    var connection = this._gridService.GetConnection(order.OriginNodeId, order.TargetNodeId).Result;
                    var edge=this._gridService.GetEdgeById(connection.EdgeId);
                    
                    return order ;
                }).ToList();
            }catch(Exception ex)
            {

            }


        }

        private void DeleteExpiredOrders()
        {
            OrderService._orders.ToList().RemoveAll(order => order.ExpirationDateUtc > DateTime.Now);   
        }
        public Task DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }


    }
}
