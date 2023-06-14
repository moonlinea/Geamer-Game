using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GemTypeManager gemTypeManager;
    public Transform spawnArea;
    public int gemCount;

    private void Start()
    {
        SpawnGems();
    }

    private void SpawnGems()
    {
        for (int i = 0; i < gemCount; i++)
        {
            GemType randomGemType = GetRandomGemType();
            Vector3 spawnPosition = GetRandomSpawnPosition();

            GameObject gemObject = Instantiate(randomGemType.model, spawnPosition, Quaternion.identity);
            gemObject.tag = randomGemType.gemName;
        }
    }

    private GemType GetRandomGemType()
    {
        int randomIndex = Random.Range(0, gemTypeManager.gemTypes.Count);
        return gemTypeManager.gemTypes[randomIndex];
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(spawnArea.position.x - spawnArea.localScale.x , spawnArea.position.x + spawnArea.localScale.x ),
                                             spawnArea.position.y,
                                             Random.Range(spawnArea.position.z - spawnArea.localScale.z , spawnArea.position.z + spawnArea.localScale.z ));
        return randomPosition;
    }
}
