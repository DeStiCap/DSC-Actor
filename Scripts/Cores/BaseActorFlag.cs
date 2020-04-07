using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Core;

namespace DSC.Actor
{
    public struct FlagData
    {
        public List<Object> m_lstSetObject;
    }

    public abstract class BaseActorFlag<FlagType> : MonoBehaviour where FlagType : struct, System.Enum
    {
        protected abstract Dictionary<FlagType, Dictionary<int, FlagData>> dicCurrentSetFlag { get; set; }

        protected abstract void MainAddFlag(FlagType eType, int nFlag, Object hObject);
        protected abstract void MainRemoveFlag(FlagType eType, int nFlag, Object hObject);

        #region Main

        public void AddFlag<Flag>(FlagType eType, Flag eFlag, Object hObject) where Flag : unmanaged, System.Enum
        {
            if (dicCurrentSetFlag == null)
                dicCurrentSetFlag = new Dictionary<FlagType, Dictionary<int, FlagData>>();

            if (!FlagUtility.TryParseInt(eFlag, out int nFlag))
            {
                Debug.LogWarning("Can add flag type int only.");
                return;
            }

            if (dicCurrentSetFlag.ContainsKey(eType))
            {
                var dicData = dicCurrentSetFlag[eType];
                if (dicData.ContainsKey(nFlag))
                {

                    var lstData = dicData[nFlag].m_lstSetObject;
                    if (lstData.Contains(hObject))
                        lstData.Add(hObject);
                }
                else
                {
                    var lstNewData = new List<Object>();
                    lstNewData.Add(hObject);

                    dicData.Add(nFlag, new FlagData
                    {
                        m_lstSetObject = lstNewData
                    });
                }
            }
            else
            {
                var dicNewData = new Dictionary<int, FlagData>();
                var lstNewData = new List<Object>();
                lstNewData.Add(hObject);
                dicNewData.Add(nFlag, new FlagData
                {
                    m_lstSetObject = lstNewData
                });
                dicCurrentSetFlag.Add(eType, dicNewData);
            }

            MainAddFlag(eType, nFlag, hObject);
        }

        public bool RemoveFlag<Flag>(FlagType eType, Flag eFlag, Object hObject) where Flag : unmanaged, System.Enum
        {
            if (dicCurrentSetFlag == null || !dicCurrentSetFlag.ContainsKey(eType))
                return false;

            if (!FlagUtility.TryParseInt(eFlag, out int nFlag))
            {
                Debug.LogWarning("Can remove flag type int only.");
                return false;
            }

            if (!dicCurrentSetFlag[eType].ContainsKey(nFlag))
                return false;

            var lstData = dicCurrentSetFlag[eType][nFlag].m_lstSetObject;
            lstData.Remove(hObject);

            if(lstData.Count <= 0)
                MainRemoveFlag(eType, nFlag, hObject);

            return true;
        }

        #endregion
    }
}