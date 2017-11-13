using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GestureRecognizer : MonoBehaviour {

    public KinectPointController PointController;
    public KinectModelControllerV2 ModelController;

    public event EventHandler OnGestureRecognized;
    
	// Use this for initialization
	protected virtual void Start () {
        Reset();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		bool isTracked = PointController.isTracked;

		if (isTracked) {
			UpdateRecognizer ();
		} else {
			Reset ();
		}
	}

	protected virtual void UpdateRecognizer() {

	}
    
    protected virtual void GestureRecognized () {
        if (UserTrackedManager.instance.isTracked)
        {
            EventHandler handler = OnGestureRecognized;
            EventArgs e = new EventArgs();

            if (handler != null)
            {
                handler(this, e);
            }

            Reset();
        }
    }
    
    protected virtual void Reset () {
        
    }
}
