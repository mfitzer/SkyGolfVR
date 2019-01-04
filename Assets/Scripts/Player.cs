using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public Hole[] holes;
    Golfball golfball;
    Club club;
    float yOffset;
    public GameObject teeBoundary;
    public Transform logo;

	// Use this for initialization
	void Start () {
        golfball = FindObjectOfType<Golfball>();
        club = FindObjectOfType<Club>();

        //Setting up first hole
        moveToTeeBox();
    }

    public void rotateLogo()
    {
        Vector3 rotation = logo.rotation.eulerAngles;
        logo.rotation = Quaternion.Euler(rotation.x, rotation.y - 90, rotation.z);
    }

    public void nextHole()
    {
        if (ScoreKeeper.ballInHole())
        {
            if (ScoreKeeper.activeHole < holes.Length)
            {
                ScoreKeeper.activeHole++;
                moveToTeeBox();
                rotateLogo();
            }
            else //End of game, restart
            {
                holes[ScoreKeeper.activeHole - 1].scoreDisplay.SetActive(false); //Disable last holes score display
                ScoreKeeper.restartGame();
                moveToTeeBox();
                rotateLogo();
            }
        }
    }

    void moveToTeeBox()
    {
        teeBoundary.SetActive(true);
        if (ScoreKeeper.activeHole - 2 >= 0)
        {
            holes[ScoreKeeper.activeHole - 2].scoreDisplay.SetActive(false); //Previous hole
        }
        Vector3 teeBoxPos = holes[ScoreKeeper.activeHole - 1].teeBox.position;

        golfball.transform.position = new Vector3(teeBoxPos.x, teeBoxPos.y + 0.025f, teeBoxPos.z); //Ball at active hole teebox
        Vector3 rotation = holes[ScoreKeeper.activeHole - 1].teeBox.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotation.y, transform.rotation.z);

        golfball.pickup.resetPosition = golfball.transform.position;
        golfball.stoppedPosition = golfball.transform.position;
        movePlayer(); //Moving player to golfball position
    }

    public void holeComplete()
    {
        //Update score display
        Hole activeHole = holes[ScoreKeeper.activeHole - 1];
        activeHole.holeScoreText.text = "Hole " + ScoreKeeper.activeHole + " Score: " + ScoreKeeper.getStrokes();
        activeHole.totalScoreText.text = "Total Score: " + ScoreKeeper.computeScore();
        if (activeHole.holeNumber == holes.Length) //Last hole
        {
            activeHole.nextHoleText.text = "Congratulations! You \ncompleted the course! \n\n- CLICK CONTROLLER PAD -\n- TO PLAY AGAIN -";
        }
        else //More holes
        {
            activeHole.nextHoleText.text = "- CLICK CONTROLLER PAD -\n- TO GO TO NEXT HOLE -";
        }
        activeHole.scoreDisplay.SetActive(true);
        movePlayer(); //Moving player to golfball position
    }

    void setYOffset() //Keeping camera rig at ground level
    {
        if (ScoreKeeper.ballInHole())
        {
            yOffset = 0.079f;
        }
        else
        {
            yOffset = -0.025f;
        }
    }

    public void movePlayer()
    {
        setYOffset();
        transform.position = new Vector3(golfball.transform.position.x, golfball.transform.position.y + yOffset, golfball.transform.position.z);
        if (club.gameObject.layer != 9) //Club not being held by player
        {
            club.transform.position = club.resetPosition.position;
            club.transform.rotation = club.resetPosition.rotation;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
