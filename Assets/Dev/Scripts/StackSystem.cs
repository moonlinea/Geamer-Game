using UnityEngine;
using DG.Tweening;

public class StackSystem : MonoBehaviour
{
    private GameObject targetObjects; // Target objesini Inspector üzerinden atayýn
    public float stackHeight = 5f; // Stack yüksekliði
    private Transform gemTransform;
    private Vector3 _offset;
    private Transform gemPos;
   

    bool b = false;

    public void Start()
    {
        gemTransform = this.transform;
        targetObjects = GameObject.FindWithTag("Target");

    }
    public void Update()
    {

       
        gemPos = targetObjects.transform;
        if (b == true)
        {
            Follow();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))

        {
            b = true;
           

        }
        else if (other.CompareTag("Jeweler"))
        {
           
            targetObjects = GameObject.FindWithTag("Jeweler");
        }
    }
    private void Follow()
    {
        gemTransform.DOMoveX(gemPos.position.x + _offset.x, 5 * Time.deltaTime);
        gemTransform.DOMoveZ(gemPos.position.z + _offset.z, 5 * Time.deltaTime);
        gemTransform.DOMoveY(gemPos.position.y + _offset.y, 5 * Time.deltaTime);

    }
 

}
