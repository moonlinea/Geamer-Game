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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("Gem_"))
        {
            GemType gemType = GetGemTypeByTag(other.tag);
            if (gemType != null)
            {
                float startingPrice = gemType.startingPrice;
                TotalGold += startingPrice;

                Debug.Log("Total Gold=" + TotalGold);
                Debug.Log(other.tag+" Gem Starting Price: " + startingPrice);
            }

            StartCoroutine(DestroyAfterDelay(other.gameObject, 3f)); // 3 saniye sonra nesneyi yok etmek için IEnumerator'u baþlat
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // Belirtilen süre kadar bekle

        Destroy(obj); // Nesneyi yok et
    }


    private GemType GetGemTypeByTag(string tag)
    {
        foreach (GemType gemType in gemTypeManager.gemTypes)
        {
            if (gemType.gemName == tag)
            {
                return gemType;
            }
        }
        return null;
    }
}
