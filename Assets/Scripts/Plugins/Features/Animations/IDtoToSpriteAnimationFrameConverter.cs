using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;

namespace Assets.Scripts.Plugins.Features.Animations
{
    public interface IDtoToSpriteAnimationFrameConverter
    {
        ISpriteAnimationFrame Convert(SpriteAnimationFrameDto dto);
    }
}
