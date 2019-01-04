using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    //Vive Controller Input
    public SteamVR_TrackedObject leftTrackedObj;
    public SteamVR_TrackedController leftTrackedController;
    public SteamVR_Controller.Device leftController
    {
        get { return SteamVR_Controller.Input((int)leftTrackedObj.index); }
    }
    public SteamVR_TrackedObject rightTrackedObj;
    public SteamVR_TrackedController rightTrackedController;
    public SteamVR_Controller.Device rightController
    {
        get { return SteamVR_Controller.Input((int)rightTrackedObj.index); }
    }

    public Player player;
    public Hand rightHand;
    public Hand leftHand;

    void rightTriggerClicked(object sender, ClickedEventArgs e)
    {
        
    }

    void rightTriggerDown()
    {
        rightHand.grabObject();
    }

    void rightTriggerUp()
    {
        rightHand.releaseObject(rightController);
    }

    void rightPadClicked(object sender, ClickedEventArgs e)
    {
        player.nextHole();
    }

    void rightGripped(object sender, ClickedEventArgs e)
    {

    }

    void leftTriggerClicked(object sender, ClickedEventArgs e)
    {
        
    }

    void leftTriggerDown()
    {
        leftHand.grabObject();
    }

    void leftTriggerUp()
    {
        leftHand.releaseObject(leftController);
    }

    void leftPadClicked(object sender, ClickedEventArgs e)
    {
        player.nextHole();
    }    

    void leftGripped(object sender, ClickedEventArgs e)
    {

    }

    // Use this for initialization
    void Start()
    {
        //Registering functions with input events
        leftTrackedController.PadClicked += leftPadClicked;
        leftTrackedController.TriggerClicked += leftTriggerClicked;
        leftTrackedController.Gripped += leftGripped;
        rightTrackedController.PadClicked += rightPadClicked;
        rightTrackedController.TriggerClicked += rightTriggerClicked;
        rightTrackedController.Gripped += rightGripped;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftController.GetHairTriggerDown())
        {
            leftTriggerDown();
        }
        if (rightController.GetHairTriggerDown())
        {
            rightTriggerDown();
        }
        if (leftController.GetHairTriggerUp())
        {
            leftTriggerUp();
        }
        if (rightController.GetHairTriggerUp())
        {
            rightTriggerUp();
        }
    }
}
