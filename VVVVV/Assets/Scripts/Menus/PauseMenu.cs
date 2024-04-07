using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    public GameObject canvas;
    public Transform player;

    private void Start()
    {
        player.GetComponent<PlayerController>().enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Resume();
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        player.GetComponent<PlayerController>().enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartAll()
    {
        Time.timeScale = 1;
        player.GetComponent<PlayerController>().enabled = true;
        SceneManager.LoadScene(1);

    }
}
