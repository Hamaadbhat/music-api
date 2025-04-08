using myMusicApi.model;

namespace myMusicApi.services
{
  
        public interface IMusicService
        {
            // Add Music based on the Music model
            Task<Music> AddMusic(Music music);

            // Edit Music based on the Music model
            Task<Music> EditMusic(Music music);

            // Delete a Music based on the id
            bool Delete(string id);

            // Details of a Music based on the id
            Music DetailsMusic(string id);

            // List of all Music
            IEnumerable<Music> ListMusic();
        }
    }

