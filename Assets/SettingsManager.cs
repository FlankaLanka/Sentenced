using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to Manage Settings (currently only bgm volume / mute)
// also handles pause/quitting game

public class SettingsManager : MonoBehaviour
{
    public float bgm_volume = 0.5f;
    public static bool paused = false;
    public bool quit = false;

    public GameObject pauseUI;
    public GameObject optionsUI;
    public GameObject quitUI;
    public GameObject fadeObj;

    // Start is called before the first frame update
    void Start()
    {
        // comment this out if don't want to play on start
        PlayBGM();


        paused = false;
        quit = false;

        if (pauseUI)
            pauseUI.SetActive(false);

        if (optionsUI)
            optionsUI.SetActive(false);
        
        if (quitUI)    
            quitUI.SetActive(false);
        
        else
            Debug.Log("WARNING: no quit UI object attached to SettingsManager.");
    }

    // Update is called once per frame
    void Update()
    {
        // Some basic scene settings -- TEMPORARY -- ideally hook this up to UI
        
        // ESC => quit game
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            // quit game if escape is pressed
            PauseGame();
            
        }
        // R => reset scene
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
        
        
        /**---------------------------
        // possible move this to some audio manager script?
        
        // M => mute bgm

        if(Input.GetKeyDown(KeyCode.M))
        {
            GetComponent<AudioSource>().mute = !GetComponent<AudioSource>().mute;
        }
        // constantly update volume
        // (can also do this manually on canvas click, etc.)
        GetComponent<AudioSource>().volume = bgm_volume;
        
        -------------------------------**/
    }

    public void PauseGame(){
        // toggle pausedUI when escape is pressed
        paused = !paused;
        if (paused) Time.timeScale = 0; 
        else Time.timeScale = 1; 
        
        quitUI.SetActive(false);
        optionsUI.SetActive(false);
        pauseUI.SetActive(paused);
    }

    // make this public function in case of use in buttons, etc.
    public void QuitGame()
    {
        Debug.Log("Call Application.Quit()");
        Application.Quit();    
    }

    
    public void ReturnToPauseMenu()
    {
        if (quitUI)
            quitUI.SetActive(false);
        if (optionsUI)
            optionsUI.SetActive(false);
    }

    // call this function to play the sfx (in case you do not want to play on awake)
    public void PlayBGM()
    {
        this.GetComponent<AudioSource>().Play();
    }

    public void NextScene(string sceneName)
    {
        StartCoroutine("SceneFadeToBlack", sceneName);
    }

    public IEnumerator SceneFadeToBlack(string sceneName)
    {
        if (fadeObj) 
            fadeObj.SetActive(true);
        
        // wait one second
        yield return new WaitForSeconds(1.0f);

        // change scene
        if (sceneName == "")
        {
            // if no sceneName is provided
            Debug.Log("No Scene Name provided, loading next scene in build index instead.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else 
        {
            Debug.Log("Loading Scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }

}
