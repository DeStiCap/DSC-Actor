using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public static class Extension_BaseActorBehaviourEvent
    {
        public static void RunEvent(this BaseActorBehaviourEvent[] arrayEvent, BaseActorController hBaseController, BaseActorBehaviour hBaseBehaviour)
        {
            if (arrayEvent == null || arrayEvent.Length <= 0)
                return;

            for(int i = 0; i < arrayEvent.Length; i++)
            {
                var hEvent = arrayEvent[i];
                if (hEvent != null)
                    hEvent.RunEvent(hBaseController,hBaseBehaviour);
            }
        }

        public static void RunEvent(this List<BaseActorBehaviourEvent> listEvent, BaseActorController hBaseController, BaseActorBehaviour hBaseBehaviour)
        {
            if (listEvent == null || listEvent.Count <= 0)
                return;

            for (int i = 0; i < listEvent.Count; i++)
            {
                var hEvent = listEvent[i];
                if (hEvent != null)
                    hEvent.RunEvent(hBaseController,hBaseBehaviour);
            }
        }
    }
}