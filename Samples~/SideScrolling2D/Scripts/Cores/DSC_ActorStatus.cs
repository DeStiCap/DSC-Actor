using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    public class DSC_ActorStatus : BaseActorStatus<ActorStatus>
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected ActorStatus m_hStatus;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override ActorStatus status { get { return m_hStatus; } }

        #endregion

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            if (m_hStatus == null)
                m_hStatus = new ActorStatus();

            m_hStatus.m_nCurrentHp = m_hStatus.m_nMaxHp;
        }

        #endregion
    }
}