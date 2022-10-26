using e_library.Model;

namespace e_library.Services;

public interface IStore
{
	public Task<IReadOnlyList<OrderInfo>> AllOrdersAsync();
	
	public Task<IReadOnlyList<OrderInfo>> PendingOrdersAsync();
	
	public Task<IReadOnlyList<OrderInfo>> ReadyOrdersAsync();
	
	public Task<IReadOnlyList<OrderInfo>> DeliveredOrdersAsync();
	
	public Task<IReadOnlyList<OrderInfo>> CancelledOrdersAsync();
	
	public void AddOrder(OrderInfo orderInfo);
	
	public void ChangeTo(int id, OrderState state);
}

