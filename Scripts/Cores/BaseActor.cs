using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Actor
{
    public abstract class BaseActor : MonoBehaviour
    {
        #region Variable - Property
        public abstract BaseActorData baseActorData { get; protected set; }
        public abstract float? overrideTimeScale { get; set; }
        public abstract float timeScaleMultiplier { get; set; }
        public abstract event UnityAction<float> onTimeScaleChange;

        public virtual float actorTimeScale
        {
            get
            {
                return overrideTimeScale == null ? DSC_Time.timeScale * timeScaleMultiplier : (float)overrideTimeScale * timeScaleMultiplier;
            }
        }

        
        public virtual float actorDeltaTime
        {
            get
            {
                return overrideTimeScale == null ? DSC_Time.deltaTime * timeScaleMultiplier : Time.unscaledDeltaTime * (float)overrideTimeScale * timeScaleMultiplier;
            }
        }

        public virtual float actorFixedDeltaTime
        {
            get
            {
                return overrideTimeScale == null ? DSC_Time.fixedDeltaTime * timeScaleMultiplier : Time.fixedUnscaledDeltaTime * (float)overrideTimeScale * timeScaleMultiplier;
            }
        }

        /// <summary>
        /// Check is time stop (or rewind) now
        /// </summary>
        public bool isTimeStop
        {
            get
            {
                return (actorTimeScale <= 0);
            }
        }

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            InitActorData();

            if (baseActorData != null)
            {
                DSC_Actor.RegisterActor(baseActorData);
            }
            else
            {
                DSC_Actor.RegisterActor(transform);
            }

            DSC_Time.onTimeScaleChange += OnTimeScaleChange;
        }

        protected virtual void OnDestroy()
        {
            DSC_Time.onTimeScaleChange -= OnTimeScaleChange;
            DSC_Actor.UnregisterActor(transform);
        }

        #endregion

        #region Base

        protected virtual void InitActorData()
        {

        }

        protected abstract void OnTimeScaleChange(float fTimeScale);

        #endregion
    }
}