namespace Read_Excel_File.Services.Abstracts;

public interface IJiraService
{
     Task<string> PostRequestJsonFormatAsync(string modelName, string modelCount);
}
