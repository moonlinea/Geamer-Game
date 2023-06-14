using UnityEngine;
using DG.Tweening;

public class StackSystem : MonoBehaviour
{
    private GameObject targetObjects; // Target objesini Inspector �zerinden atay�n
    public float stackHeight = 5f; // Stack y�ksekli�i
    private Transform gemTransform;
    private Vector3 _offset;
    private Transform gemPos;

    public void Start()
    {
        gemTransform = this.transform;
    }
    public void Update()
    {
        targetObjects = GameObject.FindWithTag("Target");

        gemPos = targetObjects.transform;

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            Debug.Log("Stack");
            Follow();
           
        }
    }
    private void Follow()
    {
        
        gemTransform.DOMoveX(gemPos.position.x + _offset.x, 5 * Time.deltaTime);
        gemTransform.DOMoveZ(gemPos.position.z + _offset.z, 5 * Time.deltaTime);
        gemTransform.DOMoveY(gemPos.position.y+_offset.y,5*Time.deltaTime);
    }

}
//public class StackSystem : MonoBehaviour
//{
//    private GameObject targetObjects; // Target objesini Inspector �zerinden atay�n
//    public float stackHeight = 1f; // Stack y�ksekli�i
//    private Transform gemTransform;
//    private Vector3 _offset;
//    private Transform gemPos;

//    public void Start()
//    {
//        gemTransform = this.transform;
//        _offset = new Vector3(0f, stackHeight, 0f);
//    }

//    public void Update()
//    {
//        targetObjects = GameObject.FindWithTag("Target");
//        gemPos = targetObjects.transform;
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("Stack");
//            Follow();
//            gemTransform.position += _offset;
//        }
//    }

//    private void Follow()
//    {
//        gemTransform.DOMoveX(gemPos.position.x + _offset.x, 5 * Time.deltaTime);
//        gemTransform.DOMoveZ(gemPos.position.z + _offset.z, 5 * Time.deltaTime);
//    }
//}

