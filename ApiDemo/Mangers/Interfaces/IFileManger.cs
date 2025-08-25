using ApiDemo.Mangers.Classes;
using ApiDemo.TypesData;

namespace ApiDemo.Mangers.Interfaces;

public interface IFileManger {
    public List<string> GetRequiredPermission(Guid fileId);
    public DBFileInfo GetFullFileInfo(Guid fileId);
    public Stream GetFileStream(Guid fileId);
    public void DeleteFile(Guid fileId);
    public Task<Guid> UploadFile(Guid OwnerUserId, Stream stream, string orignalFileName, FileManger.FileType fileType);
    public void AddFilePermission(Guid fileId, string permissionName);
    public void RemoveFilePermission(Guid fileId, string permissionName);
}