using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor.Behaviour2D
{
    [CreateAssetMenu(fileName = "Behaviour_GravityIncrease2D", menuName = "DSC/Actor/Behaviour/2D/Common/Gravity Increase")]
    public class DSC_ActorBehaviour_GravityIncrease2D : ActorBehaviour
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [Min(0)]
        [SerializeField] protected float m_fGravityMultiplier = 3f;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnFixedUpdateBehaviour(BaseActorController hBaseController)
        {
            if (!PassCondition(hBaseController))
                return;

            OnGravityIncrease(hBaseController);
        }

        #endregion

        #region Main

        protected virtual void OnGravityIncrease(BaseActorController hBaseController)
        {
            if (!hBaseController.TryGetActorData(out BaseActorData2D hActorData)
                || hActorData.m_hPhysic == null)
                return;

            Vector2 vVelocity = hActorData.m_hPhysic.velocity;
            vVelocity.y += Physics2D.gravity.y * hActorData.m_hPhysic.gravityScale * m_fGravityMultiplier * hBaseController.actorFixedDeltaTime;
            hActorData.m_hPhysic.velocity = vVelocity;
        }

        #endregion
    }
}