using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    //cheapfollow
    public Transform target;
    public float speed = 1f;
    Vector3 offset;
    public Vector2 camSensitivity=Vector2.one;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        oldMousePos = ScreenToRelative(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
        CameraRotationUpdate();
    }

    public Vector3 oldMousePos;
    void CameraRotationUpdate()
    {
        Vector3 mousePos = ScreenToRelative(Input.mousePosition);
        Vector2 delta = mousePos - oldMousePos;
        oldMousePos = mousePos;

        //transform.Rotate(delta.y, delta.x, 0f);
        transform.Rotate(Vector3.up, delta.x * camSensitivity.x);
        transform.Rotate(Vector3.right, delta.y * camSensitivity.y);
        
    }

    public Vector3 ScreenToRelative(Vector3 _in)
    {
        return new Vector3(_in.x / Screen.width, _in.y / Screen.height, 0f);
    }

}
