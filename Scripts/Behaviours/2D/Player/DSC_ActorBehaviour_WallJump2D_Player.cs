using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Actor.Behaviour2D
{
    [CreateAssetMenu(fileName = "Behaviour_WallJump2D_Player", menuName = "DSC/Actor/Behaviour/2D/Player/Wall Jump")]
    public sealed class DSC_ActorBehaviour_WallJump2D_Player : ActorBehaviour
    {
        #region Data

        class WallJumpCacheData : IActorBehaviourData, IPoolable
        {
            public UnityAction<BaseActorController> m_actJump;
            public UnityAction<BaseActorController> m_actJumpUp;
            public bool m_bWallJumping;
            public bool m_bJumpButtonUp;
            public float m_fWallJumpTimeCount;

            public void Clear()
            {
                m_bWallJumping = false;
                m_bJumpButtonUp = false;
                m_fWallJumpTimeCount = 0;
            }
        }

        #endregion

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] InputButtonType m_eButton = InputButtonType.South;

        [SerializeField] float m_fForceX = 10;
        [SerializeField] ActorBehaviourValueFloat m_hForceXValue;
        [SerializeField] float m_fForceY = 10;
        [SerializeField] ActorBehaviourValueFloat m_hForceYValue;
        [Min(0)]
        [SerializeField] float m_fLockFlagDuration;

        [Header("Event")]
        [SerializeField] BaseActorBehaviourEvent[] m_arrJumpEvent;
        [SerializeField] BaseActorBehaviourEvent[] m_arrJumpEndEvent;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(BaseActorController hBaseController)
        {
            if (DSC_Pooling.TryGetPooling(out WallJumpCacheData hOutData))
            {
                hBaseController.AddBehaviourData(hOutData);
            }
            else
            {
                hBaseController.AddBehaviourData(new WallJumpCacheData
                {
                    m_actJump = OnJump,
                    m_actJumpUp = OnJumpButtonUp
                });
            }
        }

        public override void OnStartBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetActorData(out BaseActorData2D_Player hActorData)
                && hBaseController.TryGetBehaviourData(out WallJumpCacheData hOutData))
            {
                hActorData.m_hInputButtonCallback.Add((m_eButton, GetInputType.Down), hOutData.m_actJump);
                hActorData.m_hInputButtonCallback.Add((m_eButton, GetInputType.Up), hOutData.m_actJumpUp);
            }
        }

        public override void OnStopBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetActorData(out BaseActorData2D_Player hActorData)
                && hBaseController.TryGetBehaviourData(out WallJumpCacheData hOutData))
            {
                hActorData.m_hInputButtonCallback.Remove((m_eButton, GetInputType.Down), hOutData.m_actJump);
                hActorData.m_hInputButtonCallback.Remove((m_eButton, GetInputType.Up), hOutData.m_actJumpUp);
            }
        }

        public override void OnDestroyBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetBehaviourData(out WallJumpCacheData hOutData, out int nOutIndex))
            {
                DSC_Pooling.AddPooling(hOutData);
                hBaseController.RemoveBehaviourData(nOutIndex);
            }
        }

        public override void OnUpdateBehaviour(BaseActorController hBaseController)
        {
            if (!hBaseController.TryGetBehaviourData(out WallJumpCacheData hOutData) || !hOutData.m_bWallJumping)
                return;

            hOutData.m_fWallJumpTimeCount += hBaseController.actorDeltaTime;
            float fLockFlagDuration = m_fLockFlagDuration;
            if (hOutData.m_bJumpButtonUp)
            {
                fLockFlagDuration *= 0.5f;
            }

            if (hOutData.m_fWallJumpTimeCount >= fLockFlagDuration)
            {
                EndWallJump(hBaseController, hOutData);
                return;
            }
        }

        public override void OnInterruptBehaviour(BaseActorController hBaseController)
        {
            if (!hBaseController.TryGetBehaviourData(out WallJumpCacheData hData) 
                || !hData.m_bWallJumping)
                return;

            EndWallJump(hBaseController, hData);
        }

        #endregion

        #region Main

        void OnJump(BaseActorController hBaseController)
        {
            if (hBaseController.isTimeStop || !PassCondition(hBaseController)
                || !hBaseController.TryGetActorData(out BaseActorData2D_Player hActorData)
                || !hBaseController.TryGetBehaviourData(out WallJumpCacheData hOutData)
                || hOutData.m_bWallJumping)
                return;

            if (hActorData.m_hPhysic == null)
                return;

            hOutData.m_bWallJumping = true;
            hOutData.m_bJumpButtonUp = false;
            hOutData.m_fWallJumpTimeCount = 0;

            var vVelocity = hActorData.m_hPhysic.velocity;
            vVelocity.y = 0;
            hActorData.m_hPhysic.velocity = vVelocity;

            float fForceX = m_fForceX;
            if (m_hForceXValue)
                m_hForceXValue.CalculateValue(ref fForceX);

            float fForceY = m_fForceY;
            if (m_hForceYValue)
                m_hForceYValue.CalculateValue(ref fForceY);

            Vector2 vForce = new Vector2(fForceX, fForceY);

            var fAngleY = hActorData.m_hActor.localEulerAngles.y;
            if (fAngleY == 0)
                vForce.x = -vForce.x;

            hActorData.m_hPhysic.AddForce(vForce);

            m_arrJumpEvent.RunEvent(hBaseController,this);
        }

        void OnJumpButtonUp(BaseActorController hBaseController)
        {
            if (!hBaseController.TryGetBehaviourData(out WallJumpCacheData hOutData))
                return;

            hOutData.m_bJumpButtonUp = true;
        }

        void EndWallJump(BaseActorController hBaseController, WallJumpCacheData hData)
        {
            hData.m_bWallJumping = false;
            hData.m_fWallJumpTimeCount = 0;

            m_arrJumpEndEvent.RunEvent(hBaseController,this);
        }

        #endregion
    }
}