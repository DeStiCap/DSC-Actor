using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Actor.Behaviour2D
{
    [CreateAssetMenu(fileName = "Behaviour_Walk2D_Player", menuName = "DSC/Actor/Behaviour/2D/Player/Walk")]
    public sealed class DSC_ActorBehaviour_Walk2D_Player : ActorBehaviour
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] float m_fWalkSpeed = 10f;
        [SerializeField] ActorBehaviourValueFloat m_hWalkSpeedValue;
        [EnumMask]
        [SerializeField] DirectionType2D m_eCanMoveDirection = DirectionType2D.Up | DirectionType2D.Down | DirectionType2D.Left | DirectionType2D.Right;
        [SerializeField] bool m_bOnlyOneDirection;

        [Header("Event")]
        [SerializeField] BaseActorBehaviourEvent[] m_arrOnWalkingEvent;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnFixedUpdateBehaviour(BaseActorController hBaseController)
        {
            base.OnFixedUpdateBehaviour(hBaseController);

            if (!PassCondition(hBaseController))
                return;

            if (!hBaseController.TryGetActorData<BaseActorData2D>(out var hActorData))
                return;

            var hInput = hBaseController.baseActorInput;
            
            if (hInput == null || hActorData.m_hPhysic == null)
                return;

            float fHorizontal = hInput.inputData.m_fHorizontal;
            float fMoveSpeed = m_fWalkSpeed;
            if (m_hWalkSpeedValue)
                m_hWalkSpeedValue.CalculateValue(ref fMoveSpeed);

            Vector2 vVelocity = hActorData.m_hPhysic.velocity;
            vVelocity.x = fHorizontal * fMoveSpeed * hBaseController.actorTimeScale;
            hActorData.m_hPhysic.velocity = vVelocity;

            m_arrOnWalkingEvent.RunEvent(hBaseController,this);
        }

        #endregion
    }
}