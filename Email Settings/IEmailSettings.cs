namespace e_library.Email_Settings;

public interface IEmailSettings
{
	public string Server { get; }
	
	public int Port { get; }
	
	public string Email { get; }
	
	public string Password { get; }
}
