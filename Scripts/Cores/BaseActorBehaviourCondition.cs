using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorBehaviourCondition : ScriptableObject
    {
        /// <summary>
        /// Check condition, return true when pass and return false when fail.
        /// </summary>
        public abstract bool PassCondition(BaseActorController hBaseController);
    }

    public abstract class ActorBehaviourCondition : BaseActorBehaviourCondition
    {
        [SerializeField] protected BaseActorBehaviourCondition[] m_arrCondition;

        public override bool PassCondition(BaseActorController hBaseController)
        {
            return PassAllCondition(hBaseController);
        }

        protected bool PassAllCondition(BaseActorController hBaseController)
        {
            return m_arrCondition.PassCondition(hBaseController);
        }
    }
}