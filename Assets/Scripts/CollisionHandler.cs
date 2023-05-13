using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crashAudio;

    [SerializeField] ParticleSystem sucessParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Cheats();
    }


    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled) { return; }
            
        switch(other.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }    
    }


    void StartSuccessSequence()
    {
        isTransitioning = true;         
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        sucessParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("ReloadLevel", levelLoadDelay);    
    }


    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings){
            nextSceneIndex=0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void Cheats()
    {
        if(Input.GetKey(KeyCode.L))
        LoadNextLevel();

        else if(Input.GetKeyDown(KeyCode.C))
        collisionDisabled = !collisionDisabled;
    }

    
    
}
