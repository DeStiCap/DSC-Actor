using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;
using DSC.Event;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "ActorDataBlueprint", menuName = "DSC/Actor/Data/Blueprint/Actor Data")]
    public class ActorData : BaseActorData2D, IActorData_Input
    {
        #region Variable

        #region Variable - Property

        public BaseActorInput baseInput { get; protected set; }

        public EventCallback<(InputButtonType, GetInputType), BaseActorController> inputButtonCallback { get; protected set; }


        #endregion

        public AudioSource m_hAudio;

        public DSC_ActorController m_hController;
        public DSC_ActorStatus m_hStatus;
        public DSC_ActorFlag m_hFlag;
        public DSC_ActorDamageable m_hDamageable;

        public ActorStateFlag m_eStateFlag;
        public ActorLockFlag m_eLockFlag;

        public float m_fDeltaTime;

        #endregion

        public override void Init(Transform hActor)
        {
            base.Init(hActor);

            if (hActor == null)
                return;

            m_hController = hActor.GetComponent<DSC_ActorController>();

            m_hAudio = m_hActor.GetComponent<AudioSource>();

            m_hStatus = m_hActor.GetComponent<DSC_ActorStatus>();
            m_hFlag = m_hActor.GetComponent<DSC_ActorFlag>();
            m_hDamageable = m_hActor.GetComponent<DSC_ActorDamageable>();
        }

        public void InitInput(BaseActorInput hBaseInput)
        {
            baseInput = hBaseInput;

            inputButtonCallback = new EventCallback<(InputButtonType, GetInputType), BaseActorController>();
        }
    }
}