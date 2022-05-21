using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PARAMETERS - for tuning, typically set in the editor
    //CACHE - e.g.references or readability or speed
    //STATE - private instance (member) variables

    [SerializeField] float mainThrust = 500f;
    [SerializeField] float rotationThrust = 50f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem MainBooster;
    [SerializeField] ParticleSystem LeftBooster;
    [SerializeField] ParticleSystem RightBooster;

    Rigidbody rb;
    AudioSource audioSource;

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
        if (Input.GetKey(KeyCode.Space)) StartThrusting();
        else StopThrusting();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))  RotateLeft();
        else if (Input.GetKey(KeyCode.D)) RotateRight();
        else StopRotating();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
        if (!MainBooster.isPlaying) MainBooster.Play();
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        MainBooster.Stop();
    }
   
    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!RightBooster.isPlaying) RightBooster.Play();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!LeftBooster.isPlaying) LeftBooster.Play();
    }
 
    private void StopRotating()
    {
        RightBooster.Stop();
        LeftBooster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}