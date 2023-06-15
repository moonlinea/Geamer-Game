using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GemTypeManager gemTypeManager;
    public Transform[] spawnArea;
    public int gemCount;
    public float respawnDelay = 5f; // Yeniden spawn süresi

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

            StartRespawnGems(); // Yeniden spawnlama iþlemini baþlat
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
        Collider[] colliders = Physics.OverlapBox(gridTransform.position, Vector3.one * 0.5f); // Grid alanýnda bulunan collider'larý al

        foreach (Collider collider in colliders)
        {
            if (collider.tag.StartsWith("Gem_")) // Eðer Gem etiketine sahip bir obje varsa, grid boþ deðildir
                return false;
        }

        return true; // Hiçbir Gem objesi yoksa, grid boþtur
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

        StartCoroutine(RespawnGemsCoroutine()); // Yeniden spawnlama iþlemini tekrarla
    }

    private void StartRespawnGems()
    {
        StartCoroutine(RespawnGemsCoroutine()); // Yeniden spawnlama coroutine'ini baþlat
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
