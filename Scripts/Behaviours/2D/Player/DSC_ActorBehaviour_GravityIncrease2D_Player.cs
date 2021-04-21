using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Event;

namespace DSC.Actor.Behaviour2D
{
    [CreateAssetMenu(fileName = "Behaviour_GravityIncrease2D_Player", menuName = "DSC/Actor/Behaviour/2D/Player/Gravity Increase")]
    public sealed class DSC_ActorBehaviour_GravityIncrease2D_Player : DSC_ActorBehaviour_GravityIncrease2D
    {
        #region Data

        class GravityIncreaseButtonCacheData : IActorBehaviourData, IPoolable
        {
            public UnityAction<BaseActorController> m_actButtonDown;
            public UnityAction<BaseActorController> m_actButtonUp;

            public bool m_bStoping;
            public float m_fStopTime;

            public void Clear()
            {
                m_bStoping = false;
                m_fStopTime = 0;
            }
        }

        #endregion

        #region Variable - Inspector
#pragma warning disable 0649

        [Header("Stop Option")]
        [SerializeField] bool m_bStopByHoldButton;
        [SerializeField] InputButtonType m_eStopButton;
        [SerializeField] bool m_bHasMaxStopDuration;
        [Min(0)]
        [SerializeField] float m_fMaxStopDuration;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(BaseActorController hBaseController)
        {
            base.OnCreateBehaviour(hBaseController);

            if (DSC_Pooling.TryGetPooling(out GravityIncreaseButtonCacheData hOutCache))
            {
                hBaseController.AddBehaviourData(hOutCache);
            }
            else
            {
                hBaseController.AddBehaviourData(new GravityIncreaseButtonCacheData
                {
                    m_actButtonDown = OnButtonDown,
                    m_actButtonUp = OnButtonUp
                });
            }
        }

        public override void OnStartBehaviour(BaseActorController hBaseController)
        {
            base.OnStartBehaviour(hBaseController);

            if (hBaseController.TryGetIActorData(out IActorData_Input hDataInput)
                && hBaseController.TryGetBehaviourData(out GravityIncreaseButtonCacheData hOutData))
            {
                hDataInput.inputButtonCallback.Add((m_eStopButton, GetInputType.Down), hOutData.m_actButtonDown, EventOrder.Late);
                hDataInput.inputButtonCallback.Add((m_eStopButton, GetInputType.Up), hOutData.m_actButtonUp);
            }
        }

        public override void OnStopBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetIActorData(out IActorData_Input hDataInput)
                && hBaseController.TryGetBehaviourData(out GravityIncreaseButtonCacheData hOutData))
            {
                hDataInput.inputButtonCallback.Remove((m_eStopButton, GetInputType.Down), hOutData.m_actButtonDown, EventOrder.Late);
                hDataInput.inputButtonCallback.Remove((m_eStopButton, GetInputType.Up), hOutData.m_actButtonUp);

                hOutData.m_bStoping = false;
            }

            base.OnStopBehaviour(hBaseController);
        }

        public override void OnDestroyBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetBehaviourData(out GravityIncreaseButtonCacheData hOutData, out int nOutIndex))
            {
                DSC_Pooling.AddPooling(hOutData);
                hBaseController.RemoveBehaviourData(nOutIndex);
            }

            base.OnDestroyBehaviour(hBaseController);
        }

        public override void OnUpdateBehaviour(BaseActorController hBaseController)
        {
            base.OnUpdateBehaviour(hBaseController);

            if (!m_bHasMaxStopDuration
                || !hBaseController.TryGetBehaviourData(out GravityIncreaseButtonCacheData hOutData)
                || !hOutData.m_bStoping)
                return;

            hOutData.m_fStopTime += hBaseController.actorDeltaTime;

            if (hOutData.m_fStopTime >= m_fMaxStopDuration)
            {
                hOutData.m_bStoping = false;
            }
        }

        protected override void OnGravityIncrease(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetBehaviourData(out GravityIncreaseButtonCacheData hOutData))
            {
                if (hOutData.m_bStoping)
                    return;
            }

            base.OnGravityIncrease(hBaseController);
        }

        #endregion

        #region Main

        void OnButtonDown(BaseActorController hBaseController)
        {
            if (!m_bStopByHoldButton)
                return;

            if (!hBaseController.TryGetBehaviourData(out GravityIncreaseButtonCacheData hOutData))
                return;

            hOutData.m_bStoping = true;
            hOutData.m_fStopTime = 0;
        }

        void OnButtonUp(BaseActorController hBaseController)
        {
            if (!m_bStopByHoldButton)
                return;

            if (!hBaseController.TryGetBehaviourData(out GravityIncreaseButtonCacheData hOutData))
                return;

            hOutData.m_bStoping = false;
        }

        #endregion
    }
}