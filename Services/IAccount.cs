using e_library.Model;

namespace e_library.Services;

public interface IAccount
{
  public Task CreateAsync(RegisterInfo Registration);
  
	public Task LogInAsync(LoginInfo Login);
	
	public Task LogOutAsync();
}

