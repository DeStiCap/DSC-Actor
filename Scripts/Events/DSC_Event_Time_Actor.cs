using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace DSC.Event.Helper
{
    public class DSC_Event_Time_Actor : MonoBehaviour
    {
        #region Enum

        protected enum SetType
        {
            Multiplier,
            Divider,
            Override,
            RemoveOverride
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [Min(0)]
        [SerializeField] protected float m_fTimeScale;
        [SerializeField] protected SetType m_eType;

#pragma warning restore 0649
        #endregion

        #endregion

        #region Events

        public void ChangeTimeScale(GameObject hActor)
        {
            if (hActor == null)
                return;

            var hBase = hActor.GetComponent<BaseActor>();
            if (hBase == null)
                return;

            switch (m_eType)
            {
                case SetType.Multiplier:
                    hBase.timeScaleMultiplier *= m_fTimeScale;
                    break;

                case SetType.Divider:
                    hBase.timeScaleMultiplier /= m_fTimeScale;
                    break;

                case SetType.Override:
                    hBase.overrideTimeScale = m_fTimeScale;
                    break;

                case SetType.RemoveOverride:
                    hBase.overrideTimeScale = null;
                    break;
            }
        }

        public void ReverseChangeTimeScale(GameObject hActor)
        {
            if (hActor == null)
                return;

            var hBase = hActor.GetComponent<BaseActor>();
            if (hBase == null)
                return;

            switch (m_eType)
            {
                case SetType.Multiplier:
                    hBase.timeScaleMultiplier /= m_fTimeScale;
                    break;

                case SetType.Divider:
                    hBase.timeScaleMultiplier *= m_fTimeScale;    
                    break;

                case SetType.Override:
                    hBase.overrideTimeScale = null;                  
                    break;

                case SetType.RemoveOverride:
                    hBase.overrideTimeScale = m_fTimeScale;
                    break;
            }
        }

        #endregion
    }
}