using UnityEngine;    
using UnityEngine.UI;    
using TMPro;    

public class AllGemPopUp : MonoBehaviour
{
    public GameObject popUpPanel;    // Pop-up panel nesnesi
    public GameObject textPrefab;    // Metin �rne�i nesnesi
    public Image iconGem;    // �kon g�r�nt�s�
    public Transform container;    // Metinlerin yerle�tirilece�i konteyner
    public int numberOfTextObjects;    // Metin nesnesi say�s�

    public GemTypeManager GTM;    
    public PlayerController PC;   
    int gemCount;    // Gem say�s�

    bool BtnControl = true;

    private void Start()
    {
        popUpPanel.SetActive(false);  

        numberOfTextObjects = GTM.gemTypes.Count;    // Metin nesnesi say�s�n� GemTypeManager'dan al�yoruz

        Debug.Log(numberOfTextObjects);
    }

    public void OpenPopUp()
    {
        if (BtnControl)
        {
            Time.timeScale = 0f;    // Oyun zaman�n� durdur
            popUpPanel.SetActive(true);    // Pop-up paneli aktif hale getir
            CreateTexts();    // Metinleri olu�tur

            BtnControl = false;
        }
        else
        {
            ClosePopUp();
        }
    }

    public void ClosePopUp()
    {
        Time.timeScale = 1f;    // Oyun zaman�n� tekrar ba�lat

        int childCount = container.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);    // Konteynerdeki t�m metin nesnelerini yok et
        }

        popUpPanel.SetActive(false);    // Pop-up paneli devre d��� b�rak
        BtnControl = true;
    }

    private void CreateTexts()
    {
        // �lk �ocuk olan bo� metin nesnesini kald�r
        if (container.childCount > 0)
        {
            Destroy(container.GetChild(0).gameObject);
        }

        for (int i = 0; i < numberOfTextObjects && i < GTM.gemTypes.Count; i++)
        {
            GemType gemType = GTM.gemTypes[i];    // GemType'� al
            GameObject newTextObject = Instantiate(textPrefab, container);  
            iconGem = newTextObject.GetComponentInChildren<Image>();    
            int gemCount = PlayerController.GetGemCount(gemType.gemName); 
            newTextObject.GetComponent<TextMeshProUGUI>().text = (gemType.gemName + "-->" + gemType.startingPrice + "-->" + gemCount);   
            iconGem.sprite = gemType.icon;   
            string GemName = gemType.gemName;
        }
    }
}
