using System.Collections.Generic;
using UnityEngine;

public class GemTypeManager : MonoBehaviour
{
    public List<GemType> gemTypes;

    private void Start()
    {
        AssignTagsToGems();
    }

    private void AssignTagsToGems()
    {
        foreach (GemType gemType in gemTypes)
        {
            GameObject gemObject = gemType.model;
            gemObject.tag = gemType.gemName;
        }
    }
}

[System.Serializable]
public class GemType
{
    public string gemName;
    public float startingPrice;
    public Sprite icon;
    public GameObject model;
}



//using System.Collections.Generic;
//using UnityEngine;

//public class GemTypeManager : MonoBehaviour
//{
//    public List<GemType> gemTypes;

//    private void Start()
//    {
//        AssignTagsToGems();
//    }

//    private void AssignTagsToGems()
//    {
//        foreach (GemType gemType in gemTypes)
//        {
//            GameObject gemObject = gemType.model;
//            gemObject.tag = gemType.gemName;

//            // E�er alt objeler varsa, onlar�n da tag'ini de�i�tir
//            //var childObjects = gemObject.GetComponentsInChildren<Transform>();
//            //foreach (var child in childObjects)
//            //{
//            //    child.gameObject.tag = gemType.gemName;
//            //}
//        }
//    }
//}
//[System.Serializable]
//public class GemType
//{
//    public string gemName;
//    public float startingPrice;
//    public Sprite icon;
//    public GameObject model;
//}