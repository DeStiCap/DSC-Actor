using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Actor
{
    public class DSC_ActorController_Lite : BaseActor
    {
        #region Variable

        #region Variable - Property

        public override BaseActorData baseActorData { get; protected set; }

        public override float? overrideTimeScale
        {
            get
            {
                return m_fOverrideTimeScale;
            }

            set
            {
                if (value == m_fOverrideTimeScale)
                    return;

                float fPreviousTimeScale = actorTimeScale;

                m_fOverrideTimeScale = value;

                float fCurrentTimeScale = actorTimeScale;

                if(fPreviousTimeScale != fCurrentTimeScale)
                    m_hOnTimeScaleChange?.Invoke(fCurrentTimeScale);
            }
        }

        public override float timeScaleMultiplier
        {
            get { return m_fTimeScaleMultiplier; }

            set
            {
                if (value < 0)
                    value = 0;

                if (m_fTimeScaleMultiplier == value)
                    return;

                m_fTimeScaleMultiplier = value;
                m_hOnTimeScaleChange?.Invoke(actorTimeScale);
            }
        }

        public override event UnityAction<float> onTimeScaleChange
        {
            add
            {
                m_hOnTimeScaleChange += value;
            }

            remove
            {
                m_hOnTimeScaleChange -= value;
            }
        }

        #endregion

        protected float? m_fOverrideTimeScale;
        protected float m_fTimeScaleMultiplier = 1;
        protected UnityAction<float> m_hOnTimeScaleChange;

        #endregion

        #region Events - Callback

        protected override void OnTimeScaleChange(float fTimeScale)
        {
            if (m_fOverrideTimeScale != null)
                return;

            m_hOnTimeScaleChange?.Invoke(fTimeScale);
        }

        #endregion
    }
}