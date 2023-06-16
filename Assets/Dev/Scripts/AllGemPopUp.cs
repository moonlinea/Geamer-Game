using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AllGemPopUp : MonoBehaviour
{
    public GameObject popUpPanel;
    public GemTypeManager GTM;
    public GameObject textPrefab;
    public Transform container;
    public int numberOfTextObjects;
    public Image BG;

    private void Start()
    {
        numberOfTextObjects = GTM.gemTypes.Count;
        Debug.Log(numberOfTextObjects);
       
    }

    public void OpenPopUp()
    {
        
        popUpPanel.SetActive(true);
        BG.enabled = true;
        CreateTexts();
    }

    public void ClosePopUp()
    {
        BG.enabled = false;
        popUpPanel.SetActive(false);
    }

    private void CreateTexts()
    {
        

        // Ýlk çocuk olan boþ text objesini kaldýrýn
        if (container.childCount > 0)
        {
            Destroy(container.GetChild(0).gameObject);
        }

        for (int i = 0; i < numberOfTextObjects; i++)
        {
            GameObject newTextObject = Instantiate(textPrefab, container); // GemInfoContainer'ýn altýna oluþturuluyor
            newTextObject.GetComponent<TextMeshProUGUI>().text = "Text " + (i + 1);



        }
    }
}
//foreach (GemType gemType in GTM.gemTypes)
//{
//    float startingPrice = gemType.startingPrice;
//    Debug.Log("Gem Name: " + gemType.gemName);
//    Debug.Log("Starting Price: " + startingPrice);
//}