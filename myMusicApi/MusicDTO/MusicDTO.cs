namespace myMusicApi.MusicDTOs
{
    public class MusicDTO
        {
            // To keep it simple, Music DTO properties are similar to the Music Model
            // Obviously we can add more properties and modify every think 
            // Finally, This DTO object should be mapped by AutoMapper
            public string Id { get; set; }
            public string Title { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string Artist { get; set; }
            public int Rate { get; set; }
        }
    }
