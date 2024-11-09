using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _restartScreenObject;
    [SerializeField] public GameObject startScreenObject;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Bird bird;
    [SerializeField] private ScoreDisplay scoreDisplay;
    private bool isPaused = true;
    public void LoseGame()
    {
        //_restartScreenObject.transform.position = bird.transform.position;
        _restartScreenObject.SetActive(true);
        PauseGame();
    }

    private void Start()
    {
        scoreDisplay.UpdateScore(0);
    }


    public void PauseGame()
    {
        cameraManager.SwitchToIdleCamera();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnpauseGame()
    {
        cameraManager.SwitchFollowCamera();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int amount)
    {
        Debug.Log(scoreDisplay.currentScore);
        scoreDisplay.UpdateScore(scoreDisplay.currentScore + amount);
    }
}
