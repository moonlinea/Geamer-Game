using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserNameController : MonoBehaviour
{
    public GameObject userName;
    // Start is called before the first frame update
    void Start()
    {
        userName.GetComponent<UnityEngine.UI.Text>().text = "...";
    }

    // Update is called once per frame
    void UpdateMessage(string text)
    {
        userName.GetComponent<UnityEngine.UI.Text>().text = text;
    }
}
