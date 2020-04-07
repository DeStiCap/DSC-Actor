using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "Behaviour_UpdateActorState", menuName = "DSC/Actor/Behaviour/Custom/Update Actor State")]
    public class DSC_ActorBehaviour_UpdateActorState : ActorBehaviour
    {
        #region Base - Override

        public override void OnFixedUpdateBehaviour(BaseActorController hBaseController)
        {
            if (!hBaseController.TryGetActorData(out ActorData hActorData)
                || !PassCondition(hBaseController))
                return;

            if (hActorData.m_hPhysic)
            {
                Vector2 vVelocity = hActorData.m_hPhysic.velocity;

                if (!FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.IsGrounding)
                    && !FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.Falling))
                {
                    if (vVelocity.y < 0)
                    {
                        hActorData.m_eStateFlag |= ActorStateFlag.Falling;
                        hActorData.m_eStateFlag &= ~ActorStateFlag.Jumping;
                    }
                }
            }
        }

        #endregion
    }
}