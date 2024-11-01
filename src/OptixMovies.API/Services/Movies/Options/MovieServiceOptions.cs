using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace OptixMovies.API.Services.Movies.Options;

public class MovieServiceOptions
{
    private string _MobieDbFile = string.Empty;

    [Required]
    public string MovieDbFile
    {
        get
        {
            return _MobieDbFile;
        }
        set
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _MobieDbFile = value.Replace(@"/", @"\");
            }
            else
            {
                _MobieDbFile = value.Replace(@"\", @"/");
            }
        }
    }
}
