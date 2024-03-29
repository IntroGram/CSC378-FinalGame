﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    /* 
     * Description: functionality for the pause menu
     */
    public Button quit;
    public AudioClip menuMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* 
         * Description: checks if the player tries to pause the game when it is acceptable to pause the game
         */
        /*if (GameController.instance.paused)
        {*/
            quit.gameObject.SetActive(true);
            quit.Select();
        /*}
        else
        {
            quit.gameObject.SetActive(false);
        }*/
    }

    /*public void returnToMenu()
    {
        /*
         * Description: returns to the main menu from the pause menu
         *
        GameController.instance.speedyMusic = false;
        Music.instance.music.clip = menuMusic;
        Music.instance.music.volume = 1;
        Music.instance.music.pitch = 1;
        Music.instance.music.Play();
        SceneManager.LoadScene("Main_Menu");
    }*/

    public void playAgain()
    {
        /* 
         * Description: reloads current scene from beginning
         */
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
