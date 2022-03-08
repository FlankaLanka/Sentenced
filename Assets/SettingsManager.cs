using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Class to Manage Settings (currently only bgm volume / mute)
// also handles pause/quitting game

public class SettingsManager : MonoBehaviour
{
    // default values; modified in InstantiateSettings()
    public float game_volume = 5f;
    public float music_volume = 5f;
    public float dialogue_volume = 5f;
    public float sfx_volume = 5f;

    // slider stuff -- assign manually in inspector
    public Slider g_vol;
    public Slider m_vol;
    public Slider d_vol;
    public Slider s_vol;
    public TextMeshProUGUI gv_text;
    public TextMeshProUGUI mv_text;
    public TextMeshProUGUI dv_text;
    public TextMeshProUGUI sv_text;
    
    // for pausing/quitting game?
    public static bool paused = false;
    public bool quit = false;

    // random UI stuff -- FOR PAUSE SCREEN
    public GameObject pauseUI;
    public GameObject optionsUI;
    public GameObject quitUI;
    public GameObject fadeObj;
    public bool canPause = true;

    // Start is called before the first frame update
    void Start()
    {
        // comment this out if don't want to play on start
        PlayBGM();

        InstantiateSettings();

        paused = false;
        quit = false;
        
        if (pauseUI)
            pauseUI.SetActive(false);
        if (optionsUI)
            optionsUI.SetActive(false);
        if (quitUI)
            quitUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Some basic scene settings -- TEMPORARY -- ideally hook this up to UI
        
        // ESC => quit game
        if(canPause && Input.GetKeyDown(KeyCode.Escape))
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
        // M => mute bgm -- possibly move this to some audio manager script?

        if(Input.GetKeyDown(KeyCode.M))
        {
            GetComponent<AudioSource>().mute = !GetComponent<AudioSource>().mute;
        }
        -------------------------------**/
    }

    // call this function to play the sfx (in case you do not want to play on awake)
    public void PlayBGM()
    {
        // play audio if it is attached
        if(GetComponent<AudioSource>())
            GetComponent<AudioSource>().Play();
        else
            Debug.Log("WARNING: Cannot Locate Audio Source attached to SettingsManager Object!!!");
    }

    // Pause/Unpause Game
    public void PauseGame()
    {
        // toggle pausedUI when escape is pressed
        paused = !paused;
        // Debug.Log("Pause:" + paused);

        if (paused) 
        {
            Time.timeScale = 0; 
        }
        else 
        {
            Time.timeScale = 1;
            // slide text back to the right 
            SlideText(false);
        }
        quitUI.SetActive(false);
        optionsUI.SetActive(false);
        pauseUI.SetActive(paused);
    }

    // Save and Quit game
    public void QuitGame()
    {
        Debug.Log("Call Application.Quit()");
        
        // save game at CURRENT SCENE before quitting
        PlayerPrefs.SetInt("saved", SceneManager.GetActiveScene().buildIndex);

        Application.Quit();    
    }

    // pauseMenu
    public void ReturnToPauseMenu()
    {
        if (quitUI)
            quitUI.SetActive(false);
        if (optionsUI)
            optionsUI.SetActive(false);

        // slide pause screen back to left
        SlideText(false);
    }

    // Move to Next scene with fade-to-black transition
    public void NextScene(string sceneName)
    {
        StartCoroutine("SceneFadeToBlack", sceneName);
    }

    // actual fade-to-black code
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
    
    // slide paused/menu text based on bool input
    // true = slide to left, false = slide to right
    public void SlideText(bool left)
    {
        //Debug.Log("Slide left? " + left);

        if(left){
            GameObject.Find("Pause Stuff").GetComponent<Animator>().Play("slide-left");
        }
        else{
            GameObject.Find("Pause Stuff").GetComponent<Animator>().Play("slide-right");
        }
    }

    // call fade animations for options/quit, etc.
    // obj -- object to play fade animation for
    // in -- true if play fade-in animation, false if play fade-out animation
    public void FadeAnim (string toPlay)
    {
        //Debug.Log("Anim: " + toPlay);
        
        if(toPlay == "options-in")
        {

            optionsUI.GetComponent<Animator>().Play("fade-in");
        }
        else if(toPlay == "options-out")
        {
            optionsUI.GetComponent<Animator>().Play("fade-out");
        }
        else if(toPlay == "quit-in")
        {
            if (GameObject.Find("Options UI (buttons, sliders)"))
                optionsUI.GetComponent<Animator>().Play("fade-out");
            quitUI.GetComponent<Animator>().Play("fade-in");
        }
        else if(toPlay == "quit-out")
        {
            quitUI.GetComponent<Animator>().Play("fade-out");
        }
    }





    ///////////////////////////////////////
    //-----------------------------------//
    //--   **ACTUAL SETTINGS STUFF**   --//
    //-----------------------------------//
    ///////////////////////////////////////


    // Setting up the sound settings based on PlayerPrefs
    public void InstantiateSettings()
    {
        // getting values from PlayerPrefs
        game_volume = PlayerPrefs.GetFloat("g_vol", 5f);
        music_volume = PlayerPrefs.GetFloat("m_vol", 5f);
        dialogue_volume = PlayerPrefs.GetFloat("d_vol", 5f);
        sfx_volume = PlayerPrefs.GetFloat("s_vol", 5f);

        // instantiating slider values
        g_vol.value = game_volume;
        m_vol.value = music_volume;
        d_vol.value = dialogue_volume;
        s_vol.value = sfx_volume;
        
        // set slider text after all values have been set
        SetSettingsText();
    }

    // Change settings based on sliders 
    //**DOES NOT SAVE SETTINGS**
    public void UpdateSettings()
    {
        //Debug.Log("Changing Settings");

        game_volume = g_vol.value;
        music_volume = m_vol.value;
        dialogue_volume = d_vol.value;
        sfx_volume = s_vol.value;
        
        SetSettingsText();
    }

    // Change slider text
    public void SetSettingsText()
    {
        //Debug.Log("Changing Slider Text");

        gv_text.text = game_volume.ToString();
        mv_text.text = music_volume.ToString();
        dv_text.text = dialogue_volume.ToString();
        sv_text.text = sfx_volume.ToString();
    }

    // Save Settings to Player Prefs
    public void SaveSettings()
    {
        //Debug.Log("saving values");

        PlayerPrefs.SetFloat("g_vol", g_vol.value);
        PlayerPrefs.SetFloat("m_vol", m_vol.value);
        PlayerPrefs.SetFloat("d_vol", d_vol.value);
        PlayerPrefs.SetFloat("s_vol", s_vol.value);

        PlayerPrefs.Save();
    }
}
