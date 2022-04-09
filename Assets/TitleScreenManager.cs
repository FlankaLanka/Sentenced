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

    public Animation fadeEffect;
    
    // saves the current title screen state -- {"title", "settings", "credits", "quit"}
    public string currscreen = "title";
    bool fadeAudio = false;

    // Start is called before the first frame update
    void Start()
    {
        currscreen = "title";
        fadeAudio = false;

        // show new game or continue button?
        if (PlayerPrefs.GetInt("saved", 0) > 0){
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

        if(fadeAudio){
            GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0, Time.deltaTime);
        }
    }

    // start the game !!!
    public void StartGame() 
    {
        // load saved scene number, otherwise just go to next scene in buildIndex
        //if (PlayerPrefs.GetInt("saved", 0) > 0)
        //    SceneManager.LoadScene(PlayerPrefs.GetInt("saved", 1));
        //else
        StartCoroutine("FadeAndStart");
    }

    IEnumerator FadeAndStart(){
        fadeAudio = true;
        fadeEffect.Play();
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Prologue");
        
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
