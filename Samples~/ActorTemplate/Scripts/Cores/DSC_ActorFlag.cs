using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;

namespace DSC.Template.Actor.Default
{
    [RequireComponent(typeof(DSC_ActorController))]
    public class DSC_ActorFlag : BaseActorFlag<FlagType>
    {
        #region Variable

        #region Variable - Property

        protected override Dictionary<FlagType, Dictionary<int, FlagData>> dicCurrentSetFlag { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        #endregion

        protected DSC_ActorController m_hActorController;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hActorController = GetComponent<DSC_ActorController>();
        }

        #endregion

        #region Base - Override

        protected override void MainAddFlag(FlagType eType, int nFlag, Object hObject)
        {

        }

        protected override void MainRemoveFlag(FlagType eType, int nFlag, Object hObject)
        {

        }

        #endregion
    }
}