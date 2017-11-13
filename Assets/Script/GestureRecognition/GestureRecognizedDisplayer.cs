using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GestureRecognizedDisplayer : MonoBehaviour {
    public float MaxLastRecognizedTimer = 1.0f;
    public float LastRecognizedTimer = 0.0f;

    public GestureRecognizer Recognizer;

	// Use this for initialization
	void Start () {
        Recognizer.OnGestureRecognized += HandleGestureRecognized;
	}
	
	// Update is called once per frame
	void Update () {
        if (LastRecognizedTimer > 0.0f) {
            LastRecognizedTimer -= Time.deltaTime;
            
            if (LastRecognizedTimer < 0.0f) {
                LastRecognizedTimer = 0.0f;
            }
            
            float colorRatio = LastRecognizedTimer / MaxLastRecognizedTimer;
            
            GetComponent<TextMesh>().color = new Color(1.0f - colorRatio, colorRatio, 0.0f);
        }
	}
    
    protected virtual void HandleGestureRecognized(object sender, EventArgs e)
    {
        LastRecognizedTimer = MaxLastRecognizedTimer;
        
        GetComponent<TextMesh>().color = new Color(0.0f, 1.0f, 0.0f);
    }
}
