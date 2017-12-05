using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject gameOverPanel;
    public Text gameOverText;

    private Player player;
    private int bootyCollected = 0;

    void Awake()
    {
        gameOverPanel.SetActive(false);
    }
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {

        if (player == null)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Arr...That Would Be Game Over!\nBooty plundard: " + bootyCollected;
        }
        else
            bootyCollected = player.AmountBootyEverCollected();
		
	}
}
