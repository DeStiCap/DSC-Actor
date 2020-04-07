using UnityEngine;

namespace DSC.Actor
{
    public abstract class BaseActorPhysic2D : BaseActorPhysic
    {
        protected abstract new Rigidbody2D rigidbody { get; }

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

        public virtual Vector2 velocity
        {
            get
            {
                if (rigidbody == null)
                    return default;

                return rigidbody.velocity;
            }

            set
            {
                if (rigidbody == null)
                    return;

                rigidbody.velocity = value;
            }
        }

        public virtual float angularVelocity
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

        public abstract float gravityScale { get; set; }

        public virtual void AddForce(Vector2 vForce,ForceMode2D eMode = ForceMode2D.Impulse)
        {
            if (rigidbody == null || timeScale <= 0)
                return;

            vForce *= timeScale;

            rigidbody.AddForce(vForce, eMode);
        }

        public virtual void MovePosition(Vector2 vPosition)
        {
            if (rigidbody == null || timeScale <= 0)
                return;

            rigidbody.MovePosition(vPosition);
        }

        public virtual void MoveRotation(float fAngle)
        {
            if (rigidbody == null || timeScale <= 0)
                return;

            rigidbody.MoveRotation(fAngle);
        }

        public virtual void MoveRotation(Quaternion qRot)
        {
            if (rigidbody == null || timeScale <= 0)
                return;

            rigidbody.MoveRotation(qRot);
        }
    }
}