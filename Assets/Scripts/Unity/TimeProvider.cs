using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Unity
{
    using UnityTime = UnityEngine.Time;

    public sealed class TimeProvider : ITimeProvider
    {
        public float SecondsSinceStartOfGame => UnityTime.time;

        public float SecondsSinceLastFrame => UnityTime.deltaTime;
    }
}
