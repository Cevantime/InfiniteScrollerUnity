using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        INIT,
        READY,
        RUNNING,
        FINISHED
    }

    private GameState state = GameState.INIT;

    public GameState State
    {
        get
        {
            return state;
        }
    }

    public delegate void GameStartedDel();

    public GameStartedDel onGameStarted;

    public delegate void ScoreChangedDel(int score);

    public ScoreChangedDel onScoreChanged;

    public delegate void GameFinishedDel();

    public GameFinishedDel onGameFinished;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetReady()
    {
        state = GameState.READY;
    }

    public void IncScore(int increment)
    {
        score += increment;
        onScoreChanged?.Invoke(score);
    }

    public void FinishGame()
    {
        state = GameState.FINISHED;
        onGameFinished?.Invoke();
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && state == GameState.READY)
        {
            state = GameState.RUNNING;
            if (onGameStarted != null)
            {
                onGameStarted();
            }
        }
    }
}
