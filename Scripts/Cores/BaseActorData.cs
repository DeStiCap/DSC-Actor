using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;
using DSC.Event;

namespace DSC.Actor
{
    public class BaseActorData : ScriptableObject
    {
        public Transform m_hActor;

        public virtual void Init(Transform hActor)
        {
            m_hActor = hActor;
        }
    }

    public class BaseActorData2D : BaseActorData
    {
        public BaseActorPhysic2D m_hPhysic;

        public override void Init(Transform hActor)
        {
            base.Init(hActor);

            if (hActor == null)
                return;

            m_hPhysic = hActor.GetComponent<BaseActorPhysic2D>();
        }
    }
}