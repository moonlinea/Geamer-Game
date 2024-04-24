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
            spawnArea = gridGenerator.spawnArea; // GridGenerator'dan spawn alanlar�n� al
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
            GemType randomGemType = GetRandomGemType(); // Rastgele bir Gem t�r� al
            int randomIndex = GetRandomSpawnIndex(availableIndices); // Rastgele bir spawn alan� indeksi al
            Transform spawnTransform = spawnArea[randomIndex]; // Rastgele spawn alan�n�n transformunu al

            GameObject gemObject = Instantiate(randomGemType.model, spawnTransform.position, Quaternion.identity); // Gem objesini spawn alan�na instantiate et
            gemObject.tag = randomGemType.gemName; // Gem objesinin etiketini Gem t�r�ne g�re ayarla

            availableIndices.Remove(randomIndex); // Kullan�lan spawn alan�n� listeden ��kar
        }
    }

    private GemType GetRandomGemType()
    {
        int randomIndex = Random.Range(0, gemTypeManager.gemTypes.Count); // Gem t�rlerinin say�s� aras�nda rastgele bir indeks al
        return gemTypeManager.gemTypes[randomIndex]; // Rastgele se�ilen Gem t�r�n� d�nd�r
    }

    private int GetRandomSpawnIndex(List<int> availableIndices)
    {
        int randomIndex = Random.Range(0, availableIndices.Count); // Kullan�labilir spawn alanlar� listesinin boyutu aras�nda rastgele bir indeks al
        return availableIndices[randomIndex]; // Rastgele se�ilen spawn alan� indeksini d�nd�r
    }

    public bool IsGridEmpty(Transform gridTransform)
    {
        Collider[] colliders = Physics.OverlapBox(gridTransform.position, Vector3.one * 0.5f); // Grid alan�nda bulunan collider'lar� al

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
            if (IsGridEmpty(spawnArea[i])) // E�er spawn alan� bo� ise
            {
                GemType randomGemType = GetRandomGemType(); // Rastgele bir Gem t�r� al
                GameObject gemObject = Instantiate(randomGemType.model, spawnArea[i].position, Quaternion.identity); // Gem objesini spawn alan�na instantiate et
                gemObject.tag = randomGemType.gemName; // Gem objesinin etiketini Gem t�r�ne g�re ayarla
            }
        }

        StartCoroutine(RespawnGemsCoroutine()); // Yeniden spawnlama i�lemini tekrarla
    }

    private void StartRespawnGems()
    {
        StartCoroutine(RespawnGemsCoroutine()); // Yeniden spawnlama coroutine'ini ba�lat
    }
}
