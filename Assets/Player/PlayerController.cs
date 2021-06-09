using ControlInput;
using Movement;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {

        IInputProvider _input;
        IMovementResponse _movementResponse;

        void Awake()
        {
            _movementResponse = GetComponent<IMovementResponse>();
            _input = GetComponent<IInputProvider>();
        }

        void Update()
        {
            float horizontal = _input.GetHorizontalInput();
            float vertical = _input.GetVerticalInput();

            Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;

            _movementResponse.Move(moveDir);
        }
    }
}