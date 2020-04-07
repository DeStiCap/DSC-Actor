using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "BehaviourCondition_LockFlag", menuName = "DSC/Actor/Behaviour/Condition/Lock Flag")]
    public class DSC_ActorBehaviourCondition_LockFlag : ActorBehaviourCondition
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] ActorLockFlag m_eLockFlag;
        [SerializeField] bool m_bIsLock;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(BaseActorController hBaseController)
        {
            if (!hBaseController.TryGetActorData(out ActorData hActorData)
                || !PassAllCondition(hBaseController))
                return false;

            return (m_bIsLock == FlagUtility.HasFlagUnsafe(hActorData.m_eLockFlag, m_eLockFlag));
        }

        #endregion
    }
}