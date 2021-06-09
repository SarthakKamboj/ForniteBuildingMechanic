﻿using System;
using UnityEngine;

namespace Player
{
    public class PlayerGravity : MonoBehaviour
    {
        [SerializeField] float _radius = 0.1f;
        [SerializeField] CharacterController _cc;
        [SerializeField] Transform _groundCheck;
        [SerializeField] LayerMask _groundLayerMask;
        [SerializeField] float jumpHeight = 10f;
        [SerializeField] Animator _animator;

        float gravity = 9.8f;
        float yVel = 0f;

        bool jumped = true;

        int JumpHash;

        void Awake()
        {
            JumpHash = Animator.StringToHash("Jumping");
        }

        void Update()
        {
            HandleGravity();

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                yVel = 2 * gravity * jumpHeight;
                jumped = true;
                _animator.SetBool(JumpHash, true);
            }

            HandleCollision();
        }

        void HandleCollision()
        {
            bool grounded = IsGrounded();
            bool falling = yVel <= 0f;

            if (jumped)
            {
                if (falling && grounded)
                {
                    jumped = false;
                    yVel = -0.1f;
                    _animator.SetBool(JumpHash, false);
                }
            }
            else
            {
                yVel = -0.1f;
            }
        }

        void HandleGravity()
        {
            yVel -= gravity * Time.deltaTime;
            _cc.Move(new Vector3(0f, yVel * Time.deltaTime, 0f));
        }


        bool IsGrounded()
        {
            return Physics.OverlapSphere(_groundCheck.position, _radius, _groundLayerMask).Length >= 1;
        }


    }
}