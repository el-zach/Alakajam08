using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVFX : MonoBehaviour
{
    public static DamageVFX Instance;
    public ParticleSystem deathFX;
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

    public static void DamageAt(Vector3 pos, Vector3 normal, Color color)
    {
        //Instance.pSys.Emit(pos, normal*10f, 1f, 1f, Color.white);
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = pos;
        emitParams.rotation = Random.Range(0f, 45f);
        emitParams.velocity = normal * 1f;
        emitParams.startColor = color;
        Instance.pSys.Emit(emitParams, 3);
    }

    public static void DeathAt(Vector3 pos, Vector3 normal)
    {
        
        // emitParams.rotation = Random.Range(0f, 45f);
        // emitParams.velocity = normal * 0.1f;

        ParticleSystem.MainModule main = Instance.deathFX.main;
        for (int i = 0; i < 3; i++)
        {
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position = pos;

            emitParams.startSize = Random.Range(main.startSize.constantMin, main.startSize.constantMax);
            emitParams.startColor = Color.Lerp(main.startColor.colorMin, main.startColor.colorMax, Random.value);
            Instance.deathFX.Emit(emitParams, 1);
        }
    }
}
