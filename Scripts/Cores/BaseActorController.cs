using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Actor
{
    public abstract class BaseActorController : BaseActor
    {
        #region Variable - Property

        public abstract BaseActorData actorDataBlueprint { get; }
        public BaseActorInput baseActorInput { get; private set; }

        /// <summary>
        /// List of Behaviour in this controller.
        /// </summary>
        protected abstract List<BaseActorBehaviour> listBehaviour { get; set; }

        /// <summary>
        /// List of Behaviour that running currently.
        /// </summary>
        protected abstract List<BaseActorBehaviour> listBehaviourRunning { get; set; }

        /// <summary>
        /// List of Behaviour data in this controller.
        /// </summary>
        protected abstract List<IActorBehaviourData> listBehaviourData { get; set; }

        protected abstract List<BaseUpdateMono> listPreUpdateProcessing { get; }

        #endregion

        #region Base

        public virtual void RegisterBaseInput(BaseActorInput hBaseInput)
        {
            baseActorInput = hBaseInput;

            if (hBaseInput)
            {
                hBaseInput.onRunEventInput += OnRunEventInput;
            }
        }

        protected abstract void OnRunEventInput(InputButtonType eButtonType, GetInputType eGetType);

        #endregion

        #region Base - Override

        protected override void InitActorData()
        {
            base.InitActorData();
            if (actorDataBlueprint)
            {
                baseActorData = (BaseActorData) ScriptableObject.CreateInstance(actorDataBlueprint.GetType());
                baseActorData.Init(transform);
            }
        }

        #endregion

        #region Data - Helper

        /// <summary>
        /// Get Actor Data.
        /// </summary>
        /// <typeparam name="T">Actor Data Type</typeparam>
        /// <returns>Actor Data</returns>
        public T GetActorData<T>() where T : BaseActorData
        {
            if (baseActorData is T)
            {
                return baseActorData as T;
            }

            return null;
        }

        /// <summary>
        /// Try get Actor Data
        /// </summary>
        /// <typeparam name="T">Actor Data Type</typeparam>
        /// <param name="hOutData">Actor Data</param>
        /// <returns>True if get success</returns>
        public bool TryGetActorData<T>(out T hOutData) where T : BaseActorData
        {
            hOutData = GetActorData<T>();
            return hOutData != null;
        }

        /// <summary>
        /// Try to get this behaviour data type in this controller.
        /// </summary>
        /// <typeparam name="T">Behaviour Data Type</typeparam>
        /// <param name="hOutData">Behaviour Data</param>
        /// <returns>True if get success</returns>
        public bool TryGetBehaviourData<T>(out T hOutData) where T : IActorBehaviourData
        {
            if (listBehaviourData == null)
            {
                hOutData = default;
                return false;
            }

            return listBehaviourData.TryGetData(out hOutData);
        }

        /// <summary>
        /// Try get behaviour data.
        /// </summary>
        /// <typeparam name="T">Behaviour Data Type</typeparam>
        /// <param name="hOutData">Behaviour Data</param>
        /// <param name="nOutIndex">Index</param>
        /// <returns>True if get success</returns>
        public bool TryGetBehaviourData<T>(out T hOutData,out int nOutIndex) where T : IActorBehaviourData
        {
            if (listBehaviourData == null)
            {
                hOutData = default;
                nOutIndex = -1;
                return false;
            }

            return listBehaviourData.TryGetData(out hOutData,out nOutIndex);
        }

        /// <summary>
        /// Add new behaviour data to data list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        /// <param name="hData">New data that want to add.</param>
        public virtual void AddBehaviourData<Data>(Data hData) where Data : IActorBehaviourData
        {
            if (listBehaviourData == null)
                listBehaviourData = new List<IActorBehaviourData>();

            listBehaviourData.Add(hData);
        }

        /// <summary>
        /// Remove this behaviour data type from data list.
        /// </summary>
        /// <typeparam name="Data">Data type</typeparam>
        public virtual void RemoveBehaviourData<Data>() where Data : IActorBehaviourData
        {
            if (listBehaviourData == null || listBehaviourData.Count <= 0)
                return;

            for (int i = 0; i < listBehaviourData.Count; i++)
            {
                var behaviourData = listBehaviourData[i];
                if (behaviourData is Data)
                {
                    listBehaviourData.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove behaviour data in this index from data list.
        /// </summary>
        public virtual void RemoveBehaviourData(int nDataIndex)
        {
            if (listBehaviourData == null || listBehaviourData.Count <= nDataIndex)
                return;

            listBehaviourData.RemoveAt(nDataIndex);
        }

        #endregion

        #region Behaviour

        /// <summary>
        /// Get behaviour from data.
        /// </summary>
        /// <typeparam name="T">Behaviour</typeparam>
        /// <returns>Return get behaviour</returns>
        public virtual T GetBehaviour<T>() where T : BaseActorBehaviour
        {
            if (!HasBehaviourIsList())
                return null;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                if (listBehaviour[i] is T)
                    return listBehaviour[i] as T;
            }

            return null;
        }

        /// <summary>
        /// Try Get behaviour from data.
        /// </summary>
        /// <typeparam name="T">Behaviour</typeparam>
        /// <param name="hOutBehaviour"></param>
        /// <returns>Return true if get success.</returns>
        public virtual bool TryGetBehaviour<T>(out T hOutBehaviour) where T : BaseActorBehaviour
        {
            hOutBehaviour = GetBehaviour<T>();
            return hOutBehaviour != null;
        }

        /// <summary>
        /// Add behaviour to data.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to add to data.</param>
        public virtual void AddBehaviour(BaseActorBehaviour hBehaviour)
        {
            if (hBehaviour == null)
                return;

            if (listBehaviour == null)
                listBehaviour = new List<BaseActorBehaviour>();

            listBehaviour.Add(hBehaviour);
            CreateBehaviour(hBehaviour);
        }

        /// <summary>
        /// Add list behaviour to data.
        /// </summary>
        /// <param name="lstBehaviour">List of behaviour that want to add to data.</param>
        public virtual void AddBehaviour(List<BaseActorBehaviour> lstBehaviour)
        {
            if (lstBehaviour == null || lstBehaviour.Count <= 0)
                return;

            for (int i = 0; i < lstBehaviour.Count; i++)
            {
                AddBehaviour(lstBehaviour[i]);
            }
        }

        /// <summary>
        /// Remove behaviour from data. (Remove first one that found.)
        /// </summary>
        /// <typeparam name="T">Behaviour that want to remove.</typeparam>
        public virtual void RemoveBehaviour<T>() where T : BaseActorBehaviour
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                var hBehaviour = listBehaviour[i];
                if (hBehaviour != null && hBehaviour is T)
                {
                    StopBehaviour(hBehaviour);
                    DestroyBehaviour(hBehaviour);
                    listBehaviour.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove behaviour from data. (Remove first one that found.)
        /// </summary>
        /// <param name="nBehaviourTypeID">ID of behaviour type that want to remove.</param>
        public virtual void RemoveBehaviour(int nBehaviourTypeID)
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                var hBehaviour = listBehaviour[i];
                if (hBehaviour != null && hBehaviour.behaviourTypeID == nBehaviourTypeID)
                {
                    StopBehaviour(hBehaviour);
                    DestroyBehaviour(hBehaviour);
                    listBehaviour.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove all behaviour from data.
        /// </summary>
        public virtual void RemoveAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                var hBehaviour = listBehaviour[i];
                StopBehaviour(hBehaviour);
                DestroyBehaviour(hBehaviour);
            }

            listBehaviour.Clear();
        }

        /// <summary>
        /// Remove this behaviour type in data and add new one to data instead.
        /// </summary>
        /// <typeparam name="T">Behaviour type that want to change.(Remove first one that found.)</typeparam>
        /// <param name="hNewBehaviour">>New behaviour for replace old one.</param>
        public virtual void ChangeBehaviour<T>(BaseActorBehaviour hNewBehaviour) where T : BaseActorBehaviour
        {
            RemoveBehaviour<T>();
            AddBehaviour(hNewBehaviour);
        }

        /// <summary>
        /// Remove this behaviour type ID in data and add new one to data instead.
        /// </summary>
        /// <param name="hNewBehaviour">New behaviour for replace old one.</param>
        public virtual void ChangeBehaviour(BaseActorBehaviour hNewBehaviour)
        {
            RemoveBehaviour(hNewBehaviour.behaviourTypeID);
            AddBehaviour(hNewBehaviour);
        }

        /// <summary>
        /// Interrupt this behaviour.
        /// </summary>
        /// <typeparam name="T">Behaviour type</typeparam>
        /// <param name="hBehaviour">Behaviour</param>
        public virtual void InterruptBehaviour<T>(T hBehaviour) where T : BaseActorBehaviour
        {
            hBehaviour?.OnInterruptBehaviour(this);
        }

        /// <summary>
        /// Interrupt this behaviour.
        /// </summary>
        /// <typeparam name="T">Behaviour type</typeparam>
        public virtual void InterruptBehaviour<T>() where T : BaseActorBehaviour
        {
            if (TryGetBehaviour<T>(out var hOutBehaviour))
            {
                hOutBehaviour.OnInterruptBehaviour(this);
            }
        }

        /// <summary>
        /// Interrupt all behaviour in this controller.
        /// </summary>
        public virtual void InterruptAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                InterruptBehaviour(listBehaviour[i]);
            }
        }

        #endregion

        #region Behaviour - Helper

        protected bool HasBehaviourIsList()
        {
            return (listBehaviour != null && listBehaviour.Count > 0);
        }

        /// <summary>
        /// Call create function in this behaviour.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to call create.</param>
        protected virtual void CreateBehaviour(BaseActorBehaviour hBehaviour)
        {
            hBehaviour?.OnCreateBehaviour(this);
        }

        /// <summary>
        /// Call create function from all behaviour.
        /// </summary>
        protected virtual void CreateAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                CreateBehaviour(listBehaviour[i]);
            }
        }

        /// <summary>
        /// Call destroy function in this behaviour.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to call destroy.</param>
        protected virtual void DestroyBehaviour(BaseActorBehaviour hBehaviour)
        {
            hBehaviour?.OnDestroyBehaviour(this);
        }

        /// <summary>
        /// Call destroy function from all behaviour.
        /// </summary>
        protected virtual void DestroyAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                DestroyBehaviour(listBehaviour[i]);
            }
        }

        /// <summary>
        /// Call start function in this behaviour.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to call start.</param>
        protected virtual void StartBehaviour(BaseActorBehaviour hBehaviour)
        {
            if (listBehaviourRunning == null)
                listBehaviourRunning = new List<BaseActorBehaviour>();

            if (hBehaviour == null || listBehaviourRunning.Contains(hBehaviour))
                return;

            listBehaviourRunning.Add(hBehaviour);
            hBehaviour.OnStartBehaviour(this);
        }

        /// <summary>
        /// Call start function from all behaviour.
        /// </summary>
        protected virtual void StartAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                StartBehaviour(listBehaviour[i]);
            }
        }

        /// <summary>
        /// Call stop function in this behaviour.
        /// </summary>
        /// <param name="hBehaviour">Behaviour want to call stop.</param>
        protected virtual void StopBehaviour(BaseActorBehaviour hBehaviour)
        {
            if (listBehaviourRunning == null)
                listBehaviourRunning = new List<BaseActorBehaviour>();

            if (hBehaviour == null || !listBehaviourRunning.Contains(hBehaviour))
                return;

            listBehaviourRunning.Remove(hBehaviour);
            hBehaviour.OnStopBehaviour(this);
        }

        /// <summary>
        /// Call stop function from all behaviour.
        /// </summary>
        protected virtual void StopAllBehaviour()
        {
            if (!HasBehaviourIsList())
                return;

            for (int i = 0; i < listBehaviour.Count; i++)
            {
                StopBehaviour(listBehaviour[i]);
            }
        }

        /// <summary>
        /// Call update function in this behaviour.
        /// </summary>
        /// <param name="behaviour">Behaviour want to call update.</param>
        protected virtual void UpdateBehaviour(BaseActorBehaviour behaviour)
        {
            behaviour?.OnUpdateBehaviour(this);
        }

        /// <summary>
        /// Call fix update function in this behaviour.
        /// </summary>
        /// <param name="behaviour">Behaviour want to call fixed update.</param>
        protected virtual void FixedUpdateBehaviour(BaseActorBehaviour behaviour)
        {
            behaviour?.OnFixedUpdateBehaviour(this);
        }

        /// <summary>
        /// Call late update function in this behaviour.
        /// </summary>
        /// <param name="behaviour">Behaviour want to call late update.</param>
        protected virtual void LateUpdateBehaviour(BaseActorBehaviour behaviour)
        {
            behaviour?.OnLateUpdateBehaviour(this);
        }

        protected virtual void ExecuteBehaviour()
        {
            if (!listBehaviourRunning.HasData() || isTimeStop)
                return;

            for (int i = 0; i < listBehaviourRunning.Count; i++)
            {
                UpdateBehaviour(listBehaviourRunning[i]);
            }
        }

        protected virtual void FixedExecuteBehaviour()
        {
            if (!listBehaviourRunning.HasData() || isTimeStop)
                return;

            for (int i = 0; i < listBehaviourRunning.Count; i++)
            {
                FixedUpdateBehaviour(listBehaviourRunning[i]);
            }
        }

        protected virtual void LateExecuteBehaviour()
        {
            if (!listBehaviourRunning.HasData() || isTimeStop)
                return;

            for (int i = 0; i < listBehaviourRunning.Count; i++)
            {
                LateUpdateBehaviour(listBehaviourRunning[i]);
            }
        }

        #endregion
    }
}