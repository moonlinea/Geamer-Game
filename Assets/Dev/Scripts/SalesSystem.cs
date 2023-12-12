using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SalesSystem : MonoBehaviour
{
    private GemTypeManager gemTypeManager;
    private float TotalGold;
    public TMP_Text _totalGoldText;
    bool firstGold = true;

    private void Start()
    {

        gemTypeManager = FindObjectOfType<GemTypeManager>();

        if (firstGold = false)
        {
            _totalGoldText.text = "" + PlayerPrefs.GetFloat("TotalGold", TotalGold);


        }
        else
        {
            firstGold = false;
            PlayerPrefs.SetFloat("TotalGold", 0);
        }
        // Ba�lang��ta kaydedilen toplam alt�n miktar�n� al�p _totalGoldText'e at�yoruz
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
                _totalGoldText.text = "" + TotalGold;
                PlayerPrefs.SetFloat("TotalGold", TotalGold);
                Debug.Log(other.tag + " Gem Starting Price: " + startingPrice);
            }

            StartCoroutine(DestroyAfterDelay(other.gameObject, 3f)); // 3 saniye sonra nesneyi yok etmek i�in IEnumerator'u ba�lat
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // Belirtilen s�re kadar bekle

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
