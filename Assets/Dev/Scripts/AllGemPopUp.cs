using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AllGemPopUp : MonoBehaviour
{
    public GameObject popUpPanel;
    public GameObject textPrefab;
    public Image iconGem;
    public Transform container;
    public int numberOfTextObjects;

   

    public GemTypeManager GTM;

    public PlayerController PC;
    int gemCount;

    bool BtnControl = true;
    private void Start()
    {
        popUpPanel.SetActive(false);
        numberOfTextObjects = GTM.gemTypes.Count;
        Debug.Log(numberOfTextObjects);
       
    }

    public void OpenPopUp()
    {
        if (BtnControl)
        {
            Time.timeScale = 0f;
            popUpPanel.SetActive(true);
            CreateTexts();

            BtnControl = false;
        }
        else ClosePopUp();



    }

    public void ClosePopUp()
    {
        Time.timeScale = 1f;
        int childCount = container.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);
        }
        popUpPanel.SetActive(false);
        BtnControl = true;
    }

    private void CreateTexts()
    {


        // Ýlk çocuk olan boþ text objesini kaldýrýn
        if (container.childCount > 0)
        {
            Destroy(container.GetChild(0).gameObject);
        }


        for (int i = 0; i < numberOfTextObjects && i < GTM.gemTypes.Count; i++)
        {

            GemType gemType = GTM.gemTypes[i];
            GameObject newTextObject = Instantiate(textPrefab, container); // GemInfoContainer'ýn altýna oluþturuluyor
            iconGem = newTextObject.GetComponentInChildren<Image>();
            int gemCount = PlayerController.GetGemCount(gemType.gemName); 
            newTextObject.GetComponent<TextMeshProUGUI>().text = (gemType.gemName + "-->" + gemType.startingPrice + "-->" + gemCount);
            iconGem.sprite = gemType.icon;
            string GemName = gemType.gemName;
           

        }
    }
}