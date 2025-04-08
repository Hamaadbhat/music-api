using myMusicApi.model;
using System.Globalization;

namespace myMusicApi.services
{
    public class MusicService : IMusicService
    {
        private readonly AppDbContext _context;
        public MusicService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Music> AddMusic(Music music)
        {
            if(music!=null)
            {
                music.Id = Guid.NewGuid().ToString();



                int rowsadded =_context.addMusic(music);
                return music;
            }
            else
            {
                return music;
            }
        }

        public bool Delete(string id)
        {
            Music? music = _context.Musics.FirstOrDefault(m => m.Id == id);
            if (music != null)
            {
                _context.Musics.Remove(music);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Music DetailsMusic(string id)
        {
           return _context.Musics.FirstOrDefault(m => m.Id == id);
        }

        public Task<Music> EditMusic(Music music)
        {
            Music music1 = _context.Musics.FirstOrDefault(m => m.Id == music.Id);
            if(music1 != null)
            {
                music1.Title = music.Title;
                music1.ReleaseDate = music.ReleaseDate;
                music1.Artist = music.Artist;
                music1.Rate = music.Rate;
                _context.SaveChanges();
            }
            return Task.FromResult(music);
        }

        public IEnumerable<Music> ListMusic()
        {
             return _context.GetAllMusic();
        }
    }
}
