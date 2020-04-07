using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorDamageBehaviour<ActorData,DamageBehaviourType> : ScriptableObject 
        where ActorData : BaseActorData
        where DamageBehaviourType : System.Enum
    {
        public abstract DamageBehaviourType behaviourType { get; }   
        public abstract void OnStart(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnUpdate(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnFixedUpdate(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnLateUpdate(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
        public abstract void OnEnd(ActorData hActorData, List<IActorBehaviourData> lstBehaviourData);
    }
}