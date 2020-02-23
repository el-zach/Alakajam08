using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVFX : MonoBehaviour
{
    public static DamageVFX Instance;
    private void Awake()
    {
        Instance = this;
    }
    ParticleSystem pSys;
    // Start is called before the first frame update
    void Start()
    {
        pSys = GetComponent<ParticleSystem>();
    }

    public static void DamageAt(Vector3 pos, Vector3 normal)
    {
        //Instance.pSys.Emit(pos, normal*10f, 1f, 1f, Color.white);
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = pos;
        emitParams.rotation = Random.Range(0f, 45f);
        emitParams.velocity = normal * 1f;
        Instance.pSys.Emit(emitParams, 3);
    }
}
