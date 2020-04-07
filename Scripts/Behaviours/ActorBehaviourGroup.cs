using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Actor
{
    [CreateAssetMenu(fileName = "BehaviourGroup", menuName = "DSC/Actor/Behaviour Group")]
    public class ActorBehaviourGroup : ActorBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected BaseActorBehaviour[] m_arrBehaviour;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(BaseActorController hBaseController)
        {
            base.OnCreateBehaviour(hBaseController);

            if (!m_arrBehaviour.HasData())
                return;

            for (int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnCreateBehaviour(hBaseController);
            }
        }

        public override void OnDestroyBehaviour(BaseActorController hBaseController)
        {
            base.OnDestroyBehaviour(hBaseController);

            if (!m_arrBehaviour.HasData())
                return;

            for (int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnDestroyBehaviour(hBaseController);
            }
        }

        public override void OnStartBehaviour(BaseActorController hBaseController)
        {
            if (!m_arrBehaviour.HasData())
                return;

            for (int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnStartBehaviour(hBaseController);
            }
        }

        public override void OnStopBehaviour(BaseActorController hBaseController)
        {
            if (!m_arrBehaviour.HasData())
                return;

            for (int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnStopBehaviour(hBaseController);
            }
        }

        public override void OnUpdateBehaviour(BaseActorController hBaseController)
        {
            base.OnUpdateBehaviour(hBaseController);

            if (!m_arrBehaviour.HasData() || !PassCondition(hBaseController))
                return;

            for (int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnUpdateBehaviour(hBaseController);
            }
        }

        public override void OnFixedUpdateBehaviour(BaseActorController hBaseController)
        {
            base.OnFixedUpdateBehaviour(hBaseController);

            if (!m_arrBehaviour.HasData() || !PassCondition(hBaseController))
                return;

            for (int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnFixedUpdateBehaviour(hBaseController);
            }
        }

        public override void OnLateUpdateBehaviour(BaseActorController hBaseController)
        {
            base.OnLateUpdateBehaviour(hBaseController);

            if (!m_arrBehaviour.HasData() || !PassCondition(hBaseController))
                return;

            for (int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnLateUpdateBehaviour(hBaseController);
            }
        }

        public override void OnInterruptBehaviour(BaseActorController hBaseController)
        {
            if (!m_arrBehaviour.HasData())
                return;

            for (int i = 0; i < m_arrBehaviour.Length; i++)
            {
                var hBehaviour = m_arrBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnInterruptBehaviour(hBaseController);
            }
        }

        #endregion
    }
}