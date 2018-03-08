using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolarisCore
{
    public class PlayerMovement : MonoBehaviour
    {
        public float velocity = 10f;

        private Rigidbody _rb;
        private InputHandler _ih;
        private PlayerRotation _pr;
        private HeadBob _hb;

        private bool _isMoving;
        
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
            else
            {
                Move(hMov, vMov);
            }
            Rotate(axis);

            if (buffer[(int)EInputNames.AIM]) Aim();
            if (buffer[(int)EInputNames.FIRE]) Fire();
        }
        
        private void Rotate(Vector2 axis)
        {
            Vector3 curLocal = transform.localEulerAngles;
            curLocal.y += axis.x;
            transform.localEulerAngles = curLocal;

            _pr.RotateHead(axis.y);
        }

        private void Fire()
        {
            throw new NotImplementedException();
        }

        private void Aim()
        {
            throw new NotImplementedException();
        }

        private void Move(int hMov, int vMov)
        {
            var movement = Vector3.forward * vMov;
            movement += Vector3.right * hMov;
            movement = Vector3.Normalize(movement) * velocity * Time.deltaTime;
            if (movement.magnitude > 0f)
            {
                transform.Translate(movement);
                if (!_isMoving)
                {
                    _isMoving = true;
                    _hb.StartBob();
                }
            }
            else
            {
                if (_isMoving)
                {
                    _isMoving = false;
                    _hb.StopBob();
                }
            }
        }

        private void Jump()
        {
            throw new NotImplementedException();
        }
    }
}
