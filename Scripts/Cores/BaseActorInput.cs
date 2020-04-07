using System.Collections.Generic;
using UnityEngine.Events;
using DSC.Core;

namespace DSC.Actor
{
    public abstract class BaseActorInput : BaseUpdateMono 
    {
        #region Variable - Property

        protected abstract BaseActorController baseActor { get; }
        protected abstract Dictionary<InputButtonType,GetInputType> previousGetType { get; set; }

        public abstract InputData inputData { get; set; }

        public abstract event UnityAction<InputButtonType, GetInputType> onRunEventInput;

        #endregion

        protected abstract void RunEventInput(InputButtonType eButtonType, GetInputType eGetType);

        protected abstract void SetHoldingInputData(InputButtonType eButtonType, GetInputType eGetType);

        #region Base - Main

        /// <summary>
        /// On press event process.
        /// </summary>
        /// <param name="eButtonType">Input event type.</param>
        /// <param name="bRawValue">Raw value of input.</param>
        /// <returns>Get input type of this input event.</returns>
        protected virtual GetInputType OnPressInput(InputButtonType eButtonType,bool bRawValue)
        {
            if (previousGetType == null)
                previousGetType = new Dictionary<InputButtonType, GetInputType>();

            if (!previousGetType.ContainsKey(eButtonType))
                previousGetType.Add(eButtonType, GetInputType.None);

            var eGetType = previousGetType[eButtonType];
            eGetType = InputUtility.ConvertRawValueToGetType(eGetType, bRawValue);

            RunEventInput(eButtonType, eGetType);

            SetHoldingInputData(eButtonType, eGetType);

            previousGetType[eButtonType] = eGetType;

            return eGetType;
        }

       
        #endregion
    }
}