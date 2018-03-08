using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolarisCore
{
    public class InputHandler : MonoBehaviour
    {

        private bool[] _inputBuffer = new bool[(int)EInputNames.TOTAL_INPUTS];

        // Pode ser alterado para receber teclas dinâmicas, apenas é necessário
        // que outra classe altere os arrays de input desta classe, alterando os keycodes
        // de cada ação
        // O único problema que permanece é o dos Axis, que provavelmente deve ser feito manualmente
        // para todas possibilidades

        private KeyCode[] _forwardInput = new KeyCode[2];
        private KeyCode[] _backwardInput = new KeyCode[2];
        private KeyCode[] _leftInput = new KeyCode[2];
        private KeyCode[] _rightInput = new KeyCode[2];
        private KeyCode[] _jumpInput = new KeyCode[2];
        private KeyCode[] _fireInput = new KeyCode[2];
        private KeyCode[] _aimInput = new KeyCode[2];

        void Start()
        {
            // TODO: if (InputMode.KEYBOARD)
            SetUpForKeyboard();
        }

        void Update()
        {
            HandleInput(EInputNames.FORWARD, _forwardInput);
            HandleInput(EInputNames.BACKWARD, _backwardInput);
            HandleInput(EInputNames.LEFT, _leftInput);
            HandleInput(EInputNames.RIGHT, _rightInput);
            HandleInput(EInputNames.JUMP, _jumpInput);
            HandleInput(EInputNames.FIRE, _fireInput);
            HandleInput(EInputNames.AIM, _aimInput);
        }

        public bool[] GetInputStream()
        {
            return _inputBuffer;
        }

        public Vector2 GetAxisInput()
        {
            var axisInput = new Vector2();
            // TODO: if (InputMode.KEYBOARD
            axisInput.x = Input.GetAxis("Mouse X");
            axisInput.y = Input.GetAxis("Mouse Y");
            return axisInput;
        }

        private void SetUpForKeyboard()
        {
            _forwardInput[0] = KeyCode.W; _forwardInput[1] = KeyCode.UpArrow;
            _backwardInput[0] = KeyCode.S; _forwardInput[1] = KeyCode.DownArrow;
            _leftInput[0] = KeyCode.A; _forwardInput[1] = KeyCode.LeftArrow;
            _rightInput[0] = KeyCode.D; _forwardInput[1] = KeyCode.RightArrow;
            _jumpInput[0] = KeyCode.Space; _forwardInput[1] = KeyCode.None;
            _fireInput[0] = KeyCode.Mouse0; _forwardInput[1] = KeyCode.None;
            _aimInput[0] = KeyCode.Mouse1; _forwardInput[1] = KeyCode.None;
        }

        private void HandleInput(EInputNames action, KeyCode[] keyCodes)
        {
            if (_inputBuffer[(int)action] == true)
            {
                for (int i = 0; i < keyCodes.Length; i++)
                {
                    if (Input.GetKeyUp(keyCodes[i]))
                    {
                        _inputBuffer[(int)action] = false;
                        return;
                    }
                }
            }

            else
            {
                for (int i = 0; i < keyCodes.Length; i++)
                {
                    if (Input.GetKeyDown(keyCodes[i]))
                    {
                        _inputBuffer[(int)action] = true;
                        return;
                    }
                }
            }
        }
    }
}
