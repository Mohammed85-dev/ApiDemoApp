using ApiDemo.DataBase.Interfaces;
using ApiDemo.TypesData;
using Cassandra.Data.Linq;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class FileInfoDB : IFileInfoDB {
    private readonly Table<DBFileInfo> _dbFileInfos;

    public FileInfoDB(ISession cassandraSession) {
        _dbFileInfos = new Table<DBFileInfo>(cassandraSession);
        _dbFileInfos.CreateIfNotExists();
    }

    public Guid AddFileInfo(DBFileInfo fileInfo) {
        fileInfo.UniqueFileId = Guid.NewGuid();
        _dbFileInfos.Insert(fileInfo).Execute();
        return fileInfo.UniqueFileId;
    }

    public List<string> GetRequiredPermissions(Guid fileId) {
        return _dbFileInfos.Where(f => f.UniqueFileId == fileId)
            .Select(f => f.UniqueRequiredPermission)
            .Execute().FirstOrDefault() ?? throw new InvalidOperationException();
    }

    public void AddRequiredPermission(Guid fileId, string permissionName) {
        _dbFileInfos.Where(f => f.UniqueFileId == fileId)
            .Select(f => new DBFileInfo {
                UniqueRequiredPermission = new List<string> { permissionName, },
            }).Execute().FirstOrDefault();
    }

    public void DeleteRequiredPermission(Guid fileId, string permissionName) {
        _dbFileInfos.Where(f => f.UniqueFileId == fileId).Delete();
    }

    public string GetCompleteFilePath(Guid fileId) {
        var dbFileInfo = _dbFileInfos.First(file => file.UniqueFileId == fileId).Execute();
        return Path.Combine(dbFileInfo.Path, dbFileInfo.FileName);
    }

    public void DeleteFileInfo(Guid fileId) {
        _dbFileInfos.Where(file => file.UniqueFileId == fileId).Delete().Execute();
    }

    public DBFileInfo GetFileInfo(Guid fileId) {
        return _dbFileInfos.First(file => file.UniqueFileId == fileId).Execute();
    }
}