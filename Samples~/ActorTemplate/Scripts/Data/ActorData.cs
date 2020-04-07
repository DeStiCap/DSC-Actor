using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Actor;
using DSC.Event;

namespace DSC.Template.Actor.Default
{
    public class ActorData : BaseActorData, IActorData_Input
    {
        #region Variable

        #region Variable - Property

        public BaseActorInput baseInput { get; protected set; }

        public EventCallback<(InputButtonType, GetInputType), BaseActorController> inputButtonCallback { get; protected set; }


        #endregion

        public DSC_ActorController m_hController;
        public DSC_ActorStatus m_hStatus;

        public ActorStateFlag m_eStateFlag;

        public float m_fDeltaTime;


        #endregion

        public override void Init(Transform hActor)
        {
            base.Init(hActor);

            if (hActor == null)
                return;

            m_hController = hActor.GetComponent<DSC_ActorController>();

            m_hStatus = m_hActor.GetComponent<DSC_ActorStatus>();
        }

        public void InitInput(BaseActorInput hBaseInput)
        {
            baseInput = hBaseInput;
            inputButtonCallback = new EventCallback<(InputButtonType, GetInputType), BaseActorController>();
        }
    }
}