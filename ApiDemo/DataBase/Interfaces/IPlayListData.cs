using ApiDemo.Models.Courses.PlayList;

namespace ApiDemo.DataBase.Interfaces;

public interface IPlayListDataDB {
    public PlaylistData GetPlayList(Guid playListId);
    public void CreatePlayList(CreatePlayList playList);
    public void UpdatePlayList(Guid playListId, PlaylistData playlistData);
    public void DeletePlayList(Guid playListId);
    public IEnumerable<KeyValuePair<int, Guid>> GetPlayListCourses(Guid playListId);
    public void AddPlayListCourse(Guid playListId, Guid courseId);
    public void DeletePlayListCourse(Guid playListId, int courseId);
    public Guid GetPictureFileId(Guid playListId);
    public void SetPictureFileId(Guid playListId, Guid pictureFileId);
    public void AddTag(Guid playListId, string tag);
    public void DeleteTag(Guid playListId, string tag);
}