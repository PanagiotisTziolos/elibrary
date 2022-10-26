using e_library.Services;

namespace e_library.Services.Implementation;


public sealed class OrderNotificationService : IOrderNotificationService
{  
  public event Handler OrderHandler;
  
  public void Notify()
    => OrderHandler?.Invoke();
}

