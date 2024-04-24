using Unity.Netcode;
using UnityEngine;

public class StackSystem : MonoBehaviour
{
    private GameObject targetObjects;
    public float stackHeight = 5f;
    private Transform gemTransform;
    private Vector3 _offset;
    private Transform gemPos;
   
    bool b = false;
    private bool alreadyFoundTarget = false;

    private void Start()
    {
       
        gemTransform = this.transform;
    }

    public void Update()
    {
        if (alreadyFoundTarget)
        {
            // Hedef objeyi her frame'de güncelle
            gemPos = targetObjects.transform;
            if (b == true)
            {
                Follow();
            }
        }
        else
        {
            // Hedef objeyi ara
            if (GameObject.FindWithTag("Target"))
            {
                targetObjects = GameObject.FindWithTag("Target");
                alreadyFoundTarget = true; // Hedef obje bulunduğunda bayrağı true yap
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            b = true;
        }
        else if (other.CompareTag("Sales"))
        {
            targetObjects = GameObject.FindWithTag("Jeweler");
        }
    }

    private void Follow()
    {
        float speed = 5f;
        float step = speed * Time.deltaTime;

        Vector3 targetPosition = gemPos.position + _offset; // Takip edilecek hedef pozisyonu, offset ile ayarlanýr
        gemTransform.position = Vector3.MoveTowards(gemTransform.position, targetPosition, step); // Hedefe doðru hareket et
    }
}
