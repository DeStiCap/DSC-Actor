using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor.Behaviour2D
{
    [CreateAssetMenu(fileName = "Behaviour_FacingFlip_Player", menuName = "DSC/Actor/Behaviour/2D/Player/Facing Flip")]
    public sealed class DSC_ActorBehaviour_FacingFlip_Player : ActorBehaviour
    {
        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] bool m_bRightDefault = true;

        [Header("Event")]
        [SerializeField] BaseActorBehaviourEvent[] m_arrFlipEvent;

#pragma warning restore 0649
        #endregion

        #region Base - Override

        public override void OnUpdateBehaviour(BaseActorController hBaseController)
        {
            var hInput = hBaseController.baseActorInput;

            if (!hBaseController.TryGetActorData(out BaseActorData2D_Player hActorData)
                || hInput == null || !PassCondition(hBaseController))
                return;

            float fHorizontal = hInput.inputData.m_fHorizontal;
            if (fHorizontal == 0)
                return;

            Vector3 vAngle = hActorData.m_hActor.localEulerAngles;
            float fAngle = 0;

            if (fHorizontal > 0)
            {
                fAngle = m_bRightDefault ? 0 : 180;
            }
            else if (fHorizontal < 0)
            {
                fAngle = m_bRightDefault ? 180 : 0;              
            }

            if (vAngle.y != fAngle)
            {
                vAngle.y = fAngle;
                hActorData.m_hActor.localEulerAngles = vAngle;
                m_arrFlipEvent.RunEvent(hBaseController,this);
            }
        }

        #endregion
    }
}