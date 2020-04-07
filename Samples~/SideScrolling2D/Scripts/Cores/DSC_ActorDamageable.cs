using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;
using DSC.Actor;
using DSC.Event;

namespace DSC.Template.Actor.SideScrolling2D
{
    [RequireComponent(typeof(DSC_ActorController))]
    public class DSC_ActorDamageable : BaseActorDamageable<ActorData,DamageData,DSC_ActorDamageEvent,DSC_ActorDamageBehaviour,DamageBehaviourType>
    {
        #region Enum

        protected enum DamageableEvent
        {
            TakeDamage,
            Dead,
        }

        #endregion

        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] List<DSC_ActorDamageEvent> m_lstTakeDamageEvent;
        [SerializeField] List<DSC_ActorDamageEvent> m_lstDeadEvent;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        protected override BaseActorData baseActorData
        {
            get
            {
                if (m_hActorControler == null)
                    m_hActorControler = GetComponent<DSC_ActorController>();

                if (m_hActorControler == null)
                    return null;

                return m_hActorControler.baseActorData;
            }
        }

        protected override List<DSC_ActorDamageEvent> listTakeDamageEvent { get { return m_lstTakeDamageEvent; } }
        protected override List<DSC_ActorDamageEvent> listDeadEvent { get { return m_lstDeadEvent; } }
        protected override List<DSC_ActorDamageBehaviour> listDamageBehaviour { get { return m_lstDamageBehaviour; } }
        protected override List<IActorBehaviourData> listBehaviourData { get { return m_lstBehaviourData; } }

        #endregion

        protected DSC_ActorController m_hActorControler;
        protected BaseActorStatus m_hBaseActorStatus;

        protected EventCallback<DamageableEvent,DamageData> m_hDamageableEvent = new EventCallback<DamageableEvent, DamageData>();

        protected List<DSC_ActorDamageBehaviour> m_lstDamageBehaviour = new List<DSC_ActorDamageBehaviour>();
        protected List<IActorBehaviourData> m_lstBehaviourData = new List<IActorBehaviourData>();

        protected List<DSC_ActorDamageBehaviour> m_lstTempBehaviour;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hActorControler = GetComponent<DSC_ActorController>();
            m_hBaseActorStatus = GetComponent<BaseActorStatus>();
        }

        #endregion

        #region Events

        public void AddOnTakeDamageListener(UnityAction<DamageData> hAction)
        {
            MainAddOnTakeDamageListener(hAction);
        }

        public void AddOnTakeDamageListener(UnityAction<DamageData> hAction,EventOrder eOrder)
        {
            MainAddOnTakeDamageListener(hAction,eOrder);
        }

        void MainAddOnTakeDamageListener(UnityAction<DamageData> hAction,EventOrder eOrder = EventOrder.Normal)
        {
            m_hDamageableEvent.Add(DamageableEvent.TakeDamage, hAction,eOrder);
        }

        public void RemoveOnTakeDamageListener(UnityAction<DamageData> hAction)
        {
            MainRemoveOnTakeDamageListener(hAction);
        }

        public void RemoveOnTakeDamageListener(UnityAction<DamageData> hAction,EventOrder eOrder)
        {
            MainRemoveOnTakeDamageListener(hAction,eOrder);
        }

        void MainRemoveOnTakeDamageListener(UnityAction<DamageData> hAction,EventOrder eOrder = EventOrder.Normal)
        {
            m_hDamageableEvent.Remove(DamageableEvent.TakeDamage, hAction,eOrder);
        }

        public void AddOnDeadListener(UnityAction<DamageData> hAction)
        {
            MainAddOnDeadListener(hAction);
        }

        public void AddOnDeadListener(UnityAction<DamageData> hAction,EventOrder eOrder)
        {
            MainAddOnDeadListener(hAction,eOrder);
        }

        void MainAddOnDeadListener(UnityAction<DamageData> hAction, EventOrder eOrder = EventOrder.Normal)
        {
            m_hDamageableEvent.Add(DamageableEvent.Dead, hAction, eOrder);
        }

        public void RemoveOnDeadListener(UnityAction<DamageData> hAction)
        {
            MainRemoveOnDeadListener(hAction);
        }

        public void RemoveOnDeadListener(UnityAction<DamageData> hAction,EventOrder eOrder)
        {
            MainRemoveOnDeadListener(hAction,eOrder);
        }

        void MainRemoveOnDeadListener(UnityAction<DamageData> hAction, EventOrder eOrder = EventOrder.Normal)
        {
            m_hDamageableEvent.Remove(DamageableEvent.Dead, hAction, eOrder);
        }

        #endregion

        #region Interface

        public override bool TakeDamage(DamageData hData)
        {            
            return MainTakeDamage(hData);
        }

        #endregion

        #region Main

        protected virtual bool MainTakeDamage(DamageData hData)
        {
            if (!m_hActorControler.TryGetActorData(out ActorData hActorData)
                || !m_hBaseActorStatus.TryGetStatusData(out ActorStatus hStatusData))
                return false;

            // Ignore damage during IFrame.
            if (FlagUtility.HasFlagUnsafe(hActorData.m_eStateFlag, ActorStateFlag.IFrame)
                && !FlagUtility.HasFlagUnsafe(hData.m_ePenetrateType,DamagePenetrateFlag.IFrame))
            {
                return false;
            }

            int nDamage = hData.m_nDamage;

            if (nDamage <= 0 || hStatusData == null)
            {
                //StartDamageBehaviour(hData.m_arrBehaviour);
                return false;
            }

            

            m_hActorControler.InterruptAllBehaviour();

            var nCurrentHp = hStatusData.m_nCurrentHp;
            nCurrentHp -= nDamage;
            if (nCurrentHp < 0)
                nCurrentHp = 0;

            hStatusData.m_nCurrentHp = nCurrentHp;

            if (nCurrentHp == 0)
                Dead(hData);
            else
            {
                //RunAllTakeDamageEvent(hData);                
                m_hDamageableEvent.Run(DamageableEvent.TakeDamage, hData);
                //StartDamageBehaviour(hData.m_arrBehaviour);
            }

            return true;
        }

        protected override void Dead(DamageData hData)
        {
            //RunAllDeadEvent(hData);
            m_hDamageableEvent.Run(DamageableEvent.Dead,hData);

            Destroy(gameObject);
        }

        public void EndAllDamageBehaviour(DamageBehaviourType eType)
        {
            if (!m_lstDamageBehaviour.HasData())
                return;

            if (m_lstTempBehaviour == null)
                m_lstTempBehaviour = new List<DSC_ActorDamageBehaviour>();
            else
                m_lstTempBehaviour.Clear();

            for(int i = 0; i < m_lstDamageBehaviour.Count; i++)
            {
                var hBehaviour = m_lstDamageBehaviour[i];
                if(hBehaviour != null && hBehaviour.behaviourType == eType)
                {
                    //hBehaviour.OnEnd(actorData, m_lstBehaviourData);
                    m_lstTempBehaviour.Add(hBehaviour);
                }
            }

            for(int i = 0; i < m_lstTempBehaviour.Count; i++)
            {
                m_lstDamageBehaviour.Remove(m_lstTempBehaviour[i]);
            }

            m_lstTempBehaviour.Clear();
        }

        #endregion
    }
}