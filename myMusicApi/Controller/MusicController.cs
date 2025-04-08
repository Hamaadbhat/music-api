using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using myMusicApi.model;
using myMusicApi.services;
using myMusicApi.MusicDTOs;

namespace myMusicApi.Controller
{
    [Route("api/[controller]")] 
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;
        
        public MusicController(IMusicService musicService ,IMapper mapper )
        {
            _musicService = musicService;
            _mapper = mapper;
            
        }
        [HttpPost]
        public async Task<IActionResult> Post(MusicDTO musicDTO)
        {
           Console.WriteLine("post controller called");
            
            if (musicDTO is null)
            {
                throw new ArgumentNullException(nameof(musicDTO));
            }
            // map musicDTO to music and send it to the AddMusic function 
            // that comes through the repository
            Music music = _mapper.Map<Music>(musicDTO);
            var result = await _musicService.AddMusic(music);
            if (result is not null)
            {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<MusicDTO>(result));
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // Update Music
        [HttpPut]
        public async Task<IActionResult> Put(MusicDTO musicDTO)
        {
            // map musicDTO to music and send it to the AddMusic function 
            // that comes through the repository
            var result = await _musicService.EditMusic(_mapper.Map<Music>(musicDTO));
            if (result is not null)
            {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<MusicDTO>(result));
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // Delete Music
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var result = _musicService.Delete(id);
            if (result is true)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // Details a music
        [HttpGet("Details")]
        public IActionResult Details(string id)
        {
            var result = _musicService.DetailsMusic(id);
            if (result is not null)
            {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<MusicDTO>(result));
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // List Music
        [HttpGet]
        public IActionResult List()
        {
            var result = _musicService.ListMusic();
            if (result.Any())
            {
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<IEnumerable<MusicDTO>>(result));
            }
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
   
}
