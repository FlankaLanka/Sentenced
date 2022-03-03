using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject menuObj;
    public GameObject settingsObj;
    public GameObject creditsObj;
    public GameObject quitObj;

    public GameObject newGameButton;
    public GameObject continueButton;
    
    public string currscreen = "title";
    //public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = this.GetComponent<Animator>();
        currscreen = "title";

        if (PlayerPrefs.GetInt("saved", 0) == 1){
            newGameButton.SetActive(false);
            continueButton.SetActive(true);
        }


        if (!menuObj)
            Debug.Log("WARNING: No menuObj attached to TitleScreenManager.");
        if (!settingsObj)
            Debug.Log("WARNING: No settingsObj attached to TitleScreenManager.");
        if (!creditsObj)
            Debug.Log("WARNING: No creditsObj attached to TitleScreenManager.");
        if (!quitObj)
            Debug.Log("WARNING: No creditsObj attached to TitleScreenManager.");
            
        // menuObj.SetActive(true);
        // settingsObj.SetActive(false);
        // creditsObj.SetActive(false);
    }

    public void Update(){
        // use esc key to quit or cancel quit
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (currscreen == "title"){
                QuitGame();
            }
            else{
                ReturnToMainMenu();
            }
        }
    }

    // function called by button press
    public void StartGame() 
    {
        // load next scene in the build index
        // -- CAN CHANGE THIS IN BUILD SETTINGS
        //
        // can also directly load scene by name if build settings is screwy for some reason:
        // SceneManager.LoadScene("Interact");

        // save that player started the game
        PlayerPrefs.SetInt("saved", 1);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // function called by button press
    public void OpenSettings()
    {
        if (currscreen == "credits")    creditsObj.GetComponent<Animator>().Play("fade-out");
        else if (currscreen == "quit")  quitObj.GetComponent<Animator>().Play("fade-out"); //quitObj.SetActive(false);
        
        if (currscreen == "title")      menuObj.GetComponent<Animator>().Play("slide-left");

        settingsObj.GetComponent<Animator>().Play("fade-in");

        // reset settings everytime this window is opened
        this.GetComponent<SettingsManager>().InstantiateSettings();

        currscreen = "settings";
        //menuObj.SetActive(false);
        //settingsObj.SetActive(true);
    }

    // function called by button press
    public void OpenCredits()
    {
        if(currscreen == "settings")    settingsObj.GetComponent<Animator>().Play("fade-out");//settingsObj.SetActive(false);
        if (currscreen == "quit")       quitObj.GetComponent<Animator>().Play("fade-out"); //quitObj.SetActive(false);
        
        if (currscreen == "title")      menuObj.GetComponent<Animator>().Play("slide-left");

        settingsObj.GetComponent<Animator>().Play("fade-in");
        currscreen = "credits";

        //menuObj.SetActive(false);
        //creditsObj.SetActive(true);
    }
    
    // function called by button press
    public void ReturnToMainMenu(){
        
        //menuObj.GetComponent<Animator>().Play("slide-right");
        if (currscreen == "settings")       settingsObj.GetComponent<Animator>().Play("fade-out");
        else if (currscreen == "credits")   creditsObj.GetComponent<Animator>().Play("fade-out");
        else if (currscreen == "quit")      quitObj.GetComponent<Animator>().Play("fade-out"); //quitObj.SetActive(false);

        currscreen = "title";
        menuObj.GetComponent<Animator>().Play("slide-right");

        //settingsObj.SetActive(false);
        //creditsObj.SetActive(false);
        //menuObj.SetActive(true);
    }



    // function called by button press
    public void QuitGame()
    {
        if (currscreen == "settings")       settingsObj.GetComponent<Animator>().Play("fade-out");//settingsObj.SetActive(false);
        else if (currscreen == "credits")   creditsObj.GetComponent<Animator>().Play("fade-out"); //creditsObj.SetActive(false);
        
        if (currscreen == "title")          menuObj.GetComponent<Animator>().Play("slide-left");

        currscreen = "quit";
        quitObj.GetComponent<Animator>().Play("fade-in");
        
        //StartCoroutine("Quit");
        
    }

    // quit game after brief delay (time for animation, sound effect, etc.?)
    IEnumerator Quit()
    {
        yield return new WaitForSeconds(0.25f);
        // add a fade animation?
        Application.Quit();
    }

}
