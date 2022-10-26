namespace e_library.Pages.Admin.Books;


public class FileErrorList
{
    private FileError[] _errorList;
    
    public FileErrorList(FileError[] errorList)
        =>  _errorList = errorList;
      
    public void Check()
        =>  Array.ForEach(_errorList, e => e.Check());
}
