using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 inputDirection;
    public float input;

    public float speed=1f;
    public float turnX=1f,turnY=1f;
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.AddForce(rigid.transform.forward * speed * input);
        rigid.AddTorque(Vector3.up * inputDirection.x *turnX + rigid.transform.right * inputDirection.y * turnY);
    }
}
