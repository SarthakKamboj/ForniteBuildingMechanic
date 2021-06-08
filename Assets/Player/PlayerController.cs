using ControlInput;
using Movement;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {

        IInput _input;
        IMovementResponse[] _movementResponses;

        void Awake()
        {
            _movementResponses = GetComponents<IMovementResponse>();
            _input = GetComponent<IInput>();
        }

        void Update()
        {
            float horizontal = _input.GetHorizontalInput();
            float vertical = _input.GetVerticalInput();

            Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;

            foreach (IMovementResponse movementResponse in _movementResponses)
            {
                movementResponse.Move(moveDir);
            }

        }
    }
}