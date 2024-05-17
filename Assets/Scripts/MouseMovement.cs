using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensivity = 200f;

    // X ve Y etrafýnda döndürerek etrafa bakmayý saðlayacaðýz.
    float xRotation = 0f;
    float yRotation = 0f;

    private void Start()
    {
        // Mouse merkezde ve görünmez baþlýyor.
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Mouse X ve Y input mapta özel isimler. Bunlarýn deðerlerini, hýz ve zamanla çarparak konum bilgisini alýyoruz.
        // Mouse X ve Y inputlarý kendi baþlarýna da hareket saðlarlar ama çok yavaþ olacaktýr.
        // Fps ile ayný olmasý, tutarlýlýk saðlamasý için frameler arasý geçen zamanla çarpýyoruz.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        // Hareket yönetiminden kaynaklý düzenlemeler:
        // X eksenine göre eðim düþey rostasyonu saðlýyor...
        xRotation -= mouseY;
        // Sonsuz esneklik istemiyoruz.
        //xRotation sýnýrlarý belirliyoruz, böylece karakterimiz 90 derece yukarý veya aþaðý bakabilir.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // yRotation için ayný þeyi yapmak istemeyiz.

        yRotation += mouseX;

        // Player objesinin içindeki transformu otomatik olarak alýyor.
        // Transform bileþenin rotasyonunu güncelliyoruz.
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
