#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
#endif

using System;
using System.Collections.Generic;
namespace Assets.Scripts.Plugins.Features.CharacterSheet.Noesis
{
    public interface ICharacterSheetNoesisViewModel
    {
        IEnumerable<Tuple<string, string>> Stats { get; }
    }
}