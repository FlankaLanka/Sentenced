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

    // random UI stuff
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
        
        else
            Debug.Log("WARNING: no quit UI object attached to SettingsManager.");
    }

    // Update is called once per frame
    void Update()
    {
        // Some basic scene settings -- TEMPORARY -- ideally hook this up to UI
        
        // ESC => quit game
        if(canPause && Input.GetKeyUp(KeyCode.Escape))
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
        // play audio if it is attached
        if(GetComponent<AudioSource>())
            GetComponent<AudioSource>().Play();
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

    // Setting up the sound settings based on PlayerPrefs
    public void InstantiateSettings(){
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

    public void UpdateSettings()
    {
        Debug.Log("Changing Settings");

        game_volume = g_vol.value;
        music_volume = m_vol.value;
        dialogue_volume = d_vol.value;
        sfx_volume = s_vol.value;
        
        SetSettingsText();
    }

    public void SetSettingsText()
    {
        Debug.Log("Changing Slider Text");

        gv_text.text = game_volume.ToString();
        mv_text.text = music_volume.ToString();
        dv_text.text = dialogue_volume.ToString();
        sv_text.text = sfx_volume.ToString();
    }

    // Save New Settings
    public void SaveSettings(){
        Debug.Log("saving values");

        PlayerPrefs.SetFloat("g_vol", g_vol.value);
        PlayerPrefs.SetFloat("m_vol", m_vol.value);
        PlayerPrefs.SetFloat("d_vol", d_vol.value);
        PlayerPrefs.SetFloat("s_vol", s_vol.value);

        PlayerPrefs.Save();
    }
}
