using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Actor.Behaviour2D
{
    [CreateAssetMenu(fileName = "Behaviour_DoubleJump2D_Player", menuName = "DSC/Actor/Behaviour/2D/Player/Double Jump")]
    public sealed class DSC_ActorBehaviour_DoubleJump2D_Player : ActorBehaviour
    {
        #region Data

        class DoubleJumpCacheData : IActorBehaviourData, IPoolable
        {
            public UnityAction<BaseActorController> m_actJump;
            public bool m_bCanDoubleJump;

            public void Clear()
            {
                m_bCanDoubleJump = false;
            }
        }

        #endregion

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] InputButtonType m_eButton = InputButtonType.South;
        [SerializeField] float m_fJumpForce = 10;
        [SerializeField] ActorBehaviourValueFloat m_hJumpForceValue;
        [SerializeField] BaseActorBehaviourCondition[] m_arrResetCondition;

        [Header("Event")]
        [SerializeField] BaseActorBehaviourEvent[] m_arrJumpEvent;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(BaseActorController hBaseController)
        {
            if (DSC_Pooling.TryGetPooling(out DoubleJumpCacheData hOutCache))
            {
                hBaseController.AddBehaviourData(hOutCache);
            }
            else
            {
                hBaseController.AddBehaviourData(new DoubleJumpCacheData
                {
                    m_actJump = OnJump
                });
            }
        }

        public override void OnStartBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetActorData(out BaseActorData2D_Player hActorData)
                && hBaseController.TryGetBehaviourData(out DoubleJumpCacheData hOutData))
            {
                hActorData.m_hInputButtonCallback.Add((m_eButton, GetInputType.Down), hOutData.m_actJump);
            }
        }

        public override void OnStopBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetActorData(out BaseActorData2D_Player hActorData)
                && hBaseController.TryGetBehaviourData(out DoubleJumpCacheData hOutData))
            {
                hActorData.m_hInputButtonCallback.Remove((m_eButton, GetInputType.Down), hOutData.m_actJump);
            }
        }

        public override void OnDestroyBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetBehaviourData(out DoubleJumpCacheData hOutData, out int nOutIndex))
            {
                DSC_Pooling.AddPooling(hOutData);
                hBaseController.RemoveBehaviourData(nOutIndex);
            }
        }

        public override void OnFixedUpdateBehaviour(BaseActorController hBaseController)
        {
            if (!hBaseController.TryGetBehaviourData(out DoubleJumpCacheData hOutData))
                return;

            if (!hOutData.m_bCanDoubleJump)
            {
                hOutData.m_bCanDoubleJump = m_arrResetCondition.PassCondition(hBaseController);
            }
        }

        #endregion

        #region Main

        void OnJump(BaseActorController hBaseController)
        {
            if (hBaseController.isTimeStop || !PassCondition(hBaseController))
                return;

            if (!hBaseController.TryGetActorData(out BaseActorData2D_Player hActorData)
                || !hBaseController.TryGetBehaviourData(out DoubleJumpCacheData hOutData) || !hOutData.m_bCanDoubleJump)
                return;

            var hInput = hBaseController.baseActorInput;

            if (hInput == null || hActorData.m_hPhysic == null)
                return;

            hOutData.m_bCanDoubleJump = false;

            var vVelocity = hActorData.m_hPhysic.velocity;
            vVelocity.y = 0;
            hActorData.m_hPhysic.velocity = vVelocity;

            float fJumpForce = m_fJumpForce;
            if (m_hJumpForceValue)
                m_hJumpForceValue.CalculateValue(ref fJumpForce);

            hActorData.m_hPhysic.AddForce(new Vector2(0, fJumpForce));

            m_arrJumpEvent.RunEvent(hBaseController,this);
        }

        #endregion
    }
}