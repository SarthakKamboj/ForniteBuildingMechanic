
using UnityEngine;

namespace Movement
{
    public class PlayerMovementResponse : MonoBehaviour, IMovementResponse
    {
        [SerializeField] float _smoothTime = 0.1f;
        [SerializeField] float _speed = 10f;
        [SerializeField] Transform _mainCamera;
        [SerializeField] Animator _animator;

        float _targetAngle;
        CharacterController _cc;
        float _curVel;

        int _speedHorizontalHash, _speedVerticalHash;

        void Awake()
        {
            _speedHorizontalHash = Animator.StringToHash("SpeedHorizontal");
            _speedVerticalHash = Animator.StringToHash("SpeedVertical");

            _targetAngle = transform.eulerAngles.y;
            _cc = GetComponent<CharacterController>();
        }

        public void Move(Vector3 input)
        {

            Vector3 inputVec = Vector3.Scale(input, new Vector3(1, 0, 1));

            if (inputVec.magnitude >= 0.1f)
            {
                Transform t = transform;
                Vector3 rot = t.eulerAngles;

                _targetAngle = Mathf.Atan2(inputVec.x, inputVec.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
                MoveEntity(inputVec);
                SetRotation(t, rot);
            }

            SetAnimation(inputVec);
        }

        private void SetRotation(Transform t, Vector3 rot)
        {
            float angle = Mathf.SmoothDampAngle(rot.y, _targetAngle, ref _curVel, _smoothTime);
            t.rotation = Quaternion.Euler(new Vector3(rot.x, angle, rot.z));
        }

        private void MoveEntity(Vector3 input)
        {
            Vector3 moveVec = Quaternion.Euler(new Vector3(0f, _targetAngle, 0f)) * Vector3.forward * _speed * Time.deltaTime;

            float vertical = input.z;
            float horizontal = input.x;

            if (Mathf.Abs(vertical) <= 0.1f)
            {
                moveVec *= 0.25f;
            }
            _cc.Move(moveVec);
        }

        private void SetAnimation(Vector3 moveDir)
        {
            float vertical = moveDir.z;
            float horizontal = moveDir.x;
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