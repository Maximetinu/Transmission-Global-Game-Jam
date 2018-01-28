using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathe : MonoBehaviour
{

    [Range(0.0f, 10.0f)] [SerializeField] float breatheSpeed = 2.0f; // Speed of the breathing cycle
    [Range(0.0f, 50.0f)] [SerializeField] float breatheMagnitude = 0.8f; // Magnitude of breathing

    private float sine;


    void Update()
    {
        // Update Breathing
        sine += Time.deltaTime * breatheSpeed;
        if (sine >= Mathf.PI * 2f) sine -= Mathf.PI * 2f;
        float br = Mathf.Sin(sine) * breatheMagnitude * 0.01f; // Si se reescala, debe actualizarse con la escala // 0.01f Factor arbitrario temporal

        // Apply breathing
        Vector3 breatheOffset = Vector3.up * br;
        transform.position = transform.position + breatheOffset;
    }
}