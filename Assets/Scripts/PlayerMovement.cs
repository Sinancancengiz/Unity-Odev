using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    // Orijinal yer �ekimi de�eri �zerinden ayarlama da yap�labilir, direkt de�er de verilebilir.
    // Negatif olmas�n�n sebebi d�nyan�n 0, 0, 0 noktas�na kurulu olmas�. A�a��s� negatif Y olarak kal�yor.
    public float gravity = -9.8f * 2;
    public float jumpHeight = 3f;

    // Havada z�plamay� �nlemek i�in yerde miyiz, kontrol etmeliyiz.
    public Transform groundCheck;
    // Hangi mesafeye kadar zemin kabul edilece�i
    public float groundDistance = 0.4f;
    // Hangi katmanlar�n zemin olarak kabul edilece�ini belirlemek i�in
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    private void Update()
    {
        // K�remsi �ekilde bir alan olu�turup i�ine giren alan� kontrol eden bir metod
        // Ald��� parametreleri de�i�kenler i�inde olu�turduk.
        // Posizyon, menzil, alg�lamas� gereken nesne parametrelerini al�yor.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // E�er oyuncu yerdeyse ve h�z� (d����) s�f�r�n alt�ndaysa, h�z�n� resetliyoruz.
        // Sebebi; d����� sim�le edemiyoruz. Dur denildi�i i�in durdu�u i�in nesne durdu, yere �arpt��� i�in de�il.
        // Yere �arpma etkilerinden "yaln�zca" g�rebildiklerimizi veriyoruz. �vmesini s�f�rl�yoruz.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;   // Negatif ve yukar� ��kmad���ndan emin olmak i�in -2 verdik. �zel bir nedeni yok.

        }

        // Input mapte tan�ml� �zel girdiler.
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
