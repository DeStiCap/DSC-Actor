using UnityEngine;
using DSC.Core;
using DSC.Actor;

namespace DSC.Template.Actor.SideScrolling2D
{
    [CreateAssetMenu(fileName = "StatusDataBlueprint", menuName = "DSC/Actor/Data/Blueprint/2D SideScrolling/Status Data")]
    public class ActorStatus : BaseStatusData
    {
        [Min(0)]
        public int m_nMaxHp;

        [Min(0)]
        public int m_nCurrentHp;

        [Min(0)]
        public float m_fMoveSpeed;

        [Min(0)]
        public float m_fJumpForce;

        public override void Init(BaseStatusData hBlueprint)
        {
            var hData = (ActorStatus)hBlueprint;

            m_nMaxHp = hData.m_nMaxHp;            
            m_nCurrentHp = m_nMaxHp;
            m_fMoveSpeed = hData.m_fMoveSpeed;
            m_fJumpForce = hData.m_fJumpForce;
        }
    }
}