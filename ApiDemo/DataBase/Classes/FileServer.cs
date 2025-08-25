using ApiDemo.DataBase.Interfaces;

namespace ApiDemo.DataBase.Classes;

public class FileServer : IFileServer {
    public void UploadFile(string filePath, string fileName, Stream stream) {
        string completePath = Path.Combine(filePath, fileName);
        using var fileStream = File.Create(completePath);
        stream.CopyTo(fileStream);
        fileStream.Close();
    }

    public void DeleteFile(string completeFilePath) {
        File.Delete(completeFilePath);
    }

    public void DeleteFile(string filePath, string fileName) {
        string completePath = Path.Combine(filePath, fileName);
        File.Delete(completePath);
    }

    public Stream GetFile(string completeFilePath) {
        return File.OpenRead(completeFilePath);
    }

    public Stream GetFile(string filePath, string fileName) {
        string completePath = Path.Combine(filePath, fileName);
        return GetFile(completePath);
    }

    public void DeleteFolder(string path) {
        if (Directory.Exists(path)) {
        Directory.Delete(path, true);
        }
    }
}