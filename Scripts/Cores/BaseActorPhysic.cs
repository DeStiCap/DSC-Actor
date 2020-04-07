using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorPhysic : MonoBehaviour
    {
        public abstract bool isKinematic { get; set; }

        public abstract float mass { get; set; }

        public abstract float drag { get; set; }

        public abstract float angularDrag { get; set; }
        protected abstract float timeScale { get; }
        protected abstract void OnTimeScaleChange(float fNewTimeScale);
    }
}