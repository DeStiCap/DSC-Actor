using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public static class Extension_BaseActorBehaviourCondition
    {
        public static bool PassCondition(this BaseActorBehaviourCondition[] arrCondition, BaseActorController hBaseController)
        {
            if (arrCondition == null || arrCondition.Length <= 0)
                return true;

            for (int i = 0; i < arrCondition.Length; i++)
            {
                var hCondition = arrCondition[i];
                if (hCondition != null && !hCondition.PassCondition(hBaseController))
                    return false;
            }

            return true;
        }

        public static bool PassCondition(this List<BaseActorBehaviourCondition> lstCondition, BaseActorController hBaseController)
        {
            if (lstCondition == null || lstCondition.Count <= 0)
                return true;

            for (int i = 0; i < lstCondition.Count; i++)
            {
                var hCondition = lstCondition[i];
                if (hCondition != null && !hCondition.PassCondition(hBaseController))
                    return false;
            }

            return true;
        }
    }
}