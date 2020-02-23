using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class KillMessages
    {
        public string[] messages;
        public int threshold = 10;
    }

    public GameObject[] enemyPrefab;

    public float radius = 10f;

    public Vector2 enemySpeed = new Vector2(3f,15f);

    public Vector2 enemySize = new Vector2(1f, 20f);
    public AnimationCurve sizePropability = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
    public Vector2 enemyHealth = new Vector2(125f, 3000f);

    public float spawnTime = 1f;
    public int maximumFish = 50;
    public int currentFish = 0;

    public int killedFish = 0;
    public KillMessages[] killMessages;

    public TextMeshProUGUI messageText, killCount;

    private void Start()
    {
        foreach(var fish in FindObjectsOfType<BotControls>())
        {
            fish.GetComponentInChildren<Health>().OnDeath.AddListener(FishDeath);
            currentFish++;
        }

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
        health.OnDeath.AddListener(FishDeath);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void FishDeath()
    {
        currentFish--;
        killedFish++;
        killCount.text = killedFish.ToString();
        for(int i = 0; i < killMessages.Length; i++)
        {
            if(killedFish < killMessages[i].threshold)
            {
                messageText.text = killMessages[i].messages[Random.Range(0, killMessages[i].messages.Length)];
                return;
            }
        }
    }

}
