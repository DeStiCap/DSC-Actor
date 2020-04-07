using DSC.Core;
using DSC.Event;

namespace DSC.Actor
{
    public interface IActorData_Input : IActorData
    {
        BaseActorInput baseInput { get; }

        EventCallback<(InputButtonType, GetInputType), BaseActorController> inputButtonCallback { get; }

        void InitInput(BaseActorInput hBaseInput);
    }
}