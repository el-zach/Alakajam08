using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToVolume : MonoBehaviour
{
    public AnimationCurve blend = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
    public Vector2 volume= Vector2.up;
    public Vector2 distance = new Vector2(1f,100f);
    AudioSource source;
    Transform target;

    public float db;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        target = Camera.main.transform;
    }

    private void Update()
    {
        float _distance = Vector3.Distance(transform.position, target.position);
        db = 1f-((_distance-distance.x)/(distance.y-distance.x));
        source.volume = Mathf.Max(
            blend.Evaluate( Mathf.Clamp01(db))*volume.y,
            volume.x);
    }

    private void OnDrawGizmosSelected()
    {
        if (target)
        {
            
            Gizmos.DrawWireSphere(transform.position, Vector3.Distance(transform.position, target.position));
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance.y);
    }

}
