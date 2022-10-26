namespace e_library.Pages.Admin.Books;


public class Cover
{
    private const string COVER_SHOW_PATH = "images/covers/";
    private const string SAVE_PATH = "././wwwroot/images/covers/";
    
    private TempFile _tempFile;
    private string _name;
    
    public Cover(TempFile tempFile, string name)
    {
        _tempFile = tempFile;
        _name = name;
    }
    
    public static string Directory
      =>  SAVE_PATH;
    
    public static string ShowDirectory
        =>  COVER_SHOW_PATH;
    
    public void Save()
    {
        _tempFile.MoveTo(SAVE_PATH, _name);
    }
}
