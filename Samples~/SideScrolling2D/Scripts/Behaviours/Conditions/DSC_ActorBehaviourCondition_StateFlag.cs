using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "BehaviourCondition_StateFlag", menuName = "DSC/Actor/Behaviour/Condition/State Flag")]
    public class DSC_ActorBehaviourCondition_StateFlag : ActorBehaviourCondition
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] ActorStateFlag m_eStateFlag;
        [SerializeField] bool m_bIsState;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(BaseActorController hBaseController)
        {
            if (!hBaseController.TryGetActorData(out ActorData hActorData)
                || !PassAllCondition(hBaseController))
                return false;

            return (m_bIsState == FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, m_eStateFlag));
        }

        #endregion
    }
}