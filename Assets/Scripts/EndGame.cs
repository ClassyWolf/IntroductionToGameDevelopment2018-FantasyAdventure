using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public Transform endGamePanel;

    // Open EndGamePanel
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Player")
        {
            endGamePanel.gameObject.SetActive(true);
            Time.timeScale = 0;

        }   
    }

    /*private PauseGame pauseGame;

	void Start ()
    {
        pauseGame = GetComponent<PauseGame>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Player")
        {
            pauseGame.canUsePauseMenu = false;
            pauseGame.endGamePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Game finnished");
        }
    }*/

}
