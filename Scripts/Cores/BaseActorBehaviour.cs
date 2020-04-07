using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorBehaviour : ScriptableObject
    {
        /// <summary>
        /// ID of behaviour type. For use to change,remove this behaviour type from data.
        /// </summary>
        public abstract int behaviourTypeID { get; }

        /// <summary>
        /// Check all condition, return true when pass and return false when fail.
        /// </summary>
        protected abstract bool PassCondition(BaseActorController hBaseController);
        public abstract void OnCreateBehaviour(BaseActorController hBaseController);
        public abstract void OnDestroyBehaviour(BaseActorController hBaseController);
        public abstract void OnStartBehaviour(BaseActorController hBaseController);
        public abstract void OnUpdateBehaviour(BaseActorController hBaseController);
        public abstract void OnFixedUpdateBehaviour(BaseActorController hBaseController);
        public abstract void OnLateUpdateBehaviour(BaseActorController hBaseController);
        public abstract void OnStopBehaviour(BaseActorController hBaseController);
        public abstract void OnInterruptBehaviour(BaseActorController hBaseController);
    }

    public class ActorBehaviour : BaseActorBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] BaseActorBehaviourCondition[] m_arrCondition;

#pragma warning restore 0649
        #endregion

        #endregion

        public override int behaviourTypeID { get; }

        #region Base - Override

        protected override bool PassCondition(BaseActorController hBaseController)
        {
            return m_arrCondition.PassCondition(hBaseController);
        }

        public override void OnCreateBehaviour(BaseActorController hBaseController)
        {

        }

        public override void OnDestroyBehaviour(BaseActorController hBaseController)
        {

        }

        public override void OnFixedUpdateBehaviour(BaseActorController hBaseController)
        {

        }

        public override void OnInterruptBehaviour(BaseActorController hBaseController)
        {

        }

        public override void OnLateUpdateBehaviour(BaseActorController hBaseController)
        {

        }

        public override void OnStartBehaviour(BaseActorController hBaseController)
        {

        }

        public override void OnStopBehaviour(BaseActorController hBaseController)
        {

        }

        public override void OnUpdateBehaviour(BaseActorController hBaseController)
        {

        }

        #endregion
    }
}