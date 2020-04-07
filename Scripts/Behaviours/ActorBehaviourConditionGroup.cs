using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    [CreateAssetMenu(fileName = "BehaviourConditionGroup", menuName = "DSC/Actor/Behaviour/Condition/Condition Group")]
    public class ActorBehaviourConditionGroup : BaseActorBehaviourConditionGroup
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] BaseActorBehaviourCondition[] m_arrCondition;
        [SerializeField] bool m_bAllTrue = true;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        protected override BaseActorBehaviourCondition[] conditionArray { get { return m_arrCondition; } }
        protected override bool allTrue { get { return m_bAllTrue; } }

        #endregion

        #endregion
    }
}