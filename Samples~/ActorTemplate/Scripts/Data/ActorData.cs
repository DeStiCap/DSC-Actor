using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;
using DSC.Event;

namespace DSC.Template.Actor.Default
{
    public class ActorData : BaseActorData
    {
        public DSC_ActorController m_hController;
        public DSC_ActorStatus m_hStatus;

        public BaseActorInput m_hInput;

        public ActorStateFlag m_eStateFlag;
        public EventCallback<(InputButtonType, GetInputType), ActorData, List<IActorBehaviourData>> m_hInputButtonCallback;

        public float m_fDeltaTime;

        public override void Init(Transform hActor)
        {
            base.Init(hActor);

            if (hActor == null)
                return;

            m_hController = hActor.GetComponent<DSC_ActorController>();

            m_hStatus = m_hActor.GetComponent<DSC_ActorStatus>();
        }
    }
}