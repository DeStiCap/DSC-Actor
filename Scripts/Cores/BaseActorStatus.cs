using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorStatus<ActorStatus> : MonoBehaviour
    {
        public abstract ActorStatus status { get; }
    }
}