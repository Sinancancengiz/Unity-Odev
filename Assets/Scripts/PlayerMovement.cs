using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    // Orijinal yer çekimi deðeri üzerinden ayarlama da yapýlabilir, direkt deðer de verilebilir.
    // Negatif olmasýnýn sebebi dünyanýn 0, 0, 0 noktasýna kurulu olmasý. Aþaðýsý negatif Y olarak kalýyor.
    public float gravity = -9.8f * 2;
    public float jumpHeight = 3f;

    // Havada zýplamayý önlemek için yerde miyiz, kontrol etmeliyiz.
    public Transform groundCheck;
    // Hangi mesafeye kadar zemin kabul edileceði
    public float groundDistance = 0.4f;
    // Hangi katmanlarýn zemin olarak kabul edileceðini belirlemek için
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    private void Update()
    {
        // Küremsi þekilde bir alan oluþturup içine giren alaný kontrol eden bir metod
        // Aldýðý parametreleri deðiþkenler içinde oluþturduk.
        // Posizyon, menzil, algýlamasý gereken nesne parametrelerini alýyor.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Eðer oyuncu yerdeyse ve hýzý (düþüþ) sýfýrýn altýndaysa, hýzýný resetliyoruz.
        // Sebebi; düþüþü simüle edemiyoruz. Dur denildiði için durduðu için nesne durdu, yere çarptýðý için deðil.
        // Yere çarpma etkilerinden "yalnýzca" görebildiklerimizi veriyoruz. Ývmesini sýfýrlýyoruz.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;   // Negatif ve yukarý çýkmadýðýndan emin olmak için -2 verdik. Özel bir nedeni yok.

        }

        // Input mapte tanýmlý özel girdiler.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
