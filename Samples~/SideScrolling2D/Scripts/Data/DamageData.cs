using UnityEngine;

namespace DSC.Template.Actor.SideScrolling2D
{
    public struct DamageData
    {
        public int m_nDamage;
        public Vector2? m_vPosition;
        public Transform m_hAttacker;

        public DamagePenetrateFlag m_ePenetrateType;
        public DSC_ActorDamageBehaviour[] m_arrBehaviour;
    }
}