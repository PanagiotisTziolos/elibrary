using Microsoft.AspNetCore.DataProtection;

namespace e_library.Email_Settings;

public sealed class EmailSettings : IEmailSettings
{
	private readonly string _rawPassword;
	private readonly IDataProtector? _protector;
	
	public EmailSettings()
	{
		Server = "smtp.office365.com";
		Port = 587;
		Email = "kirra11@outlook.com";
		_rawPassword = "notis123";
	}
	
	public EmailSettings(IDataProtector protector) : this()
	{
		_protector = protector;
	}
	
	public string Server { get; }
	
	public int Port { get; }
	
	public string Email { get; }
	
	public string Password
	{
		get => _protector?.Protect(_rawPassword) ?? throw new Exception("Password is protected. A protector is required.");
	}
}
