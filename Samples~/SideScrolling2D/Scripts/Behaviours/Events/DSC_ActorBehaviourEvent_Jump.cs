using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "BehaviourEvent_Jump", menuName = "DSC/Actor/Behaviour/Event/Jump")]
    public sealed class DSC_ActorBehaviourEvent_Jump : BaseActorBehaviourEvent
    {
        public override void RunEvent(BaseActorController hBaseController, BaseActorBehaviour hBaseBehaviour)
        {
            if (!hBaseController.TryGetActorData(out ActorData hActorData))
                return;

            hActorData.m_eStateFlag |= ActorStateFlag.Jumping;
            hActorData.m_eStateFlag &= ~ActorStateFlag.Falling;
        }
    }
}