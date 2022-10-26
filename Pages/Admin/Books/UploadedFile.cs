using Microsoft.AspNetCore.Components.Forms;

namespace e_library.Pages.Admin.Books;


public class UploadedFile
{
    private const int MAX_UPLOAD_SIZE = 1_000_000;
    
    private FileErrorList _errors;   
    private IBrowserFile _file;
    
    public UploadedFile(IBrowserFile file)
    {
        _file = file;
        _errors = new(
            new FileError[] 
            {
                new FileError(
                    !(_file.ContentType == "image/png" ||
                        _file.ContentType == "image/jpeg"),
                    "File is not of supported type. Please upload a png or jpeg image file."
                ),
                new FileError(
                    _file.Size > MAX_UPLOAD_SIZE,
                    "File size exceeds the maximum allowed size of 1MB."
                ),
            }
        );
    }
    
    public string Name => _file.Name;
    
    public async Task Save(Stream stream)
    {
        _errors.Check();
      
        await _file.OpenReadStream(MAX_UPLOAD_SIZE).CopyToAsync(stream);
    }
}
