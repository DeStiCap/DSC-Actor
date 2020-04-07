namespace DSC.Template.Actor.SideScrolling2D
{
    public enum ActorStateFlag
    {
        Walking         = 1 << 0,
        Jumping         = 1 << 1,
        Falling         = 1 << 2,
        FacingRight     = 1 << 3,
        IFrame          = 1 << 4,

        IsWalling       = 1 << 30,
        IsGrounding     = 1 << 31
    }
}