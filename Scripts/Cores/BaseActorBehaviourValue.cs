using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorBehaviourValue : ScriptableObject
    {

    }

    public abstract class BaseActorBehaviourValue<T> : BaseActorBehaviourValue
    {
        public abstract T value { get; }
        public abstract void CalculateValue(ref T value);
    }

    #region Temp

    public abstract class ActorBehaviourValueInt : BaseActorBehaviourValue<int>
    {
        public abstract BehaviourValueType valueType { get; }

        public override void CalculateValue(ref int value)
        {
            switch (valueType)
            {
                case BehaviourValueType.Override:
                    value = this.value;
                    break;

                case BehaviourValueType.Add:
                    value += this.value;
                    break;

                case BehaviourValueType.Multiply:
                    value *= this.value;
                    break;
            }
        }
    }

    public abstract class ActorBehaviourValueFloat : BaseActorBehaviourValue<float>
    {
        public abstract BehaviourValueType valueType { get; }

        public override void CalculateValue(ref float value)
        {
            switch (valueType)
            {
                case BehaviourValueType.Override:
                    value = this.value;
                    break;

                case BehaviourValueType.Add:
                    value += this.value;
                    break;

                case BehaviourValueType.Multiply:
                    value *= this.value;
                    break;
            }
        }
    }

    public abstract class ActorBehaviourValueBool : BaseActorBehaviourValue<bool>
    {
        public override void CalculateValue(ref bool value)
        {
            value = this.value;
        }
    }

    public abstract class ActorBehaviourValueString : BaseActorBehaviourValue<string>
    {
        public override void CalculateValue(ref string value)
        {
            value = this.value;
        }
    }

    #endregion
}