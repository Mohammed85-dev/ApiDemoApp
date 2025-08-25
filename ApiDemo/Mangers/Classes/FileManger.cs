using ApiDemo.DataBase.Interfaces;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.TypesData;

namespace ApiDemo.Mangers.Classes;

public class FileManger : IFileManger {
    public enum FileType {
        courseVideo,
        UserAvatar,
    }
    private readonly IFileServer _fileServer;
    private readonly IFileInfoDB _filesInfo;
    private readonly string _rootPath;

    public FileManger(IFileInfoDB filesInfo, IFileServer fileServer) {
        _filesInfo = filesInfo;
        _fileServer = fileServer;
        _rootPath = Path.Combine("C:", "FileServerFiles");
        _fileServer.DeleteFolder(_rootPath);
    }

    public List<string> GetRequiredPermission(Guid fileId) {
        return _filesInfo.GetRequiredPermissions(fileId);
    }

    public DBFileInfo GetFullFileInfo(Guid fileId) {
        return _filesInfo.GetFileInfo(fileId);
    }

    public Stream GetFileStream(Guid fileId) {
        return _fileServer.GetFile(_filesInfo.GetCompleteFilePath(fileId));
    }

    public void DeleteFile(Guid fileId) {
        _fileServer.DeleteFile(_filesInfo.GetCompleteFilePath(fileId));
    }

    public async Task<Guid> UploadFile(Guid OwnerUserId, Stream stream, string orignalFileName, FileType fileType) {
        var fileName = OwnerUserId + orignalFileName + fileType + stream.Length;
        var path = Path.Combine(_rootPath, fileType.ToString());
        Directory.CreateDirectory(path);
        var info = new DBFileInfo();
        info.FileName = fileName;
        info.OwnerUserId = OwnerUserId;
        info.Path = path;
        info.UniqueRequiredPermission = [fileName,];

        await _fileServer.UploadFile(info.Path, info.FileName, stream);
        return _filesInfo.AddFileInfo(info);
    }

    public void AddFilePermission(Guid fileId, string permissionName) {
        _filesInfo.AddRequiredPermission(fileId, permissionName);
    }

    public void RemoveFilePermission(Guid fileId, string permissionName) {
        _filesInfo.DeleteRequiredPermission(fileId, permissionName);
    }
}