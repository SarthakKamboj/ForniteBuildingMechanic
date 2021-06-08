using UnityEngine;

namespace ControlInput
{
    public class KeyboardInputProvider : MonoBehaviour, IInput
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