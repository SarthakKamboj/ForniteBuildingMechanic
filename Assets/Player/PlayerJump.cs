using ProjectPhysics;
using UnityEngine;

namespace Player
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] float jumpHeight = 10f;
        [SerializeField] Animator _animator;
        [SerializeField] PlayerGravity _playerGravity;

        const float GRAVITY = 9.8f;
        bool jumped = true;
        int JumpHash;

        void Awake()
        {
            JumpHash = Animator.StringToHash("Jumping");
            _playerGravity.AddOnLandedOnGroundListener(OnLand);
        }

        void OnDestroy()
        {
            _playerGravity.RemoveOnLandedOnGroundListener(OnLand);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _playerGravity.IsGrounded())
            {
                float yVel = 2 * GRAVITY * jumpHeight;
                _playerGravity.SetYVel(yVel);
                jumped = true;
                _animator.SetBool(JumpHash, true);
            }
        }

        void OnLand()
        {
            if (jumped)
            {
                jumped = false;
                _animator.SetBool(JumpHash, false);
            }
        }

    }
}