using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int width; // Grid geniþliði
    public int height; // Grid yüksekliði
    public float cellSize; // Hücre boyutu
    public Vector3 originPosition=new Vector3(-6.03f,0f,0f); // Baþlangýç pozisyonu

    private GameObject[,] grid; // Grid hücrelerini tutan 2D dizi

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 spawnPosition = GetCellPosition(x, y);
                GameObject cellObject = new GameObject("Cell (" + x + ", " + y + ")");
                cellObject.transform.position = spawnPosition;
                grid[x, y] = cellObject;
            }
        }
    }

    private Vector3 GetCellPosition(int x, int y)
    {
        Vector3 positionOffset = new Vector3(cellSize * 0.5f, 0f, cellSize * 0.5f);
        Vector3 spawnPosition = originPosition + new Vector3(x * cellSize, 0f, y * cellSize) + positionOffset;
        return spawnPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (grid != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 cellPosition = GetCellPosition(x, y);
                    Gizmos.DrawWireCube(cellPosition, new Vector3(cellSize, 0f, cellSize));
                }
            }
        }
    }
}
