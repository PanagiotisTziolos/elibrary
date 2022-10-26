using Microsoft.AspNetCore.DataProtection;

using System.Net;
using System.Net.Mail;

using e_library.Email_Settings;
using e_library.Model;
using e_library.Services;

namespace e_library.Services.Implementation;


public sealed class Mailman : IMailman
{
	private readonly IDataProtector _protector;
	private readonly IEmailSettings _emailSettings;
	
	public Mailman(IDataProtectionProvider _protectionProvider)
	{
		_protector = _protectionProvider.CreateProtector("password_protector");
		_emailSettings = new EmailSettings(_protector);
	}
	
	public void Send(EmailInfo email)
	{
    MailMessage mm = new(
                        _emailSettings.Email,
			                  email.Address,
                      	email.Subject,
			                  email.Body);
			
    using (SmtpClient client = new())
    {
      client.Host = _emailSettings.Server;
      client.Port = _emailSettings.Port;
      
      client.EnableSsl = true;
      client.UseDefaultCredentials = false;
				
      client.Credentials = new NetworkCredential(
        _emailSettings.Email, 
        _protector.Unprotect(_emailSettings.Password)
      );
				
      client.Send(mm);
    }
	}
}
