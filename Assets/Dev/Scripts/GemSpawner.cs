using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GemTypeManager gemTypeManager;
    public Transform[] spawnArea;
    public int gemCount;

    private void Start()
    {
        
        if (gemCount > spawnArea.Length)
        {
            Debug.LogError("Gem count cannot be greater than the number of spawn areas.");
            return;
        }

        SpawnGems();
        Debug.Log("Gem Count: " + gemCount);
    }

    private void SpawnGems()
    {
        List<int> availableIndices = new List<int>();

        for (int i = 0; i < spawnArea.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < gemCount; i++)
        {
            GemType randomGemType = GetRandomGemType();
            int randomIndex = GetRandomSpawnIndex(availableIndices);
            Transform spawnTransform = spawnArea[randomIndex];

            GameObject gemObject = Instantiate(randomGemType.model, spawnTransform.position, Quaternion.identity);
            gemObject.tag = randomGemType.gemName;

            availableIndices.Remove(randomIndex);
        }
    }

    private GemType GetRandomGemType()
    {
        int randomIndex = Random.Range(0, gemTypeManager.gemTypes.Count);
        return gemTypeManager.gemTypes[randomIndex];
    }

    private int GetRandomSpawnIndex(List<int> availableIndices)
    {
        int randomIndex = Random.Range(0, availableIndices.Count);
        return availableIndices[randomIndex];
    }
}
