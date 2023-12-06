using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int rowCount;
    public int columnCount;
    public GameObject tilePrefab;

    public Transform[] spawnArea; // Dizi tanýmlanýyor

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector3 currentPosition = transform.position; // Baþlangýç pozisyonu alýnýyor

        Vector3 cellSize = tilePrefab.GetComponent<Renderer>().bounds.size; // Kare objesinin boyutu alýnýyor

        spawnArea = new Transform[rowCount * columnCount]; // Doðum alaný dizisi boyutu rowCount * columnCount olarak ayarlanýyor

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                Vector3 position = currentPosition + new Vector3(column * cellSize.x, 0, row * cellSize.z); // Yeni kare pozisyonu hesaplanýyor
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform); // Kare objesi oluþturuluyor
                tile.name = $"Tile ({row}, {column})"; // Kare objesinin adý belirleniyor

                spawnArea[row * columnCount + column] = tile.transform; // Kare objesinin Transform bilgisi doðum alaný dizisine ekleniyor
            }
        }
    }
}
