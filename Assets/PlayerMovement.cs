using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolarisCore
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float DISTANCE_TO_GROUND = 1f;

        public float velocity = 10f;
        public float jumpForce = 150f;

        private Rigidbody _rb;
        private InputHandler _ih;
        private PlayerRotation _pr;
        private HeadBob _hb;

        private float _velocityMod = 1f;

        private bool _isMoving;
        private bool _isGrounded;
        private bool _isAnimatingJump;
        private bool _isAiming;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _ih = GetComponent<InputHandler>();
            _pr = GetComponentInChildren<PlayerRotation>();
            _hb = GetComponentInChildren<HeadBob>();

            // TODO: Mover isso para outro lugar
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            HandleMovimentation();
            HandleAnimations();
        }

        private void HandleMovimentation()
        {
            bool[] buffer = _ih.GetInputStream();

            int hMov, vMov;
            Vector2 axis = _ih.GetAxisInput();

            vMov = buffer[(int)EInputNames.FORWARD] ? 1 : 0;
            vMov = buffer[(int)EInputNames.BACKWARD] ? -1 : vMov;

            hMov = buffer[(int)EInputNames.RIGHT] ? 1 : 0;
            hMov = buffer[(int)EInputNames.LEFT] ? -1 : hMov;

            if (buffer[(int)EInputNames.JUMP])
            {
                Jump();
            }

            Move(hMov, vMov);
            Rotate(axis);

            if (buffer[(int)EInputNames.AIM]) Aim();
            if (buffer[(int)EInputNames.FIRE]) Fire();
        }
        
        private void Rotate(Vector2 axis)
        {
            transform.Rotate(transform.up * axis.x);

            _pr.RotateHead(axis.y);
        }

        private void Fire()
        {
            throw new NotImplementedException();
        }

        private void Aim()
        {
            if (!_isGrounded) return;

            if (_isAiming)
            {
                if(_isMoving)
                {
                    _hb.StartWalkingBob();
                }
                _isAiming = false;
                _velocityMod = 1f;
            }
            else
            {
                _hb.StopBobImmediately();
                _isAiming = true;
                _velocityMod = 0.4f;
            }
        }

        private void Move(int hMov, int vMov)
        {
            var movement = Vector3.forward * vMov;
            movement += Vector3.right * hMov;
            movement = Vector3.Normalize(movement) * velocity * Time.deltaTime * _velocityMod;
            if (movement.magnitude > 0f)
            {
                transform.Translate(movement);
                if (!_isMoving && !_isAiming)
                {
                    _isMoving = true;
                    _hb.StartWalkingBob();
                }
            }
            else
            {
                if (_isMoving)
                {
                    _isMoving = false;
                    _hb.StopWalkingBob();
                }
            }
        }

        private void Jump()
        {
            if (_isGrounded)
            {
                Aim();
                _hb.StopBobImmediately();
                _rb.AddForce(Vector3.up * jumpForce);
                _isGrounded = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                _isAnimatingJump = true;
            }
        }

        private void HandleAnimations()
        {
            if (_isAnimatingJump)
            {
                _isGrounded = _hb.JumpAnimation();
                _isAnimatingJump = !_isGrounded;
                if (!_isAnimatingJump && _isMoving)
                {
                    _hb.StartWalkingBob();
                }
            }
        }
    }
}
