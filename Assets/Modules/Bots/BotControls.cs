using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotControls : MonoBehaviour
{
    [Header("Setup")]
    public Transform lookTarget;
    Movement move;

    [Header("Runtime")]
    public Transform chaseTarget;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        lookTarget.rotation = Quaternion.LookRotation((chaseTarget.position- transform.position).normalized);
        //Vector3 chasing = lookTarget.rotation*(chaseTarget.position - transform.position);
        //move.inputDirection = new Vector3(chasing.x,chasing.z,0f).normalized;
        move.inputDirection.y = Mathf.Clamp(Vector3.Distance(transform.position, chaseTarget.position),-1f,1f);
    }
}
