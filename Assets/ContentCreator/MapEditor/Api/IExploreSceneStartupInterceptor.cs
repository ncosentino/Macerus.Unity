using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Api
{
    public interface IExploreSceneStartupInterceptor
    {
        void Intercept(GameObject explore);
    }
}
