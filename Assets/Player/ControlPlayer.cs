using UnityEngine;

namespace Player
{
    public class ControlPlayer : MonoBehaviour
    {

        [SerializeField] CharacterController cc;
        [SerializeField] float speed = 10f;
        [SerializeField] float minVerticalAngle = -45f, maxVerticalAngle = 45f;
        [SerializeField] float mouseXSensitivity = 2f, mouseYSensitivity = 2f;

        float xAngleRot = 0f;

        void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float xInput = Input.GetAxis("Horizontal");
            float zInput = Input.GetAxis("Vertical");

            if (Mathf.Abs(xInput) >= 0.1f)
            {
                cc.Move(transform.right * speed * Time.deltaTime * Mathf.Sign(xInput));
            }

            if (Mathf.Abs(zInput) >= 0.1f)
            {
                cc.Move(transform.forward * speed * Time.deltaTime * Mathf.Sign(zInput));
            }

            float normedInput = Mathf.Sqrt(xInput * xInput + zInput * zInput);

            float h = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
            float v = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

            xAngleRot = Clamp(minVerticalAngle, maxVerticalAngle, xAngleRot - v);

            transform.rotation = Quaternion.Euler(new Vector3(xAngleRot, transform.localEulerAngles.y, 0f));
            transform.Rotate(Vector3.up * h);
        }

        float Clamp(float min, float max, float val)
        {
            return Mathf.Max(min, Mathf.Min(val, max));
        }

    }
}