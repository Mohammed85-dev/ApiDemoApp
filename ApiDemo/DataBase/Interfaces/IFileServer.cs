namespace ApiDemo.DataBase.Interfaces;

public interface IFileServer {
    public Task UploadFile(string filePath, string fileName, Stream stream);
    public Stream GetFile(string completeFilePath);
    public Stream GetFile(string filePath, string fileName);
    public void DeleteFile(string completeFilePath);
    public void DeleteFile(string filePath, string fileName);
    public void DeleteFolder(string folderPath);
}