using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int rowCount;
    public int columnCount;
    public GameObject tilePrefab;

    public Transform[] spawnArea; // Dizi tan�mlan�yor

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector3 currentPosition = transform.position; // Ba�lang�� pozisyonu al�n�yor

        Vector3 cellSize = tilePrefab.GetComponent<Renderer>().bounds.size; // Kare objesinin boyutu al�n�yor

        spawnArea = new Transform[rowCount * columnCount]; // Do�um alan� dizisi boyutu rowCount * columnCount olarak ayarlan�yor

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                Vector3 position = currentPosition + new Vector3(column * cellSize.x, 0, row * cellSize.z); // Yeni kare pozisyonu hesaplan�yor
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform); // Kare objesi olu�turuluyor
                tile.name = $"Tile ({row}, {column})"; // Kare objesinin ad� belirleniyor

                spawnArea[row * columnCount + column] = tile.transform; // Kare objesinin Transform bilgisi do�um alan� dizisine ekleniyor
            }
        }
    }
}
