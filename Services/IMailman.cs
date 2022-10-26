using e_library.Model;

namespace e_library.Services;


public interface IMailman
{
	public void Send(EmailInfo email);
}
