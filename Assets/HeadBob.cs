using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {

    // Precisa iniciar em 0.2 e terminar em 0.2
    public AnimationCurve bobCurve;
    [Range(0f, 3f)]
    public float bounceRate = 2f;

    // Precisa iniciar em 0.2 e terminar em 0.2
    public AnimationCurve jumpCurve;
    //public float jumpAnimationSpeed = ;

    private bool _isBobbing;
    private bool _requestedStopWalking;
    private float _cycleTime;
    private float _cycleTimeJump;

    public void StartWalkingBob()
    {
        _requestedStopWalking = false;
        _isBobbing = true;
    }

    public void StopWalkingBob()
    {
        _requestedStopWalking = true;
    }

    public void StopBobImmediately()
    {
        _isBobbing = false;
        _cycleTime = 0f;
        transform.localPosition = new Vector3(transform.localPosition.x, bobCurve.Evaluate(1f), transform.localPosition.z);
    }

    void Update()
    {
        if (Mathf.Abs(1 - _cycleTime) < 0.05f)
        {
            _cycleTime = 0f;
            if (_requestedStopWalking)
            {
                _isBobbing = false;
                transform.localPosition = new Vector3(transform.localPosition.x, bobCurve.Evaluate(1f), transform.localPosition.z);
            }
        }

        if (_isBobbing)
        {
            _cycleTime += Time.deltaTime * bounceRate;
            float y = bobCurve.Evaluate(_cycleTime);
            transform.localPosition = new Vector3(transform.localPosition.x,y, transform.localPosition.z);
        }



        //////////////// DEBUG
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartWalkingBob();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            StopWalkingBob();
        }
    }

    // True quando pronto
    // Possível melhoria futura dessa parte de animação do pulo
    public bool JumpAnimation()
    {
        _cycleTimeJump += Time.deltaTime * 2f;
        float y = jumpCurve.Evaluate(_cycleTimeJump);
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        if (_cycleTimeJump >= 0.95f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, bobCurve.Evaluate(1f), transform.localPosition.z);
            _cycleTimeJump = 0f;
            return true;
        }
        return false;
    }
}
