using UnityEngine;    
using UnityEngine.UI;    
using TMPro;    

public class AllGemPopUp : MonoBehaviour
{
    public GameObject popUpPanel;    // Pop-up panel nesnesi
    public GameObject textPrefab;    // Metin örneði nesnesi
    public Image iconGem;    // Ýkon görüntüsü
    public Transform container;    // Metinlerin yerleþtirileceði konteyner
    public int numberOfTextObjects;    // Metin nesnesi sayýsý

    public GemTypeManager GTM;    
    public PlayerController PC;   
    int gemCount;    // Gem sayýsý

    bool BtnControl = true;

    private void Start()
    {
        popUpPanel.SetActive(false);  

        numberOfTextObjects = GTM.gemTypes.Count;    // Metin nesnesi sayýsýný GemTypeManager'dan alýyoruz

        Debug.Log(numberOfTextObjects);
    }

    public void OpenPopUp()
    {
        if (BtnControl)
        {
            Time.timeScale = 0f;    // Oyun zamanýný durdur
            popUpPanel.SetActive(true);    // Pop-up paneli aktif hale getir
            CreateTexts();    // Metinleri oluþtur

            BtnControl = false;
        }
        else
        {
            ClosePopUp();
        }
    }

    public void ClosePopUp()
    {
        Time.timeScale = 1f;    // Oyun zamanýný tekrar baþlat

        int childCount = container.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);    // Konteynerdeki tüm metin nesnelerini yok et
        }

        popUpPanel.SetActive(false);    // Pop-up paneli devre dýþý býrak
        BtnControl = true;
    }

    private void CreateTexts()
    {
        // Ýlk çocuk olan boþ metin nesnesini kaldýr
        if (container.childCount > 0)
        {
            Destroy(container.GetChild(0).gameObject);
        }

        for (int i = 0; i < numberOfTextObjects && i < GTM.gemTypes.Count; i++)
        {
            GemType gemType = GTM.gemTypes[i];    // GemType'ý al
            GameObject newTextObject = Instantiate(textPrefab, container);  
            iconGem = newTextObject.GetComponentInChildren<Image>();    
            int gemCount = PlayerController.GetGemCount(gemType.gemName); 
            newTextObject.GetComponent<TextMeshProUGUI>().text = (gemType.gemName + "-->" + gemType.startingPrice + "-->" + gemCount);   
            iconGem.sprite = gemType.icon;   
            string GemName = gemType.gemName;
        }
    }
}
