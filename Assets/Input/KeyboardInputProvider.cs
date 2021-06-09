using UnityEngine;

namespace ControlInput
{
    public class KeyboardInputProvider : MonoBehaviour, IInputProvider
    {
        public float GetHorizontalInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public float GetVerticalInput()
        {
            return Input.GetAxisRaw("Vertical");
        }
    }
}