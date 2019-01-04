using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hole : MonoBehaviour {
    Player player;
    Golfball golfball;
    public int holeNumber;
    public Transform teeBox;
    public GameObject scoreDisplay;
    public Text holeScoreText;
    public Text totalScoreText;
    public Text nextHoleText;
    public AudioSource ballSunkSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && golfball.ballInPlay && ScoreKeeper.activeHole == holeNumber)
        {
            golfball.resetHole();
            ballSunk();
            ballSunkSFX.Play();
        }
    }

    void ballSunk()
    {
        ScoreKeeper.sunkBall();
        player.holeComplete();
        Debug.Log("You made it in!");
    }

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();
        golfball = FindObjectOfType<Golfball>();
        scoreDisplay.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
