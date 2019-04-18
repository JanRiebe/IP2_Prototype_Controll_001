using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject BirdPrefab;
    public Vector2 spawnRandomBetween;
    public float nextSpawnIn;

    private void Start()
    {
        nextSpawnIn = Random.Range(spawnRandomBetween.x, spawnRandomBetween.y);
    }

    // Update is called once per frame
    void Update()
    {
        nextSpawnIn -= Time.deltaTime;

        if(nextSpawnIn <= 0)
        {
            nextSpawnIn = Random.Range(spawnRandomBetween.x, spawnRandomBetween.y);
            Instantiate(BirdPrefab, new Vector3(-13, Camera.main.transform.position.y + Random.Range(-5,5),-0.5f), Quaternion.identity);
        }
    }
}
