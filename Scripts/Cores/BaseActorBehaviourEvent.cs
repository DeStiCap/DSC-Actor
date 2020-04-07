using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorBehaviourEvent : ScriptableObject
    {
        public abstract void RunEvent(BaseActorController hBaseController,BaseActorBehaviour hBaseBehaviour);
    }
}