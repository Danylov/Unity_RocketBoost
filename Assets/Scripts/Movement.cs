using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 1000.0f;
    [SerializeField] private float rotationThrust = 100.0f;
    [SerializeField] private AudioClip mainEngine;
    private Rigidbody rb;
    private AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)  audioSource.PlayOneShot(mainEngine);
        }
        else audioSource.Stop();
    } 
    void ProcessRotation()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}