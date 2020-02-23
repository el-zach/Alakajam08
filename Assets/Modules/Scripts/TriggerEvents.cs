using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public UnityEvent OnPlayerEnter, OnPlayerExit, OnPlayerStay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.CompareTag("Player"))
            OnPlayerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.CompareTag("Player"))
            OnPlayerExit.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.CompareTag("Player"))
            OnPlayerStay.Invoke();
    }

}
