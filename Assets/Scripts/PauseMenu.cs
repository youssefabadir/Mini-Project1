using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isPause = false;
    public GameObject PMenu;
    public GameObject Options;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

/*    public void isPaused()
    {
        if (isPause)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
*/
    public void Resume()
    {
        Options.SetActive(false);
        PMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
    void Pause()
    {
        PMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }

    public void Restart()
    {

        PMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Options.SetActive(false);
        PMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
