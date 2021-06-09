using System;
using UnityEngine;

namespace ProjectPhysics
{
    public class PlayerGravity : MonoBehaviour
    {
        [SerializeField] float _radius = 0.1f;
        [SerializeField] CharacterController _cc;
        [SerializeField] Transform _groundCheck;
        [SerializeField] LayerMask _groundLayerMask;

        [HideInInspector] public Action OnLandedOnGround;

        float _yVel = 0f;
        const float GRAVITY = 9.8f;

        void Update()
        {
            HandleGravity();
            HandleCollision();
        }

        public void AddOnLandedOnGroundListener(Action func)
        {
            OnLandedOnGround += func;
        }

        public void RemoveOnLandedOnGroundListener(Action func)
        {
            OnLandedOnGround -= func;
        }

        void HandleCollision()
        {
            bool grounded = IsGrounded();
            bool falling = _yVel <= 0f;

            if (grounded && falling)
            {
                _yVel = -0.1f;
                OnLandedOnGround?.Invoke();
            }
        }

        void HandleGravity()
        {
            if (_yVel > 0f || !IsGrounded())
            {
                _yVel -= GRAVITY * Time.deltaTime;
                _cc.Move(new Vector3(0f, _yVel * Time.deltaTime, 0f));
            }
        }


        public bool IsGrounded()
        {
            return Physics.OverlapSphere(_groundCheck.position, _radius, _groundLayerMask).Length >= 1;
        }

        public void SetYVel(float yVel)
        {
            _yVel = yVel;
        }


    }
}