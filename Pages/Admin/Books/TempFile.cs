using Microsoft.AspNetCore.Components.Forms;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace e_library.Pages.Admin.Books;


public class TempFile
{
    private const string TEMP_SHOW_PATH = "images/temp/";
    private const string TEMP_PATH = "././wwwroot/images/temp/";
    
    private UploadedFile _uploadedFile;
    private string _name;
    private string _path;
    
    public TempFile(UploadedFile uploadedFile)
    {
        _uploadedFile = uploadedFile;
        _name = uploadedFile.Name;
        _path = $"{TEMP_PATH}{_name}";
    }
    
    public void Delete()
        =>  File.Delete(_path);
        
    public static string Directory
        =>  TEMP_PATH;
        
    public static string ShowDirectory
        =>  TEMP_SHOW_PATH;
    
    public async Task<string> ViewPath()
    {
        if (!File.Exists(_path))
            await Create();
      
        return $"{TEMP_SHOW_PATH}{_name}";
    }
    
    public void MoveTo(string path, string name)
    {
        using (var image = Image.Load(new FileStream(_path, FileMode.Open)))
        {
            image.SaveAsJpeg($"{path}{name}.jpg");
        }
    }
    
    private async Task Create()
    {
        await using FileStream fs = new (_path, FileMode.Create);
        
        try
        {
            await _uploadedFile.Save(fs);
        }
        catch (Exception ex)
        {
            File.Delete(_path);
            
            throw new Exception(ex.Message);
        }
    }
}
