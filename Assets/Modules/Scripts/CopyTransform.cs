using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    [System.Flags]
    public enum Options { None, Position, Rotation}

    public Transform target;
    public float positionSpeed, rotationSpeed;
    public Options options;


    // Update is called once per frame
    void Update()
    {
        if (options.HasFlag(Options.Position)){
            if (positionSpeed == 0f)
                SetPosition();
            else
                LerpPosition();
        }

        if (options.HasFlag(Options.Rotation)){
            if (rotationSpeed == 0f)
                SetRotation();
            else
                LerpRotation();
        }
    }

    void SetPosition()
    {
        transform.position = target.position;
    }

    void SetRotation()
    {
        transform.rotation = target.rotation;
    }

    void LerpPosition()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * positionSpeed);
    }
    void LerpRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * rotationSpeed);
    }

}
