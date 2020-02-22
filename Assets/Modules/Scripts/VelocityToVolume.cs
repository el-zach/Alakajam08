using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityToVolume : MonoBehaviour
{
    public Rigidbody target;
    AudioSource source;
    public AnimationCurve blend = new AnimationCurve(new Keyframe(0f,0f),new Keyframe(1f,1f));
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = Mathf.Clamp(blend.Evaluate(target.velocity.magnitude),0.05f,1f);
    }
}
