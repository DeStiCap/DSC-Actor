using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Actor
{
    public abstract class BaseActorDamageable<ActorData, DamageData, DamageEvent, DamageBehaviour, DamageBehaviourType> : MonoBehaviour, IDamageable<DamageData>
        where ActorData : BaseActorData
        where DamageData : struct
        where DamageEvent : BaseActorDamageEvent<ActorData, DamageData>
        where DamageBehaviour : BaseActorDamageBehaviour<ActorData,DamageBehaviourType>
        where DamageBehaviourType : System.Enum
    {
        protected abstract BaseActorData baseActorData { get; }
        protected abstract List<DamageEvent> listTakeDamageEvent { get; }
        protected abstract List<DamageEvent> listDeadEvent { get; }
        protected abstract List<DamageBehaviour> listDamageBehaviour { get; }
        protected abstract List<IActorBehaviourData> listBehaviourData { get; }

        public abstract bool TakeDamage(DamageData hData);

        protected abstract void Dead(DamageData hData);

        /*
        #region Base - Mono

        protected virtual void Update()
        {
            ExecuteAllDamageBehaviour();
        }

        protected virtual void FixedUpdate()
        {
            FixedExecuteAllDamageBehaviour();
        }

        protected virtual void LateUpdate()
        {
            LateExecuteAllDamageBehaviour();
        }

        #endregion

        #region Main

        public void StartDamageBehaviour(DamageBehaviour hBehaviour)
        {
            if (hBehaviour == null)
                return;

            hBehaviour.OnStart(actorData, listBehaviourData);
            listDamageBehaviour.Add(hBehaviour);
        }

        public void StartDamageBehaviour(DamageBehaviour[] arrBehaviour)
        {
            if (!arrBehaviour.HasData())
                return;

            for(int i = 0; i < arrBehaviour.Length; i++)
            {
                var hBehaviour = arrBehaviour[i];
                if(hBehaviour != null)
                {
                    hBehaviour.OnStart(actorData, listBehaviourData);
                    listDamageBehaviour.Add(hBehaviour);
                }
            }
        }

        public void EndDamageBehaviour(DamageBehaviour hBehaviour)
        {
            if (hBehaviour == null)
                return;

            hBehaviour.OnEnd(actorData, listBehaviourData);
            listDamageBehaviour.Remove(hBehaviour);
        }

        public void EndAllDamageBehaviour()
        {
            if (!listDamageBehaviour.HasData())
                return;

            for (int i = 0; i < listDamageBehaviour.Count; i++)
            {
                var hBehaviour = listDamageBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnEnd(actorData, listBehaviourData);
            }

            listDamageBehaviour.Clear();
        }

        protected void ExecuteAllDamageBehaviour()
        {
            if (!listDamageBehaviour.HasData())
                return;

            for(int i = 0; i < listDamageBehaviour.Count; i++)
            {
                var hBehaviour = listDamageBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnUpdate(actorData,listBehaviourData);
            }
        }

        protected void FixedExecuteAllDamageBehaviour()
        {
            if (!listDamageBehaviour.HasData())
                return;

            for (int i = 0; i < listDamageBehaviour.Count; i++)
            {
                var hBehaviour = listDamageBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnFixedUpdate(actorData, listBehaviourData);
            }
        }
        
        protected void LateExecuteAllDamageBehaviour()
        {
            if (!listDamageBehaviour.HasData())
                return;

            for (int i = 0; i < listDamageBehaviour.Count; i++)
            {
                var hBehaviour = listDamageBehaviour[i];
                if (hBehaviour != null)
                    hBehaviour.OnEnd(actorData, listBehaviourData);
            }
        }

        #endregion

        #region Helper

        protected void RunAllTakeDamageEvent(DamageData hData)
        {
            if (listTakeDamageEvent != null && listTakeDamageEvent.Count > 0)
            {
                for (int i = 0; i < listTakeDamageEvent.Count; i++)
                {
                    var hTakeDamageEvent = listTakeDamageEvent[i];
                    if (hTakeDamageEvent != null)
                        hTakeDamageEvent.RunEvent(actorData, hData);
                }
            }
        }

        protected void RunAllDeadEvent(DamageData hData)
        {
            if(listDeadEvent != null && listDeadEvent.Count > 0)
            {
                for(int i = 0; i < listDeadEvent.Count; i++)
                {
                    var hDeadEvent = listDeadEvent[i];
                    if (hDeadEvent != null)
                        hDeadEvent.RunEvent(actorData, hData);
                }
            }
        }

        #endregion

    */
    }
}