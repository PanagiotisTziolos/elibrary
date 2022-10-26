namespace e_library.Pages.Admin.Books;


public class FileError
{
    private bool _error;
    private string _message;
    
    public FileError(bool error, string message)
    {
        _error = error;
        _message = message;
    }
    
    public void Check()
    {
        if (_error)
            throw new Exception(_message);
    }
}
