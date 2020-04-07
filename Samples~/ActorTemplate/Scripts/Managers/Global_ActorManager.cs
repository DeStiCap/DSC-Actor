using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace DSC.Template.Actor.Default
{
    public sealed class Global_ActorManager : DSC_Actor
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        static Global_ActorManager instance
        {
            get
            {
                if (m_hInstance == null && m_bAppStart && !m_bAppQuit)
                    Debug.LogWarning("Don't have Global_ActorManager in scene.");

                return m_hInstance;
            }
        }

        #endregion

        static Global_ActorManager m_hInstance;
        static bool m_bAppStart;
        static bool m_bAppQuit;

        Dictionary<Transform, BaseActorData> m_dicActorData = new Dictionary<Transform, BaseActorData>();

        #endregion

        #region Base - Mono

        private void Awake()
        {
            if (m_hInstance == null)
            {
                baseInstance = this;
                m_hInstance = this;
            }
            else if (m_hInstance != this)
            {
                Destroy(this);
                return;
            }

            Application.quitting += OnAppQuit;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnAppStart()
        {
            m_bAppStart = true;
            m_bAppQuit = false;
        }

        void OnAppQuit()
        {
            Application.quitting -= OnAppQuit;

            m_bAppStart = false;
            m_bAppQuit = true;
        }

        #endregion

        #region Helper

        #endregion

        #region Base - Override

        protected override void MainRegisterActor(Transform hActor)
        {
            if (hActor == null)
                return;

            var hData = ScriptableObject.CreateInstance(typeof(BaseActorData));

            m_dicActorData.Add(hActor, (BaseActorData)hData);
        }

        protected override void MainRegisterActor(BaseActorData hData)
        {
            if (hData == null)
                return;

            var hActor = hData.m_hActor;

            if (hActor == null || m_dicActorData.ContainsKey(hActor))
                return;


            m_dicActorData.Add(hActor, hData);
        }

        protected override void MainUnregisterActor(Transform hActor)
        {
            m_dicActorData.Remove(hActor);
        }

        protected override void MainUnregisterActor<ActorData>(ActorData hData)
        {
            if (hData == null)
                return;

            m_dicActorData.Remove(hData.m_hActor);
        }

        protected override ActorData MainGetActorData<ActorData>(Transform hActor)
        {
            if (!m_dicActorData.ContainsKey(hActor))
                return default;

            var hData = m_dicActorData[hActor];
            if (!(hData is ActorData))
                return default;

            return (ActorData)hData;
        }

        protected override bool MainTryGetActorData<ActorData>(Transform hActor, out ActorData hOutData)
        {
            hOutData = MainGetActorData<ActorData>(hActor);

            return hOutData != null;
        }

        #endregion
    }
}