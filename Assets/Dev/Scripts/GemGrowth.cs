using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGrowth : MonoBehaviour
{
    private Collider objectCollider;
    private float scale = 0;

    private void Start()
    {
        objectCollider = GetComponent<Collider>();
        objectCollider.enabled = false;
        StartCoroutine(GrowGem());
    }

    private IEnumerator GrowGem()
    {
        while (scale < 1)
        {
            yield return new WaitForSeconds(5);
            scale += 1;
            Debug.Log("Gem size increased: " + scale);

            if (scale >= 0.25f)
            {
                objectCollider.enabled = false;
                Debug.Log("Gem can be collected now!");
            }
        }
    }
}
