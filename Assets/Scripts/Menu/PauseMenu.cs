using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseGameMenu;

    private bool _isPause;

    private void Awake()
    {
        _pauseGameMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        _pauseGameMenu.SetActive(false);
        Time.timeScale = 1.0f;
        _isPause = false;
    }

    private void Pause()
    {
        _pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPause = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}