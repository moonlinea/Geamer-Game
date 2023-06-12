using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    private static GridGenerator instance;

    public static GridGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GridGenerator>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GridGenerator>();
                    singletonObject.name = typeof(GridGenerator).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }
}
