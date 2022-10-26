using e_library.Model;

namespace e_library.Services;

public interface ICart
{
	public IReadOnlyList<OrderItemInfo> Items { get; }
	
	public Task Add(int id);
	
	public void Remove(int id);
	
	public void Update(IReadOnlyList<OrderItemInfo> orderItems);
	
	public void Clear();
}


