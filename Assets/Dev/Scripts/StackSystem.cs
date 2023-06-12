using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSystem : MonoBehaviour
{
    private Stack<GameObject> gemStack = new Stack<GameObject>();

    public void AddGem(GameObject gem)
    {
        gemStack.Push(gem);
        gem.transform.SetParent(transform); // Gemi oyuncunun �zerindeki stack'e eklenir
        gem.transform.localPosition = Vector3.zero; // Gemi oyuncunun �zerindeki s�f�r noktas�na konumland�r�l�r
    }

    public void RemoveGem()
    {
        if (gemStack.Count > 0)
        {
            GameObject gem = gemStack.Pop();
            Destroy(gem);
        }
    }

    public int GetGemCount()
    {
        return gemStack.Count;
    }

    public void ClearStack()
    {
        foreach (GameObject gem in gemStack)
        {
            Destroy(gem);
        }
        gemStack.Clear();
    }
    

}
