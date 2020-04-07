using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "BehaviourEvent_Walk_Player", menuName = "DSC/Actor/Behaviour/Event/Player/Walk")]
    public sealed class DSC_ActorBehaviourEvent_Walk_Player : BaseActorBehaviourEvent
    {
        public override void RunEvent(BaseActorController hBaseController, BaseActorBehaviour hBaseBehaviour)
        {
            var hInput = hBaseController.baseActorInput;
            if (hInput == null ||!hBaseController.TryGetActorData(out ActorData hActorData))
                return;


            float fHorizontal = hInput.inputData.m_fHorizontal;

            if (fHorizontal == 0)
            {
                if (!FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.Walking))
                    return;

                hActorData.m_eStateFlag &= ~ActorStateFlag.Walking;
            }
            else if (!FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.Walking))
                hActorData.m_eStateFlag |= ActorStateFlag.Walking;
        }
    }
}