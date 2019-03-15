using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public AudioClip music;
    public float musicVol = 0.3f;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Music.instance.music.clip = music;
        Music.instance.music.Play();
        Music.instance.music.volume = musicVol;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
