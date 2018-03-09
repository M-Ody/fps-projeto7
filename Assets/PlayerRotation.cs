using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    private Vector3 currentLocal;

    private void Start()
    {
        currentLocal = transform.localEulerAngles; ;
    }

    public void RotateHead (float pitch)
    {
        currentLocal.x = Mathf.Clamp(currentLocal.x -= pitch, -80f,80f);
        transform.localEulerAngles = currentLocal;
    }
}
