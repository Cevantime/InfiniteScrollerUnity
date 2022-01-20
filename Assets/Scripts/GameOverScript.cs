using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        GameObject.Find("GameManager").GetComponent<GameManager>().onGameFinished += Display;
    }

    void Display()
    {
        gameObject.SetActive(true);
    }
}
