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

    private static Dictionary<string, int> gemCounts; // Sayaçlarý tutmak için sözlük

    private void Start()
    {
        GTM = FindObjectOfType<GemTypeManager>(); 
        InitializeGemCounts(); 
        LoadGemCounts();
    }

    private void InitializeGemCounts()
    {
        gemCounts = new Dictionary<string, int>(); // Yeni bir sayaç sözlüðü oluþtur

        foreach (GemType gemType in GTM.gemTypes)
        {
            string gemName = gemType.gemName; // Gem'in adý
            int count = PlayerPrefs.GetInt(gemName, 0); // Gem sayacýný PlayerPrefs'ten yükle, yoksa 0 olarak ayarla
            gemCounts.Add(gemName, count); // Yüklenen sayacý sözlüðe ekle
        }
    }

    private void FixedUpdate()
    {
        // Joystick ile karakter kontrolü
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _playerSpeed, _rigidbody.velocity.y, _joystick.Vertical * _playerSpeed); // Hareket vektörünü güncelle

            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity); // Karakterin hareket yönüne dönmesini saðla

            _Anim.SetBool("walking", true); // Walking animasyonunu baþlat
        }
        else
        {
            _Anim.SetBool("walking", false); // Walking animasyonunu durdur
        }
    }

    private void OnTriggerEnter(Collider GamCol)
    {
        // Gem'e temas edildiðinde
        if (GamCol.tag.StartsWith("Gem_"))
        {
            string gemTag = GamCol.tag; // Gem'in etiketi

            if (gemCounts.ContainsKey(gemTag))
            {
                gemCounts[gemTag]++; // Ýlgili sayacý bir artýr
                Debug.Log("Gem Count for " + gemTag + ": " + gemCounts[gemTag]);
                PlayerPrefs.SetInt(gemTag, gemCounts[gemTag]); // PlayerPrefs'e güncellenen sayacý kaydet
            }
        }
    }

    public static int GetGemCount(string gemTag)
    {
        // Belirtilen gemTag'e sahip sayaç var mý?
        if (gemCounts.ContainsKey(gemTag))
        {
            return gemCounts[gemTag]; // Sayaç deðerini döndür
        }

        return 0;
    }

    private void LoadGemCounts()
    {
        // PlayerPrefs'ten kaydedilen sayaçlarý yükle
        foreach (GemType gemType in GTM.gemTypes)
        {
            string gemName = gemType.gemName; 
            int count = PlayerPrefs.GetInt(gemName, 0); 
            gemCounts[gemName] = count; 
        }
    }
}
