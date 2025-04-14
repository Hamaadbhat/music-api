using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace myMusicApi.model
{
    [Keyless]
    public class eu_crop_dict
    {
        public string EU_CROP_CODE { get; set; } = null!;
        public string EU_CROP_NAME { get; set; } = null!;

        public string LANGUAGE { get; set; } = null!;
    }
}
