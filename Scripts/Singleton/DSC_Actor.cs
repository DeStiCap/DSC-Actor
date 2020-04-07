using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class DSC_Actor : MonoBehaviour
    {
        protected static DSC_Actor baseInstance { get; set; }

        public static void RegisterActor(Transform hActor)
        {
            if (baseInstance == null)
            {
                return;
            }

            baseInstance.MainRegisterActor(hActor);
        }

        public static void RegisterActor(BaseActorData hData)
        {
            if (baseInstance == null)
            {
                return;
            }

            baseInstance.MainRegisterActor(hData);
        }

        protected abstract void MainRegisterActor(Transform hActor);
        protected abstract void MainRegisterActor(BaseActorData hData);

        public static void UnregisterActor(Transform hActor)
        {
            if (baseInstance == null)
            {
                return;
            }

            baseInstance.MainUnregisterActor(hActor);
        }

        public static void UnregisterActor<ActorData>(ActorData hData) where ActorData : BaseActorData
        {
            if (baseInstance == null)
            {
                return;
            }

            baseInstance.MainUnregisterActor(hData);
        }

        protected abstract void MainUnregisterActor(Transform hActor);
        protected abstract void MainUnregisterActor<ActorData>(ActorData hData) where ActorData : BaseActorData;

        public static ActorData GetActorData<ActorData>(Transform hActor) where ActorData : BaseActorData
        {
            if (baseInstance == null)
            {
                ShowNoManagerWarning();
                return null;
            }

            return baseInstance.MainGetActorData<ActorData>(hActor);
        }

        protected abstract ActorData MainGetActorData<ActorData>(Transform hActor) where ActorData : BaseActorData;

        public static bool TryGetActorData<ActorData>(Transform hActor, out ActorData hOutData) where ActorData : BaseActorData
        {
            if (baseInstance == null)
            {
                ShowNoManagerWarning();
                hOutData = null;
                return false;
            }

            return baseInstance.MainTryGetActorData(hActor, out hOutData);
        }

        protected abstract bool MainTryGetActorData<ActorData>(Transform hActor, out ActorData hOutData) where ActorData : BaseActorData;

        #region Helper

        protected static void ShowNoManagerWarning()
        {
            Debug.LogWarning("Don't have Actor Manager in scene.");
        }

        #endregion
    }
}