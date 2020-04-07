using UnityEngine;
using DSC.Core;

namespace DSC.Template.Actor.SideScrolling2D
{
    [System.Serializable]
    public class ActorStatus
    {
        [Min(0)]
        public int m_nMaxHp;

        [Min(0)]
        public int m_nCurrentHp;

        [Min(0)]
        public float m_fMoveSpeed;

        [Min(0)]
        public float m_fJumpForce;
    }
}