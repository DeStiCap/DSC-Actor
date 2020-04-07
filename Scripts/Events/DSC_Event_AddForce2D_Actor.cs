using UnityEngine;
using DSC.Actor;

namespace DSC.Event.Helper
{
    public class DSC_Event_AddForce2D_Actor : MonoBehaviour
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] protected BaseActorPhysic2D m_hTarget;
        [SerializeField] protected float m_fForce;
        [SerializeField] protected Vector2 m_vForceDirection;
        [SerializeField] protected ForceMode2D m_eMode;

        [Header("Debug")]
        [SerializeField] protected bool m_bShowDebugLog;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        public BaseActorPhysic2D target
        {
            get
            {
                if (m_hTarget == null)
                    m_hTarget = GetComponent<BaseActorPhysic2D>();

                return m_hTarget;
            }
            set { m_hTarget = value; }
        }

        public float force
        {
            get { return m_fForce; }
            set { m_fForce = value; }
        }

        public float directionX
        {
            get { return m_vForceDirection.x; }
            set { m_vForceDirection.x = value; }
        }

        public float directionY
        {
            get { return m_vForceDirection.y; }
            set { m_vForceDirection.y = value; }
        }

        public ForceMode2D mode
        {
            get { return m_eMode; }
            set { m_eMode = value; }
        }

        #endregion

        #endregion

        #region Events

        public void AddForce()
        {
            MainAddForce(m_fForce);
        }

        public void AddForce(float fAddForce)
        {
            MainAddForce(fAddForce);
        }

        public void AddForce(GameObject hTarget)
        {
            SetTarget(hTarget);
            AddForce();
        }

        public void SetTarget(GameObject hTarget)
        {
            if (hTarget == null)
                m_hTarget = null;

            m_hTarget = hTarget.GetComponent<BaseActorPhysic2D>();
        }

        public void ResetDirectionToZero()
        {
            m_vForceDirection = Vector2.zero;
        }

        #endregion

        #region Main

        protected virtual void MainAddForce(float fAddForce)
        {
            var hPhysic = target;

            if (hPhysic == null)
            {
                if (m_bShowDebugLog)
                    Debug.LogWarning("Don't have target rigidbody to add force.");
                return;
            }

            hPhysic.AddForce(fAddForce * m_vForceDirection, m_eMode);
        }

        #endregion
    }
}