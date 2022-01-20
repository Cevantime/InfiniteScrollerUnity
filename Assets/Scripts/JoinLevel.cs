using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinLevel : MonoBehaviour
{
    public void OnJoinLevel()
    {
        SceneManager.LoadScene("Scenes/Gameplay");
    }
}
