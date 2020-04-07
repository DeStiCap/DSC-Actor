using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Actor
{
    public class DSC_ActorStatus : BaseActorStatus
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected BaseStatusData m_hStatusBlueprint;

        [ReadOnlyField]
        [SerializeField] protected BaseStatusData m_hStatus;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override BaseStatusData baseStatusData { get { return m_hStatus; } protected set { m_hStatus = value; } }
        public override BaseStatusData statusDataBlueprint { get { return m_hStatusBlueprint; } }

        #endregion

        #endregion
    }
}