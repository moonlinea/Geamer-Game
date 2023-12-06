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
        LoadGemCounts();
    }

    private void InitializeGemCounts()
    {
        gemCounts = new Dictionary<string, int>(); // Yeni bir saya� s�zl��� olu�tur

        foreach (GemType gemType in GTM.gemTypes)
        {
            string gemName = gemType.gemName; // Gem'in ad�
            int count = PlayerPrefs.GetInt(gemName, 0); // Gem sayac�n� PlayerPrefs'ten y�kle, yoksa 0 olarak ayarla
            gemCounts.Add(gemName, count); // Y�klenen sayac� s�zl��e ekle
        }
    }

    private void FixedUpdate()
    {
        // Joystick ile karakter kontrol�
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _playerSpeed, _rigidbody.velocity.y, _joystick.Vertical * _playerSpeed); // Hareket vekt�r�n� g�ncelle

            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity); // Karakterin hareket y�n�ne d�nmesini sa�la

            _Anim.SetBool("walking", true); // Walking animasyonunu ba�lat
        }
        else
        {
            _Anim.SetBool("walking", false); // Walking animasyonunu durdur
        }
    }

    private void OnTriggerEnter(Collider GamCol)
    {
        // Gem'e temas edildi�inde
        if (GamCol.tag.StartsWith("Gem_"))
        {
            string gemTag = GamCol.tag; // Gem'in etiketi

            if (gemCounts.ContainsKey(gemTag))
            {
                gemCounts[gemTag]++; // �lgili sayac� bir art�r
                Debug.Log("Gem Count for " + gemTag + ": " + gemCounts[gemTag]);
                PlayerPrefs.SetInt(gemTag, gemCounts[gemTag]); // PlayerPrefs'e g�ncellenen sayac� kaydet
            }
        }
    }

    public static int GetGemCount(string gemTag)
    {
        // Belirtilen gemTag'e sahip saya� var m�?
        if (gemCounts.ContainsKey(gemTag))
        {
            return gemCounts[gemTag]; // Saya� de�erini d�nd�r
        }

        return 0;
    }

    private void LoadGemCounts()
    {
        // PlayerPrefs'ten kaydedilen saya�lar� y�kle
        foreach (GemType gemType in GTM.gemTypes)
        {
            string gemName = gemType.gemName; 
            int count = PlayerPrefs.GetInt(gemName, 0); 
            gemCounts[gemName] = count; 
        }
    }
}
