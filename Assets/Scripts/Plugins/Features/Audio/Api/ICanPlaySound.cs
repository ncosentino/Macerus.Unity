using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Plugins.Features.Audio.Api
{
    public interface ICanPlaySound
    {
        void PlaySound(float[] wave);
    }
}
