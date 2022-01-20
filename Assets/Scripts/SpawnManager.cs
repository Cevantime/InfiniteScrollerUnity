using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnRate = 1f;
    public GameObject[] platforms;
    public float maxYGap = 3;
    public float maxYRange = 15f;
    public float xOffset = 10f;
    private GameManager gameManager;
    private float currentY;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.onGameStarted += StartSpawning;
    }

    void StartSpawning()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (gameManager.State == GameManager.GameState.RUNNING)
        {
            SpawnPlatform();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnPlatform()
    {
        GameObject platform = platforms[Random.Range(0, platforms.Length)];
        float newY = Random.Range(currentY - maxYGap, currentY + maxYGap);
        newY = Mathf.Clamp(newY, -maxYRange, maxYRange);
        currentY = newY;
        GameObject p = Instantiate(platform);
        p.transform.position = new Vector3(xOffset, newY, 0);
        if (Random.Range(0, 2) == 0)
        {
            platform = platforms[Random.Range(0, platforms.Length)];
            p = Instantiate(platform);
            p.transform.position = new Vector3(xOffset, newY + maxYGap * 2 * ((Random.Range(0, 2) * 2) - 1), 0);
        }
    }
}
