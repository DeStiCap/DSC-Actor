using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Actor.Behaviour2D
{
    [CreateAssetMenu(fileName = "Behaviour_Jump2D_Player", menuName = "DSC/Actor/Behaviour/2D/Player/Jump")]
    public sealed class DSC_ActorBehaviour_Jump2D_Player : ActorBehaviour
    {
        #region Data

        class JumpCacheData : IActorBehaviourData, IPoolable
        {
            public UnityAction<BaseActorController> m_actJump;

            public void Clear()
            {

            }
        }

        #endregion

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] InputButtonType m_eButton = InputButtonType.South;
        [SerializeField] float m_fJumpForce = 10;
        [SerializeField] ActorBehaviourValueFloat m_hJumpForceValue;

        [Header("Event")]
        [SerializeField] BaseActorBehaviourEvent[] m_arrJumpEvent;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnCreateBehaviour(BaseActorController hBaseController)
        {
            if (DSC_Pooling.TryGetPooling(out JumpCacheData hOutData))
            {
                hBaseController.AddBehaviourData(hOutData);
            }
            else
            {
                hBaseController.AddBehaviourData(new JumpCacheData
                {
                    m_actJump = OnJump
                });
            }
        }

        public override void OnStartBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetIActorData(out IActorData_Input hDataInput) 
                && hBaseController.TryGetBehaviourData(out JumpCacheData hOutData))
            {
                hDataInput.inputButtonCallback.Add((m_eButton, GetInputType.Down), hOutData.m_actJump);
            }
        }

        public override void OnStopBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetIActorData(out IActorData_Input hDataInput)
                && hBaseController.TryGetBehaviourData(out JumpCacheData hOutData))
            {
                hDataInput.inputButtonCallback.Remove((m_eButton, GetInputType.Down), hOutData.m_actJump);
            }
        }

        public override void OnDestroyBehaviour(BaseActorController hBaseController)
        {
            if (hBaseController.TryGetBehaviourData(out JumpCacheData hOutData, out int nOutIndex))
            {
                DSC_Pooling.AddPooling(hOutData);
                hBaseController.RemoveBehaviourData(nOutIndex);
            }
        }

        #endregion

        #region Main

        void OnJump(BaseActorController hBaseController)
        {
            if (hBaseController.isTimeStop || !PassCondition(hBaseController))
                return;

            if (!hBaseController.TryGetActorData(out BaseActorData2D hActorData))
                return;

            var hInput = hBaseController.baseActorInput;

            if (hInput == null || hActorData.m_hPhysic == null)
                return;

            float fJumpForce = m_fJumpForce;
            if (m_hJumpForceValue)
                m_hJumpForceValue.CalculateValue(ref fJumpForce);
            
            hActorData.m_hPhysic.AddForce(new Vector2(0, fJumpForce));

            m_arrJumpEvent.RunEvent(hBaseController,this);
        }

        #endregion
    }
}