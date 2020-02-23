using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    public Vector2 damage = new Vector2(400, 600);
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health)
        {
            int vla = health.Damage(Random.Range((int)damage.x, (int)damage.y));
            UserInterface.RenderDamageNumbers(vla, other.transform.position);
        }
    }
}
