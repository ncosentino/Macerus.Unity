using UnityEngine;

namespace Assets.ContentCreator.MapEditor.Behaviours
{
    public sealed class DoorBehaviour :
        MonoBehaviour,
        IConvertableBehaviour
    {
        public bool AutomaticInteraction;

        public string TransitionToMapId;

        public bool HasPositionTransition;

        public float TransitionToX;

        public float TransitionToY;
    }
}
