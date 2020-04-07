using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseStatusData : ScriptableObject
    {
        public abstract void Init(BaseStatusData hBlueprint);
    }
}