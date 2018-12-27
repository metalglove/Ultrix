using System.Collections.Generic;
using Ultrix.Application.DTOs;

namespace Ultrix.Presentation.ViewModels.Meme
{
    public class SharedMemesViewModel
    {
        public IEnumerable<SharedMemeDto> SharedMemes { get; set; }

        public SharedMemesViewModel(IEnumerable<SharedMemeDto> sharedMemes)
        {
            SharedMemes = sharedMemes;
        }
    }
}
