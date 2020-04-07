using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "BehaviourEvent_FlipFacing", menuName = "DSC/Actor/Behaviour/Event/Flip Facing")]
    public sealed class DSC_ActorBehaviourEvent_FlipFacing : BaseActorBehaviourEvent
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] bool m_bRightDefault = true;

#pragma warning restore 0649
        #endregion

        public override void RunEvent(BaseActorController hBaseController, BaseActorBehaviour hBaseBehaviour)
        {
            if (!hBaseController.TryGetActorData(out ActorData hActorData))
                return;

            float fAngle = hActorData.m_hActor.localEulerAngles.y;
            if(m_bRightDefault && fAngle == 0)
                hActorData.m_eStateFlag |= ActorStateFlag.FacingRight;
            else if(!m_bRightDefault && fAngle != 0)
                hActorData.m_eStateFlag &= ~ActorStateFlag.FacingRight;
        }
    }
}