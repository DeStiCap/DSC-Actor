using UnityEngine;
using DSC.Core;

namespace DSC.Actor
{
    public class DSC_ActorPhysic3D : BaseActorPhysic3D
    {
        #region Variable

        #region Variable - Property

        protected override Rigidbody rigidbody { get { return m_hRigid; } }

        protected override float timeScale { get { return m_fTimeScale; } }

        public override bool useGravity { get { return m_bOriginalUseGravity; } set { m_bOriginalUseGravity = value; } }

        #endregion

        protected BaseActor m_hActorController;
        protected Rigidbody m_hRigid;

        protected float m_fTimeScale = 1;

        protected float m_fOriginalLinearDrag;
        protected float m_fOriginalAngularDrag;
        protected bool m_bOriginalUseGravity;

        protected Vector3 m_vVelocityBeforeStop;
        protected Vector3 m_vAngularVelocityBeforeStop;

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

            m_hRigid = GetComponent<Rigidbody>();
            if (m_hRigid == null)
            {
                Debug.LogError("Don't have Rigidbody in this Actor", gameObject);
            }
            else
            {
                m_fOriginalLinearDrag = m_hRigid.drag;
                m_fOriginalAngularDrag = m_hRigid.angularDrag;
                m_bOriginalUseGravity = m_hRigid.useGravity;

                m_hRigid.useGravity = false;
            }
        }

        private void FixedUpdate()
        {
            if (!m_hRigid.isKinematic)
            {
                var vVelocity = m_hRigid.velocity;
                vVelocity += Physics.gravity * m_fTimeScale * m_hActorController.actorFixedDeltaTime;
                m_hRigid.velocity = vVelocity;
            }
        }

        #endregion

        #region Base - Override

        protected override void OnTimeScaleChange(float fNewTimeScale)
        {
            var vVelocity = m_hRigid.velocity;
            var vAngularVelocity = m_hRigid.angularVelocity;

            if (fNewTimeScale == 0)
            {
                m_hRigid.drag = m_fOriginalLinearDrag;
                m_hRigid.angularDrag = m_fOriginalAngularDrag;

                m_vVelocityBeforeStop = vVelocity / m_fTimeScale;
                m_vAngularVelocityBeforeStop = vAngularVelocity / m_fTimeScale;
            }
            else
            {
                m_hRigid.drag = m_fOriginalLinearDrag * fNewTimeScale;
                m_hRigid.angularDrag = m_fOriginalAngularDrag * fNewTimeScale;
            }

            if (m_fTimeScale != 0)
            {
                vVelocity /= m_fTimeScale;
                vAngularVelocity /= m_fTimeScale;
            }
            else
            {
                vVelocity = m_vVelocityBeforeStop;
                vAngularVelocity = m_vAngularVelocityBeforeStop;
            }

            vVelocity *= fNewTimeScale;
            vAngularVelocity *= fNewTimeScale;

            m_hRigid.velocity = vVelocity;
            m_hRigid.angularVelocity = vAngularVelocity;

            m_fTimeScale = fNewTimeScale;
        }

        #endregion
    }
}