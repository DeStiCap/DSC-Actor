using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Actor.Behaviour.Condition
{
    [CreateAssetMenu(fileName = "BehaviourCondition_Input_Holding", menuName = "DSC/Actor/Behaviour/Condition/Input/Holding")]
    public sealed class DSC_ActorBehaviourCondition_Input_Holding : ActorBehaviourCondition
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] InputButtonType m_eInput;
        [SerializeField] bool m_bHolding = true;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override bool PassCondition(BaseActorController hBaseController)
        {
            var hInput = hBaseController.baseActorInput;
            if (hInput == null || !PassAllCondition(hBaseController))
                return false;

            return (m_bHolding == FlagUtility.HasFlagUnsafe(hInput.inputData.m_eHoldingInput, m_eInput));
        }

        #endregion
    }
}