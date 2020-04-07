using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;
using DSC.Event;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName ="ActorDataBlueprint",menuName = "DSC/Actor/Data/Blueprint/Actor Data")]
    public class ActorData : BaseActorData2D_Player
    {
        public AudioSource m_hAudio;

        public DSC_ActorController m_hController;
        public DSC_ActorStatus m_hStatus;
        public DSC_ActorFlag m_hFlag;
        public DSC_ActorDamageable m_hDamageable;

        public BaseActorInput m_hInput;

        public ActorStateFlag m_eStateFlag;
        public ActorLockFlag m_eLockFlag;

        public float m_fDeltaTime;

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
    }
}