using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _smoothTime = 0.1f;
        [SerializeField] float _speed = 10f;
        [SerializeField] Transform _mainCamera;


        float _targetAngle;
        float _curVel;
        CharacterController _cc;

        void Awake()
        {
            _targetAngle = transform.eulerAngles.y;
            _cc = GetComponent<CharacterController>();
        }

        void Update()
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");

            Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;
            if (moveDir.magnitude >= 0.1f)
            {
                Transform t = transform;
                Vector3 rot = t.eulerAngles;

                _targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
                Vector3 moveVec = Quaternion.Euler(new Vector3(0f, _targetAngle, 0f)) * Vector3.forward * _speed * Time.deltaTime;
                _cc.Move(moveVec);

                float angle = Mathf.SmoothDampAngle(rot.y, _targetAngle, ref _curVel, _smoothTime);
                t.rotation = Quaternion.Euler(new Vector3(rot.x, angle, rot.z));
            }
        }
    }
}