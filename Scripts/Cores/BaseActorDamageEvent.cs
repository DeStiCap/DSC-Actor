using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorDamageEvent<ActorData,DamageData> : ScriptableObject 
        where ActorData : BaseActorData
        where DamageData : struct
    {
        public abstract void RunEvent(ActorData hActorData, DamageData hDamageData);
    }
}