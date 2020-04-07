using UnityEngine;
using DSC.Core;

namespace DSC.Actor
{
    public class DSC_ActorPhysic2D : BaseActorPhysic2D
    {
        #region Variable

        #region Variable - Property

        protected override Rigidbody2D rigidbody { get { return m_hRigid; } }
        protected override float timeScale { get { return m_fTimeScale; } }
        public override float gravityScale { get { return m_fOriginalGravityScale; } set { m_fOriginalGravityScale = value; } } 

        #endregion

        protected BaseActor m_hActorController;
        protected Rigidbody2D m_hRigid;

        protected float m_fTimeScale = 1;

        protected float m_fOriginalLinearDrag;
        protected float m_fOriginalAngularDrag;
        protected float m_fOriginalGravityScale;

        protected Vector2 m_vVelocityBeforeStop;
        protected float m_fAngularVelocityBeforeStop;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hActorController = GetComponent<BaseActor>();

            if (m_hActorController == null)
            {
                Debug.LogError("Don't have Actor Controller in this Actor", gameObject);
            }
            else
            {
                m_hActorController.onTimeScaleChange += OnTimeScaleChange;
            }

            m_hRigid = GetComponent<Rigidbody2D>();
            if (m_hRigid == null)
            {
                Debug.LogError("Don't have Rigidbody2D in this Actor", gameObject);
            }
            else
            {
                m_fOriginalLinearDrag = m_hRigid.drag;
                m_fOriginalAngularDrag = m_hRigid.angularDrag;
                m_fOriginalGravityScale = m_hRigid.gravityScale;

                m_hRigid.gravityScale = 0;
            }
        }

        private void FixedUpdate()
        {
            if (!m_hRigid.isKinematic)
            {
                var vVelocity = m_hRigid.velocity;
                vVelocity += Physics2D.gravity * m_fOriginalGravityScale * m_fTimeScale * m_hActorController.actorFixedDeltaTime;
                m_hRigid.velocity = vVelocity;
            }
        }

        #endregion

        #region Base - Override

        protected override void OnTimeScaleChange(float fNewTimeScale)
        {
            var vVelocity = m_hRigid.velocity;
            var fAngularVelocity = m_hRigid.angularVelocity;

            if (fNewTimeScale == 0)
            {
                m_hRigid.drag = m_fOriginalLinearDrag;
                m_hRigid.angularDrag = m_fOriginalAngularDrag;

                m_vVelocityBeforeStop = vVelocity / m_fTimeScale;
                m_fAngularVelocityBeforeStop = fAngularVelocity / m_fTimeScale;
            }
            else
            {
                m_hRigid.drag = m_fOriginalLinearDrag * fNewTimeScale;
                m_hRigid.angularDrag = m_fOriginalAngularDrag * fNewTimeScale;
            }

            if (m_fTimeScale != 0)
            {
                vVelocity /= m_fTimeScale;
                fAngularVelocity /= m_fTimeScale;
            }
            else
            {
                vVelocity = m_vVelocityBeforeStop;
                fAngularVelocity = m_fAngularVelocityBeforeStop;
            }

            vVelocity *= fNewTimeScale;
            fAngularVelocity *= fNewTimeScale;

            m_hRigid.velocity = vVelocity;
            m_hRigid.angularVelocity = fAngularVelocity;

            m_fTimeScale = fNewTimeScale;
        }

        #endregion
    }
}