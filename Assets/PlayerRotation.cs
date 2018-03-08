using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {
    public void RotateHead (float pitch)
    {
        Vector3 curLocal = transform.localEulerAngles;
        curLocal.x -= pitch;
        transform.localEulerAngles = curLocal;
    }
}
