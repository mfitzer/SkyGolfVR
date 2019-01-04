using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour {
    public Hand hand;
    public Golfball golfball;

	// Use this for initialization
	void Start () {
		
	}

    IEnumerator makeClubInteractive(Collider other)
    {
        yield return new WaitForSeconds(0.25f);
        other.GetComponent<Rigidbody>().isKinematic = false;
        other.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Club":
                hand.collidingObj = other.gameObject;
                StartCoroutine(makeClubInteractive(other));
                break;
            case "ClubParts":
                hand.collidingObj = other.transform.parent.gameObject;
                break;
            case "Ball":
                if (!golfball.ballInPlay) //Can't pick up ball while in play
                {
                    hand.collidingObj = other.gameObject;
                }
                break;
        }

        if (other.transform.CompareTag("TeeBoundary"))
        {
            golfball.crossedTeeBoundary = !golfball.crossedTeeBoundary;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Club":
                hand.collidingObj = other.gameObject;
                break;
            case "ClubParts":
                hand.collidingObj = other.transform.parent.gameObject;
                break;
            case "Ball":
                if (!golfball.ballInPlay) //Can't pick up ball while in play
                {
                    hand.collidingObj = other.gameObject;
                }
                break;
            case "TeeBoundary":
                hand.collidingObj = other.gameObject;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Club":
                hand.collidingObj = null;
                break;
            case "ClubParts":
                hand.collidingObj = null;
                break;
            case "Ball":
                if (!golfball.ballInPlay) //Can't pick up ball while in play
                {
                    hand.collidingObj = null;
                }
                break;
            case "TeeBoundary":
                hand.collidingObj = null;
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
