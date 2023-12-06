using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GemTypeManager gemTypeManager;
    public Transform[] spawnArea;
    public int gemCount;
    public float respawnDelay = 5f; 

    private void Start()
    {
        GridGenerator gridGenerator = FindObjectOfType<GridGenerator>();
        if (gridGenerator != null)
        {
            spawnArea = gridGenerator.spawnArea; // GridGenerator'dan spawn alanlarýný al
            gemCount = spawnArea.Length;

            if (gemCount > spawnArea.Length)
            {
                Debug.LogError("Gem count cannot be greater than the number of spawn areas.");
                return;
            }

            SpawnGems(); 

            StartRespawnGems(); 
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
            GemType randomGemType = GetRandomGemType(); // Rastgele bir Gem türü al
            int randomIndex = GetRandomSpawnIndex(availableIndices); // Rastgele bir spawn alaný indeksi al
            Transform spawnTransform = spawnArea[randomIndex]; // Rastgele spawn alanýnýn transformunu al

            GameObject gemObject = Instantiate(randomGemType.model, spawnTransform.position, Quaternion.identity); // Gem objesini spawn alanýna instantiate et
            gemObject.tag = randomGemType.gemName; // Gem objesinin etiketini Gem türüne göre ayarla

            availableIndices.Remove(randomIndex); // Kullanýlan spawn alanýný listeden çýkar
        }
    }

    private GemType GetRandomGemType()
    {
        int randomIndex = Random.Range(0, gemTypeManager.gemTypes.Count); // Gem türlerinin sayýsý arasýnda rastgele bir indeks al
        return gemTypeManager.gemTypes[randomIndex]; // Rastgele seçilen Gem türünü döndür
    }

    private int GetRandomSpawnIndex(List<int> availableIndices)
    {
        int randomIndex = Random.Range(0, availableIndices.Count); // Kullanýlabilir spawn alanlarý listesinin boyutu arasýnda rastgele bir indeks al
        return availableIndices[randomIndex]; // Rastgele seçilen spawn alaný indeksini döndür
    }

    public bool IsGridEmpty(Transform gridTransform)
    {
        Collider[] colliders = Physics.OverlapBox(gridTransform.position, Vector3.one * 0.5f); // Grid alanýnda bulunan collider'larý al

        foreach (Collider collider in colliders)
        {
            if (collider.tag.StartsWith("Gem_")) 
                return false;
        }

        return true; 
    }

    private IEnumerator RespawnGemsCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        for (int i = 0; i < spawnArea.Length; i++)
        {
            if (IsGridEmpty(spawnArea[i])) // Eðer spawn alaný boþ ise
            {
                GemType randomGemType = GetRandomGemType(); // Rastgele bir Gem türü al
                GameObject gemObject = Instantiate(randomGemType.model, spawnArea[i].position, Quaternion.identity); // Gem objesini spawn alanýna instantiate et
                gemObject.tag = randomGemType.gemName; // Gem objesinin etiketini Gem türüne göre ayarla
            }
        }

        StartCoroutine(RespawnGemsCoroutine()); // Yeniden spawnlama iþlemini tekrarla
    }

    private void StartRespawnGems()
    {
        StartCoroutine(RespawnGemsCoroutine()); // Yeniden spawnlama coroutine'ini baþlat
    }
}
