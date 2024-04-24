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

    private void OnTriggerEnter(Collider other)//Satýþ yerine geldiðinde süreye göre yok etme ve satýlan gemlerin gold karþýlýðýný hesaplama
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

            StartCoroutine(DestroyAfterDelay(other.gameObject, 3f));    // 3 saniye sonra nesneyi yok etmek için IEnumerator'u baþlat
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
            if (gemType.gemName == tag)    // Eðer GemType'ýn gemName özelliði etikete eþitse
            {
                return gemType;    // GemType'ý döndür
            }
        }
        return null;    // GemType bulunamazsa null döndür
    }
}
