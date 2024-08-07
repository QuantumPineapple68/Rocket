using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHnadler : MonoBehaviour
{
   [SerializeField] private AudioClip collision;
   [SerializeField] private AudioClip success;
   [SerializeField] private ParticleSystem collisionParticle;
   [SerializeField] private ParticleSystem successParticle;
   
   AudioSource audioSource;
   private bool StopEvents = false;
   private bool cheat = false;
   private void Start()
   {
      audioSource = GetComponent<AudioSource>();
   }

   private void Update()
   {
      debugCheat();
   }

   void debugCheat()
   {
      if (Input.GetKey(KeyCode.C))
      {
         cheat = true;
      }
      else if (Input.GetKey(KeyCode.L))
      {
         LoadNextLevel();
      }  
   }
   
   void OnCollisionEnter(Collision other)
   {
      if (StopEvents) { return; }
      switch (other.gameObject.tag)
      {
         case "Friendly":
            break;
         case "Finish":
            SuccesSequence();
            break;
         default:
            if (cheat == false)
            {
               CrashSequence();
            }
            else
            {
               break;
            }

            break;
      }
   }
   
   void SuccesSequence()
   {
      successParticle.Play();
      audioSource.Stop();
      audioSource.PlayOneShot(success);
      GetComponent<Movement>().enabled = false;
      Invoke("LoadNextLevel", 1.5f);
      GetComponent<Rigidbody>().freezeRotation = false;
      StopEvents = true;
   }
   void CrashSequence()
   {
      collisionParticle.Play();
      audioSource.Stop();
      audioSource.PlayOneShot(collision);
      GetComponent<Movement>().enabled = false;
      Invoke("ReloadLevel", 2f);
      StopEvents = true;
   }

   void ReloadLevel()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }


   void LoadNextLevel()
   {
      
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentSceneIndex+1);
      int nextSceneIndex = currentSceneIndex + 1;
      if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
      {
         nextSceneIndex = 0;
      }
      SceneManager.LoadScene(nextSceneIndex);
   }
}


