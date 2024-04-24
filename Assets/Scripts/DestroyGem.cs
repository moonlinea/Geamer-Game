using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGem : MonoBehaviour
{
    private GemTypeManager gemTypeManager;
    private float TotalGold;

    private void Start()
    {
        gemTypeManager = FindObjectOfType<GemTypeManager>();    
    }

    private void OnTriggerEnter(Collider other)//Sat�� yerine geldi�inde s�reye g�re yok etme ve sat�lan gemlerin gold kar��l���n� hesaplama
    {
        if (other.tag.StartsWith("Gem_"))   
        {
            GemType gemType = GetGemTypeByTag(other.tag);   
            if (gemType != null) 
            {
                float startingPrice = gemType.startingPrice;   
                TotalGold += startingPrice;   

                Debug.Log("Total Gold=" + TotalGold);
                Debug.Log(other.tag + " Gem Starting Price: " + startingPrice);
            }

            StartCoroutine(DestroyAfterDelay(other.gameObject, 3f));    // 3 saniye sonra nesneyi yok etmek i�in IEnumerator'u ba�lat
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);   

        Destroy(obj);  
    }

    private GemType GetGemTypeByTag(string tag)
    {
        foreach (GemType gemType in gemTypeManager.gemTypes)    // GemTypeManager'daki gemTypes listesinde gezin
        {
            if (gemType.gemName == tag)    // E�er GemType'�n gemName �zelli�i etikete e�itse
            {
                return gemType;    // GemType'� d�nd�r
            }
        }
        return null;    // GemType bulunamazsa null d�nd�r
    }
}
