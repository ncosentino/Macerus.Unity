using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class DynamicAnimationBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public string AnimationId;

        public bool Visible;
    }
}
