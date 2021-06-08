
using UnityEngine;

namespace Movement
{
    public class PlayerMovementResponse : MonoBehaviour, IMovementResponse
    {
        [SerializeField] float _smoothTime = 0.1f;
        [SerializeField] float _speed = 10f;
        [SerializeField] Transform _mainCamera;

        float _targetAngle;
        CharacterController _cc;
        float _curVel;

        void Awake()
        {
            _targetAngle = transform.eulerAngles.y;
            _cc = GetComponent<CharacterController>();
        }

        public void Move(Vector3 input)
        {

            Vector3 moveDir = Vector3.Scale(input, new Vector3(1, 0, 1));

            if (moveDir.magnitude >= 0.1f)
            {
                Transform t = transform;
                Vector3 rot = t.eulerAngles;

                _targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
                Vector3 moveVec = Quaternion.Euler(new Vector3(0f, _targetAngle, 0f)) * Vector3.forward * _speed * Time.deltaTime;

                float vertical = input.z;

                if (Mathf.Abs(vertical) <= 0.1f)
                {
                    moveVec *= 0.25f;
                }
                _cc.Move(moveVec);

                float angle = Mathf.SmoothDampAngle(rot.y, _targetAngle, ref _curVel, _smoothTime);
                t.rotation = Quaternion.Euler(new Vector3(rot.x, angle, rot.z));
            }
        }
    }
}