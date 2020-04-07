using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Actor
{
    public class DSC_ActorController : BaseActorController
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected BaseActorData m_hDataBlueprint;
        [SerializeField] protected List<BaseActorBehaviour> m_lstBehaviour;

        [Header("Pre Processing")]
        [SerializeField] protected List<BaseUpdateMono> m_lstPreUpdate;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public override BaseActorData baseActorData { get { return m_hBaseData; } protected set { m_hBaseData = value; } }
        public override BaseActorData actorDataBlueprint { get { return m_hDataBlueprint; } }

        protected override List<BaseActorBehaviour> listBehaviour { get { return m_lstBehaviour; } set { m_lstBehaviour = value; } }

        protected override List<IActorBehaviourData> listBehaviourData { get { return m_lstBehaviourData; } set { m_lstBehaviourData = value; } }

        protected override List<BaseActorBehaviour> listBehaviourRunning { get; set; }

        protected override List<BaseUpdateMono> listPreUpdateProcessing { get { return m_lstPreUpdate; } }

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

                if (fPreviousTimeScale != fCurrentTimeScale)
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

        protected BaseActorData m_hBaseData;

        protected List<IActorBehaviourData> m_lstBehaviourData = new List<IActorBehaviourData>();

        protected float? m_fOverrideTimeScale;

        protected float m_fTimeScaleMultiplier = 1;

        protected UnityAction<float> m_hOnTimeScaleChange;

        #endregion

        #region Base - Mono

        protected override void Awake()
        {
            base.Awake();

            if (!DSC_Actor.TryGetActorData(transform, out m_hBaseData))
            {
                InitActorData();
            }



            CreateAllBehaviour();
        }

        protected virtual void OnEnable()
        {
            StartAllBehaviour();
        }

        protected virtual void OnDisable()
        {
            StopAllBehaviour();
        }

        protected override void OnDestroy()
        {
            DestroyAllBehaviour();

            base.OnDestroy();
        }

        protected virtual void Update()
        {
            if (m_lstPreUpdate.HasData())
                m_lstPreUpdate.OnUpdate();

            ExecuteBehaviour();
        }

        protected virtual void FixedUpdate()
        {
            FixedExecuteBehaviour();
        }

        protected virtual void LateUpdate()
        {
            LateExecuteBehaviour();
        }

        #endregion

        #region Base - Override

        public override void RegisterBaseInput(BaseActorInput hBaseInput)
        {
            base.RegisterBaseInput(hBaseInput);

            m_hBaseData.m_hInput = hBaseInput;
        }

        #endregion

        #region Events

        #region Events - Callback

        protected override void OnTimeScaleChange(float fTimeScale)
        {
            if (m_fOverrideTimeScale != null)
                return;

            m_hOnTimeScaleChange?.Invoke(fTimeScale);
        }

        protected override void OnRunEventInput(InputButtonType eButtonType, GetInputType eGetType)
        {
            m_hBaseData.m_hInputButtonCallback.Run((eButtonType, eGetType), this);
        }

        #endregion

        #endregion
    }
}