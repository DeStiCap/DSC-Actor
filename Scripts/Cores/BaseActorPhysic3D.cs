using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorPhysic3D : BaseActorPhysic
    {
        protected abstract new Rigidbody rigidbody { get; }
        public override bool isKinematic
        {
            get
            {
                if (rigidbody == null)
                    return default;

                return rigidbody.isKinematic;
            }

            set
            {
                if (rigidbody == null)
                    return;

                rigidbody.isKinematic = value;
            }
        }

        public override float mass
        {
            get
            {
                if (rigidbody == null)
                    return 1;

                return rigidbody.mass;
            }

            set
            {
                if (rigidbody == null)
                    return;

                rigidbody.mass = value;
            }
        }

        public override float drag
        {
            get
            {
                if (rigidbody == null)
                    return default;

                return rigidbody.drag;
            }

            set
            {
                if (rigidbody == null)
                    return;

                rigidbody.drag = value;
            }
        }

        public override float angularDrag
        {
            get
            {
                if (rigidbody == null)
                    return default;

                return rigidbody.angularDrag;
            }

            set
            {
                if (rigidbody == null)
                    return;

                rigidbody.angularDrag = value;
            }
        }

        public virtual Vector3 angularVelocity
        {
            get
            {
                if (rigidbody == null)
                    return default;

                return rigidbody.angularVelocity;
            }

            set
            {
                if (rigidbody == null)
                    return;

                rigidbody.angularVelocity = value;
            }
        }

        public virtual bool useGravity
        {
            get
            {
                if (rigidbody == null)
                    return default;

                return rigidbody.useGravity;
            }

            set
            {
                if (rigidbody == null)
                    return;

                rigidbody.useGravity = value;
            }
        }

        public virtual void AddForce(Vector3 vForce,ForceMode eMode = ForceMode.Impulse)
        {
            if (rigidbody == null || timeScale <= 0)
                return;

            vForce *= timeScale;

            rigidbody.AddForce(vForce, eMode);
        }

        public virtual void AddExplosionForce(float fForce,Vector3 vPosition,float fRadius,float fUpwardsModifier = default,ForceMode eMode = ForceMode.Impulse)
        {
            if (rigidbody == null || timeScale <= 0)
                return;

            rigidbody.AddExplosionForce(fForce, vPosition, fRadius, fUpwardsModifier, eMode);
        }

        public virtual void MovePosition(Vector3 vPosition)
        {
            if (rigidbody == null || timeScale <= 0)
                return;

            rigidbody.MovePosition(vPosition);
        }
        
        public virtual void MoveRotation(Quaternion qRot)
        {
            if (rigidbody == null || timeScale <= 0)
                return;

            rigidbody.MoveRotation(qRot);
        }

        public virtual void MoveRotation(Vector3 vRot)
        {
            if (rigidbody == null || timeScale <= 0)
                return;

            rigidbody.MoveRotation(Quaternion.Euler(vRot));
        }
    }
}