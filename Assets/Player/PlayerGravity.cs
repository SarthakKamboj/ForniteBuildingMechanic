using UnityEngine;

namespace Player
{
    public class PlayerGravity : MonoBehaviour
    {

        [SerializeField] CharacterController cc;
        [SerializeField] Transform groundCheck;
        [SerializeField] LayerMask groundLayerMask;

        float gravity = 9.8f;

        void Update()
        {

            HandleGravity();
        }

        private void HandleGravity()
        {
            if (!IsGrounded())
            {
                cc.Move(new Vector3(0f, -gravity * Time.deltaTime, 0f));
            }
        }

        bool IsGrounded()
        {
            return Physics.OverlapSphere(groundCheck.position, 0.1f, groundLayerMask).Length >= 1;
        }
    }
}