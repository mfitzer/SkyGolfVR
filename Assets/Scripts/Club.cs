using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {
    bool inTeeBox = true;
    internal Pickup pickup;
    public Transform resetPosition;
    Rigidbody rb;
    public AudioSource golfballHitSFX;

	// Use this for initialization
	void Start () {
        pickup = GetComponent<Pickup>();
        rb = GetComponent<Rigidbody>();
        pickup.resetPosition = resetPosition.position;
        checkPosition();
	}

    void checkPosition()
    {
        if (!inTeeBox && gameObject.layer != 9) //Layer 9 means its being held
        {
            StartCoroutine(delayReset(2));
        }
        pickup.resetPosition = resetPosition.position;
    }

    IEnumerator delayReset(int delay)
    {
        yield return new WaitForSeconds(delay);
        if (!inTeeBox && gameObject.layer != 9)
        {
            Debug.Log("Teebox reset");
            pickup.resetObj(pickup.resetPosition);
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeeBox"))
        {
            inTeeBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TeeBox"))
        {
            inTeeBox = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            golfballHitSFX.pitch = 0.8f;
            golfballHitSFX.volume = (rb.velocity.magnitude * 100000000) / 6; //Gets percent between 0 and 1 (if over 1, than volume = 1)
            golfballHitSFX.Play();
        }

        Debug.Log("Club collided with: " + collision.gameObject.name);
    }

    // Update is called once per frame
    void Update () {
        checkPosition();

        if (rb.isKinematic)
        {
            Debug.Log("Club kinematic");
        }
	}
}
