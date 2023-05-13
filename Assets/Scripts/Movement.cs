using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float pushThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem  mainEngineParticles;
    [SerializeField] ParticleSystem  leftThrusterParticles;
    [SerializeField] ParticleSystem  rightThrusterParticles;
    

    Rigidbody rb;
    AudioSource audioSource;

    bool isAlive;

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
       if(Input.GetKey(KeyCode.Space))
            StartThrusting();
       else
            StopThrusting();
            
    }

    void ProcessRotation()
    {
        if( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))

            RotateLeft();

        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))

            RotateRight();
        
        else
            StopRotating();
        
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * pushThrust * Time.deltaTime);

        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);     

        if(!mainEngineParticles.isPlaying)
            mainEngineParticles.Play();
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
   
    void RotateLeft()
    {
        ApplyRotation(rotationThrust);

        if(!rightThrusterParticles.isPlaying)
            rightThrusterParticles.Play();
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);

        if(!leftThrusterParticles.isPlaying)
            leftThrusterParticles.Play();
    }

    void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true ;
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        rb.freezeRotation = false ;
    }

    
   
}