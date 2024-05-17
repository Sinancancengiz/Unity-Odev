using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensivity = 200f;

    // X ve Y etraf�nda d�nd�rerek etrafa bakmay� sa�layaca��z.
    float xRotation = 0f;
    float yRotation = 0f;

    private void Start()
    {
        // Mouse merkezde ve g�r�nmez ba�l�yor.
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Mouse X ve Y input mapta �zel isimler. Bunlar�n de�erlerini, h�z ve zamanla �arparak konum bilgisini al�yoruz.
        // Mouse X ve Y inputlar� kendi ba�lar�na da hareket sa�larlar ama �ok yava� olacakt�r.
        // Fps ile ayn� olmas�, tutarl�l�k sa�lamas� i�in frameler aras� ge�en zamanla �arp�yoruz.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        // Hareket y�netiminden kaynakl� d�zenlemeler:
        // X eksenine g�re e�im d��ey rostasyonu sa�l�yor...
        xRotation -= mouseY;
        // Sonsuz esneklik istemiyoruz.
        //xRotation s�n�rlar� belirliyoruz, b�ylece karakterimiz 90 derece yukar� veya a�a�� bakabilir.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // yRotation i�in ayn� �eyi yapmak istemeyiz.

        yRotation += mouseX;

        // Player objesinin i�indeki transformu otomatik olarak al�yor.
        // Transform bile�enin rotasyonunu g�ncelliyoruz.
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
