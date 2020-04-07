using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorBehaviourConditionGroup : BaseActorBehaviourCondition
    {
        protected abstract BaseActorBehaviourCondition[] conditionArray { get; }
        protected abstract bool allTrue { get; }

        public override bool PassCondition(BaseActorController hBaseController)
        {
            if (conditionArray == null || conditionArray.Length <= 0)
                return true;

            bool bResult = allTrue;

            for (int i = 0; i < conditionArray.Length; i++)
            {
                var hCondtion = conditionArray[i];
                if (hCondtion != null)
                {
                    bool bPass = hCondtion.PassCondition(hBaseController);
                    if (allTrue && !bPass)
                        return false;
                    else if (!allTrue && bPass)
                        return true;
                }
            }

            return bResult;
        }
    }
}