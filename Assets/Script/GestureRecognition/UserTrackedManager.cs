using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTrackedManager : MonoBehaviour {

    public static UserTrackedManager instance;
    public KinectPointController pointController;
    public bool isTracked;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    void Update () {
		if (pointController.isTracked != isTracked)
        {
            isTracked = pointController.isTracked;
            ChangeColor();
        }
	}

    void ChangeColor()
    {
        GetComponent<TextMesh>().text = (isTracked) ? "Tracked" : "Not Tracked";
        GetComponent<TextMesh>().color = (isTracked) ? new Color(0.0f, 1.0f, 0.0f) : new Color(1.0f, 0f, 0.0f);
    }
}
