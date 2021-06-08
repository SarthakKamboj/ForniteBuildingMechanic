using UnityEngine;

namespace Movement
{
    public class PlayerMovementAnimationResponse : MonoBehaviour, IMovementResponse
    {

        [SerializeField] Animator _animator;

        int _speedHorizontalHash, _speedVerticalHash;

        void Awake()
        {
            _speedHorizontalHash = Animator.StringToHash("SpeedHorizontal");
            _speedVerticalHash = Animator.StringToHash("SpeedVertical");
        }

        public void Move(Vector3 input)
        {
            Vector3 moveDir = Vector3.Scale(input, new Vector3(1, 0, 1));
            float vertical = input.z;
            float horizontal = input.x;

            if (Mathf.Abs(vertical) <= 0.1f)
            {
                _animator.SetFloat(_speedVerticalHash, 0f);
                _animator.SetFloat(_speedHorizontalHash, horizontal);
            }
            else
            {
                _animator.SetFloat(_speedVerticalHash, moveDir.magnitude);
                _animator.SetFloat(_speedHorizontalHash, 0f);
            }
        }
    }
}