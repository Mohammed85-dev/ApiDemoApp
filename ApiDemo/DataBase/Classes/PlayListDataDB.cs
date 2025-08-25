using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.Courses;
using ApiDemo.Models.Courses.PlayList;
using Cassandra.Data.Linq;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class PlayListDataDB : IPlayListDataDB {
    private readonly Table<PlaylistData> _playlistData;

    public PlayListDataDB(ISession cassandraSession) {
        _playlistData = new Table<PlaylistData>(cassandraSession);
        _playlistData.CreateIfNotExists();
    }

    private CqlQuery<PlaylistData> pldata(Guid playListId) {
        return _playlistData.Where(p => p.uniquePlaylistId == playListId);
    }

    public PlaylistData GetPlayList(Guid playListId) {
        return pldata(playListId).FirstOrDefault().Execute();
    }

    public void CreatePlayList(CreatePlayList playList) {
        _playlistData.Insert(new PlaylistData() {
            Name = playList.Name,
            Description = playList.Description,
            Tags = playList.Tags,
            Created = DateTime.Now,
        }).Execute();
    }

    public void UpdatePlayList(Guid playListId, PlaylistData playlistData) {
        pldata(playListId).Select(p => playlistData).Update().Execute();
    }

    public void DeletePlayList(Guid playListId) {
        pldata(playListId).Delete().Execute();
    }

    public IEnumerable<KeyValuePair<int, Guid>> GetPlayListCourses(Guid playListId) {
        return pldata(playListId).Select(p => p.Courses).FirstOrDefault().Execute();
    }

    public void AddPlayListCourse(Guid playListId, Guid courseId) {
        pldata(playListId).Select(p => new PlaylistData() {
            Courses = new Dictionary<int, Guid> { { p.Courses.Count, courseId } }
        }).Update().Execute();
    }

    public void DeletePlayListCourse(Guid playListId, int courseId) {
        pldata(playListId).Select(p => new PlaylistData() {
            Courses = p.Courses.SubstractAssign(courseId),
        }).Update().Execute();
    }

    public byte[]? GetPicture(Guid playListId) {
        return pldata(playListId).Select(p => p.Picture).Execute().FirstOrDefault();
    }

    public void SetPicture(Guid playListId, byte[] picture) {
        pldata(playListId).Select(p => new PlaylistData() {
            Picture = picture,
        }).Update().Execute();
    }

    public void AddTag(Guid playListId, string tag) {
        pldata(playListId).Select(p => new PlaylistData() {
            Tags = { tag },
        }).Update().Execute();
    }

    public void DeleteTag(Guid playListId, string tag) {
        pldata(playListId).Select(p => p.Tags.Remove(tag)).Update().Execute();
    }
}