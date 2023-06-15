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
        Vector3 currentPosition = transform.position;

        Vector3 cellSize = tilePrefab.GetComponent<Renderer>().bounds.size;

        spawnArea = new Transform[rowCount * columnCount]; // Dizi boyutu rowCount * columnCount olarak ayarlanýyor

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                Vector3 position = currentPosition + new Vector3(column * cellSize.x, 0, row * cellSize.z);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                tile.name = $"Tile ({row}, {column})";

                spawnArea[row * columnCount + column] = tile.transform; // Transform bilgisi spawnArea dizisine ekleniyor
            }
        }
    }
}
