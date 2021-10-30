using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public float bgm_volume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // comment this out if don't want to play on start
        PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            // quit game if escape is pressed
            QuitGame();
        }

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
        Application.Quit();
    }

    // call this function to play the sfx (in case you do not want to play on awake)
    public void PlayBGM()
    {
        this.GetComponent<AudioSource>().Play();
    }

}
