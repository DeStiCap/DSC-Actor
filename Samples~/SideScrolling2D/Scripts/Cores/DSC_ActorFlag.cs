using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [RequireComponent(typeof(DSC_ActorController))]
    public class DSC_ActorFlag : BaseActorFlag<FlagType>
    {
        #region Variable

        #region Variable - Property

        protected override Dictionary<FlagType, Dictionary<int, FlagData>> dicCurrentSetFlag { get; set; }

        #endregion

        DSC_ActorController m_hActorController;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hActorController = GetComponent<DSC_ActorController>();
        }

        #endregion

        #region Base - Override

        protected override void MainAddFlag(FlagType eType, int nFlag, Object hObject)
        {
            if (!(m_hActorController.baseActorData is ActorData))
                return;

            var hActorData = (ActorData)m_hActorController.baseActorData;

            switch (eType)
            {
                case FlagType.Lock:
                    hActorData.m_eLockFlag |= (ActorLockFlag)nFlag;
                    break;
            }
        }

        protected override void MainRemoveFlag(FlagType eType, int nFlag, Object hObject)
        {
            if (!(m_hActorController.baseActorData is ActorData))
                return;

            var hActorData = (ActorData)m_hActorController.baseActorData;

            switch (eType)
            {
                case FlagType.Lock:
                    hActorData.m_eLockFlag &= ~(ActorLockFlag)nFlag;
                    break;
            }
        }

        #endregion

        #region Events

        public virtual void SetIsGrounding(bool bGrounding)
        {
            if (!m_hActorController.TryGetActorData(out ActorData hData))
                return;

            if (bGrounding)
            {
                hData.m_eStateFlag |= ActorStateFlag.IsGrounding;
                hData.m_eStateFlag &= ~ActorStateFlag.Falling;
            }
            else
                hData.m_eStateFlag &= ~ActorStateFlag.IsGrounding;
        }

        public virtual void SetIsWalling(bool bWalling)
        {
            if (!m_hActorController.TryGetActorData(out ActorData hData))
                return;

            if (bWalling)
                hData.m_eStateFlag |= ActorStateFlag.IsWalling;
            else
                hData.m_eStateFlag &= ~ActorStateFlag.IsWalling;
        }

        #endregion
    }
}