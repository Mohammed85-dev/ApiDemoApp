using ApiDemo.TypesData;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.DataBase.Interfaces;

public interface IFileInfoDB {
    /**/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <returns>FileId</returns>
    public Guid AddFileInfo(DBFileInfo fileInfo);
    public List<string> GetRequiredPermissions(Guid fileId);
    public void AddRequiredPermission(Guid fileId, string permissionName);
    public void DeleteRequiredPermission(Guid fileId, string permissionName);
    public string GetCompleteFilePath(Guid fileId);
    public void DeleteFileInfo(Guid fileId);
    public DBFileInfo GetFileInfo(Guid fileId);
}