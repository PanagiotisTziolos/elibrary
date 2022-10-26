namespace e_library.Services;

public delegate Task Handler();

public interface IOrderNotificationService
{
  public event Handler OrderHandler;
  
  public void Notify();
}

