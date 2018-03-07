using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolarisCore
{
    public class PlayerMovement : MonoBehaviour
    {

        private Rigidbody _rb;
        private InputHandler _ih;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _ih = GetComponent<InputHandler>();
        }

        void Update()
        {
            var buffer = _ih.GetInputStream();
            Debug.Log("ANY " + buffer[0] + "| W " + buffer[1] + "| A " + buffer[3] + "| S " + buffer[2] + "| D" + buffer[4]);
        }
    }
}
