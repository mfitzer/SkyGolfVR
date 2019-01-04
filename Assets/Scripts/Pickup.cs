using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    InputManager inputManager;
    Golfball golfball;
    Club club;
    public GameObject[] children;
    internal Vector3 startPosition;
    internal Vector3 resetPosition;
    internal Quaternion startRotation;
    Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (inputManager.rightHand.objInHand == obj)
        {
            inputManager.rightController.TriggerHapticPulse(1500);
        }
        else if (inputManager.leftHand.objInHand == obj)
        {
            inputManager.leftController.TriggerHapticPulse(1500);
        }
    }

    public void resetObj(Vector3 position)
    {
        transform.position = position;
        transform.rotation = startRotation;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
    }

    // Use this for initialization
    void Start () {
        inputManager = FindObjectOfType<InputManager>();
        Golfball golfball = FindObjectOfType<Golfball>();
        Club club = FindObjectOfType<Club>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
