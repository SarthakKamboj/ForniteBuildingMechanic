using UnityEngine;

namespace ControlInput
{
    public interface IInputProvider
    {
        float GetHorizontalInput();
        float GetVerticalInput();
    }
}