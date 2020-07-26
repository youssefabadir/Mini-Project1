using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider Volume;
    public AudioSource [] Audio;
    public bool MusicOn = false;
    public bool MusicOff = true;
    public GameObject MOn;
    public GameObject MOff;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < Audio.Length; i++)
        {
            Audio[i].volume = Volume.value;

        }
        if (Volume.value == 0)
        {
            MOn.SetActive(true);
            MOff.SetActive(false);
            MusicOn = true;
            MusicOff = false;
        }
        else
        {
            MOff.SetActive(true);
            MOn.SetActive(false);
            MusicOn = false;
            MusicOff = true;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
    }

    public void Quit()
    {
        Application.Quit(); 
    }

    public void Music()
    {
        if(MusicOff)
        {
            Volume.value = 0;
            MusicOn = true;
            MusicOff = false;
        }
        else
        {
            Volume.value = 1f;
            MusicOn = false;
            MusicOff = true;
        }
    }
}
