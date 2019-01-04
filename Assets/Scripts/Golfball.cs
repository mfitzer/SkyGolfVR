using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golfball : MonoBehaviour {
    Player player;
    Rigidbody rb;
    Vector3 zero;
    bool ballMoving;
    internal bool ballInPlay;
    internal bool thrown;
    internal bool crossedTeeBoundary;
    internal Vector3 stoppedPosition;
    internal Pickup pickup;
    AudioSource golfballHitSFX;
    public Transform boundary;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();
        pickup = GetComponent<Pickup>();
        rb = GetComponent<Rigidbody>();
        stoppedPosition = pickup.startPosition;
        resetHole();
        golfballHitSFX = GetComponent<AudioSource>();
	}

    public void resetHole() //Resets bools for new hole
    {
        ballMoving = false;
        ballInPlay = false;
        thrown = false;
        crossedTeeBoundary = false;
    }

    void checkVelocity()
    {
        if (rb.velocity == Vector3.zero && rb.angularVelocity == Vector3.zero)
        {
            if (ballMoving) //Ball just stopped
            {
                stoppedPosition = transform.position;
                ballMoving = false;
                if (ballInPlay)
                {
                    player.movePlayer();
                }
            }
        }
        else
        {
            ballMoving = true;
        }
    }

    void ballThrown()
    {
        //Resetting stuff after it was thrown
        resetHole();
        pickup.resetPosition = pickup.startPosition;
        StartCoroutine(delayReset(2));
    }

    void checkIfThrown()
    {
        if (thrown && crossedTeeBoundary && gameObject.layer != 9) //Layer 9 means it is being held
        {
            ballThrown();
            Debug.Log("You can't throw the ball!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeeBoundary"))
        {
            if (!thrown)
            {
                ballInPlay = true;
                player.teeBoundary.SetActive(false);
                ScoreKeeper.resetActiveHole();
                ScoreKeeper.addStroke();
                Debug.Log("Ball in play!");
            }
            else
            {
                ballThrown();
                Debug.Log("You can't throw the ball!");
            }
        }
        else if (other.CompareTag("OutOfBounds") && ballInPlay)
        {
            ScoreKeeper.addStroke();
            StartCoroutine(delayReset(0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Club"))
        {
            thrown = false;
            if (ballInPlay)
            {
                ScoreKeeper.addStroke();
                //Debug.Log("Strokes: " + ScoreKeeper.getStrokes());
            }
        }
        else if (collision.gameObject.CompareTag("Hand"))
        {
            Debug.Log("Hand hit");
            if (ballInPlay)
            {
                Debug.Log("That's cheating you can't roll the ball with your hand!");
                StartCoroutine(delayReset(1));
            }
        }

        //Sound effect
        if (collision.gameObject.CompareTag("Brick"))
        {
            golfballHitSFX.pitch = 0.5f;
            golfballHitSFX.volume = rb.velocity.magnitude / 3; //Gets percent between 0 and 1 (if over 1, than volume = 1
            golfballHitSFX.Play();
        }
    }

    IEnumerator delayReset(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (ScoreKeeper.ballInHole())
        {
            ScoreKeeper.invalidBallSink();
        }
        pickup.resetObj(pickup.resetPosition);
    }

    void setResetPosition()
    {
        if (ballInPlay)
        {
            pickup.resetPosition = stoppedPosition;
        }
        else
        {
            pickup.resetPosition = player.holes[ScoreKeeper.activeHole - 1].teeBox.position; //Reset to teebox of active hole
        }
    }

    // Update is called once per frame
    void Update () {
        checkVelocity();
        checkIfThrown();
        setResetPosition();

        if (transform.position.y < boundary.position.y)
        {
            transform.position = pickup.resetPosition;
        }
	}
}
