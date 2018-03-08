using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {

    public AnimationCurve bobCurve;
    [Range(0f, 3f)]
    public float bounceRate = 2f;
    [Range(0f, 1f)]
    public float movementRate = 0.1f;

    private bool _isBobbing;
    private bool _requestedStop;
    private float _cycleTime;

    public void StartBob()
    {
        _requestedStop = false;
        _isBobbing = true;
    }

    public void StopBob()
    {
        _requestedStop = true;
    }

    void Update()
    {
        if (Mathf.Abs(1 - _cycleTime) < 0.05f)
        {
            _cycleTime = 0f;
            if (_requestedStop) _isBobbing = false;
        }

        if (_isBobbing)
        {
            _cycleTime += Time.deltaTime * bounceRate;
            float y = bobCurve.Evaluate(_cycleTime) * movementRate;
            transform.localPosition = new Vector3(transform.localPosition.x,y, transform.localPosition.z);
        }



        //////////////// DEBUG
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartBob();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            StopBob();
        }
    }
}
