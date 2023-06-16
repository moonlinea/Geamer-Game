using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _Anim;

    [SerializeField] private float _playerSpeed;
    private GemTypeManager GTM;
    private static Dictionary<string, int> gemCounts; // Saya�lar� tutmak i�in s�zl�k

    private void Start()
    {
        GTM = FindObjectOfType<GemTypeManager>();
        InitializeGemCounts();
       

    }
    private void InitializeGemCounts()
    {
        gemCounts = new Dictionary<string, int>();

        foreach (GemType gemType in GTM.gemTypes)
        {
            gemCounts.Add(gemType.gemName, 0); // Her bir gem t�r� i�in ba�lang��ta 0 sayac� ayarla
        }
    }

    private void FixedUpdate()// Character Positions and Rotations Controller With Joystick
    {


        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _playerSpeed, _rigidbody.velocity.y, _joystick.Vertical * _playerSpeed);
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _Anim.SetBool("walking", true);
        }
        else _Anim.SetBool("walking", false);

    }
    private void OnTriggerEnter(Collider GamCol)
    {
        if (GamCol.tag.StartsWith("Gem_"))
        {
            string gemTag = GamCol.tag; // Gem'in etiketi
           
            if (gemCounts.ContainsKey(gemTag))
            {
                gemCounts[gemTag]++; // �lgili sayac� bir art�r
                Debug.Log("Gem Count for " + gemTag + ": " + gemCounts[gemTag]);
            }
        }
    }

    public static int GetGemCount(string gemTag)
    {
        if (gemCounts.ContainsKey(gemTag))
        {
            return gemCounts[gemTag];
        }

        return 0; // E�er belirtilen gemTag'e sahip bir saya� yoksa 0 de�erini d�nd�r
    }


}



