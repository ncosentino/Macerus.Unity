#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else

#endif

using Macerus.Plugins.Features.PartyBar;

namespace Assets.Scripts.Plugins.Features.PartyBar.Noesis
{
    public interface IPartyBarPortraitNoesisViewModelConverter
    {
        IPartyBarPortraitNoesisViewModel Convert(IPartyBarPortraitViewModel viewModel);
    }
}