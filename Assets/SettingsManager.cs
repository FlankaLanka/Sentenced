using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to Manage Settings (currently only bgm volume / mute)
// also handles quitting game

public class SettingsManager : MonoBehaviour
{
    public float bgm_volume = 0.5f;
    public bool quit = false;

    public GameObject quitUI;
    public GameObject fadeObj;

    // Start is called before the first frame update
    void Start()
    {
        // comment this out if don't want to play on start
        PlayBGM();
        quit = false;
        if (quitUI) quitUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Some basic scene settings -- TEMPORARY -- ideally hook this up to UI
        
        // ESC => quit game
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            // quit game if escape is pressed
            QuitGame();
        }
        // R => reset scene
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
        // M => mute bgm
        if(Input.GetKeyDown(KeyCode.M))
        {
            GetComponent<AudioSource>().mute = !GetComponent<AudioSource>().mute;
        }

        // constantly update volume
        // (can also do this manually on canvas click, etc.)
        GetComponent<AudioSource>().volume = bgm_volume;
    }

    // make this public function in case of use in buttons, etc.
    public void QuitGame()
    {
        // can add other features (fade out? save game? etc.)
        if(quit){
            Debug.Log("Call Application.Quit()");
            Application.Quit();    
        }

        quit = true;
        quitUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CancelQuit()
    {
        Time.timeScale = 1;
        quit = false;
        quitUI.SetActive(false);
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
