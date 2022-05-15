using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
   [SerializeField] float leveLoadDelay = 2.0f;
   void OnCollisionEnter(Collision other)
   {
      switch (other.gameObject.tag)
      {
         case "Start": 
            Debug.Log("This thing is your stat point"); 
            break;
         case "Finish":
            StartSuccessSequence();
            break;
         case "Obstacle": 
            Debug.Log("You hit an obstacle!");
            goto default;
         default:
            StartCrashSequence();
            break;
      }   
   }

   void LoadNextLevel()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      int nextSceneIndex = currentSceneIndex + 1;
      if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) nextSceneIndex = 0;
      SceneManager.LoadScene(nextSceneIndex);
   }

   void ReloadLevel()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
      SceneManager.LoadScene(currentSceneIndex);
   }

   void StartCrashSequence()
   {
      GetComponent<Movement>().enabled = false;
      Invoke(nameof(ReloadLevel), leveLoadDelay);
   }

   void StartSuccessSequence()
   {
      GetComponent<Movement>().enabled = false;
      Invoke(nameof(LoadNextLevel), leveLoadDelay); 
   }
}
