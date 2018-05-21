using UnityEngine;

namespace Assets.Scripts.Behaviours.Generic
{
    public sealed class UpdateBehaviour : MonoBehaviour
    {
        public IUpdate ToUpdate { get; set; }

        private void Update()
        {
            ToUpdate?.Update(Time.deltaTime);
        }
    }
}
