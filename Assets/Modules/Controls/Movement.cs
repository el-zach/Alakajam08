using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Setup")]
    public float speed=1f;
    public float startBoost = 1f;
    public float sideBoostStrength = 2f;
    public float sideSpeed = 1f;
    // public float turnX=1f,turnY=1f;
    public float turnDivisor = 1f;
    public Transform lookTransform;

    [Header("Runtime")]
    public Vector3 inputDirection;
    public float inputBoost;
    public bool inputRecenter = false;

    Rigidbody rigid;
    

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.AddForce(lookTransform.forward * inputDirection.y * speed);
        rigid.AddForce(lookTransform.right * inputDirection.x * sideSpeed);
        
        if(inputDirection.sqrMagnitude>0.1f || inputRecenter)
            RotateTowards();
    }

    public void SideBoost(float value)
    {
        rigid.AddForce(lookTransform.right * (value>0f?1f:-1f) * sideBoostStrength, ForceMode.Impulse);
    }

    public void StartBoost(float value)
    {
        if(Mathf.Abs(value) <1f)
            rigid.AddForce(lookTransform.forward * (value > 0f ? 1f : -1f) * startBoost, ForceMode.Impulse);
        else
            rigid.AddForce(lookTransform.forward * value, ForceMode.Impulse);
    }

    public void Recenter(bool value)
    {
        inputRecenter = value;
    }

    //void OldMovement()
    //{
    //    rigid.AddForce(rigid.transform.forward * speed * inputBoost);
    //    rigid.AddTorque(Vector3.up * inputDirection.x * turnX + rigid.transform.right * inputDirection.y * turnY);
    //}

    void RotateTowards()
    {
        Transform targetTransform = lookTransform;
        Quaternion AngleDifference = Quaternion.FromToRotation(rigid.transform.up, targetTransform.up);
        float AngleToCorrect = Quaternion.Angle(targetTransform.rotation, rigid.transform.rotation);
        Vector3 Perpendicular = Vector3.Cross(targetTransform.up, targetTransform.forward);
        if (Vector3.Dot(rigid.transform.forward, Perpendicular) < 0)
            AngleToCorrect *= -1;
        Quaternion Correction = Quaternion.AngleAxis(AngleToCorrect, targetTransform.up);

        Vector3 MainRotation = RectifyAngleDifference((AngleDifference).eulerAngles);
        Vector3 CorrectiveRotation = RectifyAngleDifference((Correction).eulerAngles);
        rigid.AddTorque(((MainRotation - CorrectiveRotation / 2) - rigid.angularVelocity) / turnDivisor, ForceMode.Force);
    }
    private Vector3 RectifyAngleDifference(Vector3 angdiff)
    {
        if (angdiff.x > 180) angdiff.x -= 360;
        if (angdiff.y > 180) angdiff.y -= 360;
        if (angdiff.z > 180) angdiff.z -= 360;
        return angdiff;
    }
}
