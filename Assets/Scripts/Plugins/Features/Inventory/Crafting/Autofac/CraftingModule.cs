using Assets.Scripts.Plugins.Features.Inventory.Crafting.Noesis.Resources;
using Assets.Scripts.Plugins.Features.Inventory.Noesis;
using Assets.Scripts.Plugins.Features.Inventory.Noesis.Resources;

using Autofac;

using Macerus.Plugins.Features.Inventory.Api;

namespace Assets.Scripts.Plugins.Features.Inventory.Crafting.Autofac
{
    public sealed class CraftingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CraftingWindowNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CraftingWindow>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(x =>
                {
                    var itemSlotCollectionNoesisViewModel = new ItemSlotCollectionNoesisViewModel(
                        x.Resolve<IBagSlotToNoesisViewModelConverter>(),
                        x.ResolveNamed<IItemSlotCollectionViewModel>("player crafting bag"),
                        null);
                    var playerCraftingBagView = new CraftingBagView(itemSlotCollectionNoesisViewModel);
                    return playerCraftingBagView;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
