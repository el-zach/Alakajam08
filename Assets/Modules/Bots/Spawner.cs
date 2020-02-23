using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    public float radius = 10f;

    public Vector2 enemySpeed = new Vector2(3f,15f);

    public Vector2 enemySize = new Vector2(1f, 20f);
    public AnimationCurve sizePropability = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
    public Vector2 enemyHealth = new Vector2(125f, 3000f);

    public float spawnTime = 1f;
    public int maximumFish = 50;
    public int currentFish = 0;

    private void Start()
    {
        StartCoroutine(RepeatedSpawning());
    }

    IEnumerator RepeatedSpawning()
    {
        yield return new WaitForSeconds(spawnTime);
        if(currentFish<maximumFish)
            SpawnFishAt(Random.insideUnitSphere * radius + transform.position);
        StartCoroutine(RepeatedSpawning());
    }

    //public Transform playerTransform;

    public void SpawnFishAt(Vector3 pos)
    {
        if (pos.y <= -60f) pos.y = -58f;
        GameObject clone =Instantiate(enemyPrefab[Random.Range(0,enemyPrefab.Length)], pos, Quaternion.identity);
        BotControls bot = clone.GetComponentInChildren<BotControls>();
        //bot.
        Movement move = clone.GetComponentInChildren<Movement>();
        move.speed = Random.Range(enemySpeed.x, enemySpeed.y);

        float mod = sizePropability.Evaluate(Random.value);
        float sizeMod = mod * (enemySize.y - enemySize.x) + enemySize.x;
        bot.transform.localScale = Vector3.one * sizeMod;
        bot.GetComponent<SphereCollider>().radius /= sizeMod;

        Health health = clone.GetComponentInChildren<Health>();
        health.health = Mathf.FloorToInt(mod + (enemyHealth.y-enemyHealth.x)+enemyHealth.x );

        currentFish++;
        health.OnDeath.AddListener(() => currentFish--);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
