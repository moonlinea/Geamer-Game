using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GemTypeManager gemTypeManager;
    public Transform[] spawnArea;
    public int gemCount;
    public float respawnDelay = 5f; // Yeniden spawn s�resi

    private void Start()
    {
        GridGenerator gridGenerator = FindObjectOfType<GridGenerator>();
        if (gridGenerator != null)
        {
            spawnArea = gridGenerator.spawnArea;
            gemCount = spawnArea.Length;

            if (gemCount > spawnArea.Length)
            {
                Debug.LogError("Gem count cannot be greater than the number of spawn areas.");
                return;
            }

            SpawnGems();
            Debug.Log("Gem Count: " + gemCount);

            StartRespawnGems(); // Yeniden spawnlama i�lemini ba�lat
        }
        else
        {
            Debug.LogError("GridGenerator not found in the scene.");
        }
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

    public bool IsGridEmpty(Transform gridTransform)
    {
        Collider[] colliders = Physics.OverlapBox(gridTransform.position, Vector3.one * 0.5f); // Grid alan�nda bulunan collider'lar� al

        foreach (Collider collider in colliders)
        {
            if (collider.tag.StartsWith("Gem_")) // E�er Gem etiketine sahip bir obje varsa, grid bo� de�ildir
                return false;
        }

        return true; // Hi�bir Gem objesi yoksa, grid bo�tur
    }

    private IEnumerator RespawnGemsCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        for (int i = 0; i < spawnArea.Length; i++)
        {
            if (IsGridEmpty(spawnArea[i]))
            {
                GemType randomGemType = GetRandomGemType();
                GameObject gemObject = Instantiate(randomGemType.model, spawnArea[i].position, Quaternion.identity);
                gemObject.tag = randomGemType.gemName;
            }
        }

        StartCoroutine(RespawnGemsCoroutine()); // Yeniden spawnlama i�lemini tekrarla
    }

    private void StartRespawnGems()
    {
        StartCoroutine(RespawnGemsCoroutine()); // Yeniden spawnlama coroutine'ini ba�lat
    }
}



//using System.Collections.Generic;
//using UnityEngine;

//public class GemSpawner : MonoBehaviour
//{
//    public GemTypeManager gemTypeManager;
//    public Transform[] spawnArea;
//    public int gemCount;

//    private void Start()
//    {
//        GridGenerator gridGenerator = FindObjectOfType<GridGenerator>();
//        if (gridGenerator != null)
//        {
//            spawnArea = gridGenerator.spawnArea;
//            gemCount = spawnArea.Length;

//            if (gemCount > spawnArea.Length)
//            {
//                Debug.LogError("Gem count cannot be greater than the number of spawn areas.");
//                return;
//            }

//            SpawnGems();
//            Debug.Log("Gem Count: " + gemCount);
//        }
//        else
//        {
//            Debug.LogError("GridGenerator not found in the scene.");
//        }
//    }

//    private void SpawnGems()
//    {
//        List<int> availableIndices = new List<int>();

//        for (int i = 0; i < spawnArea.Length; i++)
//        {
//            availableIndices.Add(i);
//        }

//        for (int i = 0; i < gemCount; i++)
//        {
//            GemType randomGemType = GetRandomGemType();
//            int randomIndex = GetRandomSpawnIndex(availableIndices);
//            Transform spawnTransform = spawnArea[randomIndex];

//            GameObject gemObject = Instantiate(randomGemType.model, spawnTransform.position, Quaternion.identity);
//            gemObject.tag = randomGemType.gemName;

//            availableIndices.Remove(randomIndex);
//        }
//    }

//    private GemType GetRandomGemType()
//    {
//        int randomIndex = Random.Range(0, gemTypeManager.gemTypes.Count);
//        return gemTypeManager.gemTypes[randomIndex];
//    }

//    private int GetRandomSpawnIndex(List<int> availableIndices)
//    {
//        int randomIndex = Random.Range(0, availableIndices.Count);
//        return availableIndices[randomIndex];
//    }
//}
