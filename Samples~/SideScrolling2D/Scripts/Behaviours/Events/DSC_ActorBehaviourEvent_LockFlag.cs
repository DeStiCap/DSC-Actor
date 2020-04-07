using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "BehaviourEvent_LockFlag", menuName = "DSC/Actor/Behaviour/Event/Lock Flag")]
    public sealed class DSC_ActorBehaviourEvent_LockFlag : BaseActorBehaviourEvent
    {
        #region Enum

        enum LockType
        {
            Lock,
            Unlock
        }

        #endregion

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] LockType m_eLockType;
        [SerializeField] ActorLockFlag[] m_arrFlag;

#pragma warning restore 0649
        #endregion

        public override void RunEvent(BaseActorController hBaseController, BaseActorBehaviour hBaseBehaviour)
        {
            if (!hBaseController.TryGetActorData(out ActorData hActorData))
                return;

            if (!m_arrFlag.HasData())
                return;

            if (hActorData.m_hFlag)
            {
                switch (m_eLockType)
                {
                    case LockType.Lock:
                        for(int i = 0; i < m_arrFlag.Length; i++)
                        {
                            hActorData.m_hFlag.AddFlag(FlagType.Lock, m_arrFlag[i], hBaseBehaviour);
                        }
                        break;

                    case LockType.Unlock:
                        for (int i = 0; i < m_arrFlag.Length; i++)
                        {
                            hActorData.m_hFlag.RemoveFlag(FlagType.Lock, m_arrFlag[i], hBaseBehaviour);
                        }
                        break;
                }

                
            }
        }
    }
}