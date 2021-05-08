#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else

#endif

using Macerus.Plugins.Features.HeaderBar.Api.CombatTurnOrder;

namespace Assets.Scripts.Plugins.Features.HeaderBar.CombatTurnOrder.Noesis
{
    public interface ICombatPortraitNoesisViewModelConverter
    {
        ICombatTurnOrderPortraitNoesisViewModel Convert(ICombatTurnOrderPortraitViewModel viewModel);
    }
}