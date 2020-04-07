using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorStatus : MonoBehaviour
    {
        #region Variable

        public abstract BaseStatusData baseStatusData { get; protected set; }
        public abstract BaseStatusData statusDataBlueprint { get; }

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            InitStatusData();
        }

        #endregion

        #region Base

        protected virtual void InitStatusData()
        {
            if (statusDataBlueprint)
            {
                baseStatusData = (BaseStatusData)ScriptableObject.CreateInstance(statusDataBlueprint.GetType());
                baseStatusData.Init(statusDataBlueprint);
            }
        }

        #endregion

        #region Data - Helper

        public T GetStatusData<T>() where T : BaseStatusData
        {
            if (!baseStatusData is T)
                return null;

            return baseStatusData as T;
        }

        public bool TryGetStatusData<T>(out T hOutData) where T : BaseStatusData
        {
            hOutData = GetStatusData<T>();
            return hOutData != null;
        }

        #endregion
    }
}