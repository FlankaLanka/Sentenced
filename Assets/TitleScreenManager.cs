using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    GameObject menuObj;
    GameObject settingsObj;
    GameObject creditsObj;

    // Start is called before the first frame update
    void Start()
    {
        
        if (!menuObj)
            Debug.Log("WARNING: No menuObj attached to TitleScreenManager.");
        if (!settingsObj)
            Debug.Log("WARNING: No settingsObj attached to TitleScreenManager.");
        if (!creditsObj)
            Debug.Log("WARNING: No creditsObj attached to TitleScreenManager.");
            
        // menuObj.SetActive(true);
        // settingsObj.SetActive(false);
        // creditsObj.SetActive(false);
    }

    // function called by button press
    public void StartGame() 
    {
        // load next scene in the build index
        // -- CAN CHANGE THIS IN BUILD SETTINGS
        //
        // can also directly load scene by name if build settings is screwy for some reason:
        // SceneManager.LoadScene("Interact");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // function called by button press
    public void OpenSettings()
    {
        menuObj.SetActive(false);
        
        settingsObj.SetActive(true);
    }

    // function called by button press
    public void OpenCredits()
    {
        menuObj.SetActive(false);

        creditsObj.SetActive(true);
    }

    
    // function called by button press
    public void ReturnToMainMenu(){
        settingsObj.SetActive(false);
        creditsObj.SetActive(false);

        menuObj.SetActive(true);
    }



    // function called by button press
    public void QuitGame()
    {
        StartCoroutine("Quit");
    }

    // quit game after brief delay (time for animation, sound effect, etc.?)
    IEnumerator Quit()
    {
        yield return new WaitForSeconds(0.25f);
        // add a fade animation?
        Application.Quit();
    }

}
