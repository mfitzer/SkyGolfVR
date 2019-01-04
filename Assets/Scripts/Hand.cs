using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    public HandAnim handAnim;
    internal GameObject collidingObj;
    internal GameObject objInHand;
    int objLayer;

    // Use this for initialization
    void Start()
    {

    }

    void changeLayer (GameObject obj, int layer)
    {
        obj.layer = layer;
        for (int i = 0; i < obj.GetComponent<Pickup>().children.Length; i++)
        {
            obj.GetComponent<Pickup>().children[i].layer = layer;
        }
    }

    public void grabObject()
    {
        if (collidingObj != null)
        {
            objInHand = collidingObj;
            collidingObj = null;
            FixedJoint joint = objInHand.AddComponent<FixedJoint>();
            joint.connectedBody = transform.GetComponent<Rigidbody>();
            objLayer = objInHand.layer;
            changeLayer(objInHand, 9); //Only collides with interactables
        }

        handAnim.gripped();
    }

    public void releaseObject(SteamVR_Controller.Device controller)
    {
        if (objInHand != null)
        {
            FixedJoint joint = objInHand.GetComponent<FixedJoint>();
            Rigidbody objRig = objInHand.GetComponent<Rigidbody>();
            if (objInHand.CompareTag("Ball"))
            {
                objInHand.GetComponent<Golfball>().thrown = true;
            }
            if (joint != null)
            {
                joint.connectedBody = null;
                Destroy(joint);
                objRig.velocity = controller.velocity;
                objRig.angularVelocity = controller.angularVelocity;

                //Changing layer
                changeLayer(objInHand, objLayer); //Collides with everything
                objInHand = null;
            }
        }

        handAnim.ungripped();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
