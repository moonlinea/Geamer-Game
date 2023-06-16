using UnityEngine;

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
        else if (other.CompareTag("Sales"))
        {
            targetObjects = GameObject.FindWithTag("Jeweler");
        }
    }

    private void Follow()
    {
        float speed = 5f;
        float step = speed * Time.deltaTime;

        Vector3 targetPosition = gemPos.position + _offset;
        gemTransform.position = Vector3.MoveTowards(gemTransform.position, targetPosition, step);
    }
}
