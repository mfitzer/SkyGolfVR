using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnim : MonoBehaviour {
    SkinnedMeshRenderer skinMR;

	// Use this for initialization
	void Start () {
        skinMR = GetComponent<SkinnedMeshRenderer>();
	}

    public void gripped() //Open hand
    {
        skinMR.SetBlendShapeWeight(1, 100);
    }

    public void ungripped() //Close hand
    {
        skinMR.SetBlendShapeWeight(1, 30);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
