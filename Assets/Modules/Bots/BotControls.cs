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
        curvingAxis = Random.onUnitSphere;
        if (!chaseTarget) StartCoroutine(RandomCurveTimed(Random.Range(3f, 12f)));
    }

    // Update is called once per frame
    void Update()
    {
        if (chaseTarget) Chasing();
        else Curves();
    }

    void Chasing()
    {
        Vector3 dir = chaseTarget.position - transform.position;
        lookTarget.rotation = Quaternion.LookRotation(dir, Vector3.up);
        move.inputDirection.y = 1f;
    }

    Vector3 curvingAxis = Vector3.up;
    //Vector3 targetCurvingAxis = Vector3.up;
    void Curves()
    {
        //curvingAxis = Vector3.Lerp(curvingAxis, targetCurvingAxis, Time.deltaTime );
        lookTarget.Rotate(curvingAxis, Time.deltaTime * 2f);
        move.inputDirection.y = 0.5f;
    }

    IEnumerator RandomCurveTimed(float t)
    {
        yield return new WaitForSeconds(t);

        if (!chaseTarget)
        {
            curvingAxis = Random.onUnitSphere;
            StartCoroutine(RandomCurveTimed(Random.Range(3f, 12f)));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody && other.attachedRigidbody.CompareTag("Player"))
        {
            chaseTarget = other.attachedRigidbody.transform;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        foreach(var contact in collision.contacts)
        {
            if (contact.otherCollider.attachedRigidbody && contact.otherCollider.attachedRigidbody.CompareTag("Player"))
            {
                contact.otherCollider.GetComponent<Health>().Damage(1);
            }
        }
    }
}
