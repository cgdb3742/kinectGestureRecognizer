using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRecognizer : GestureRecognizer {
    public float MaxDistance = 0.1f;
    public float MinDuration = 1.0f;
    
    protected float _punchTimer = 0.0f;

    // Update is called once per frame
	protected override void UpdateRecognizer()
    {
        float currentDistance = Vector3.Distance(PointController.Hand_Right.transform.position, PointController.Hand_Left.transform.position);

		//Debug.Log (currentDistance);

        if (currentDistance <= MaxDistance)
        {
            _punchTimer += Time.deltaTime;

            if (_punchTimer >= MinDuration)
            {
                GestureRecognized();

                Debug.Log("Exit Gesture Recognized.");
            }
        }
        else
        {
            _punchTimer -= Time.deltaTime;

            if (_punchTimer <= 0.0f)
            {
                Reset();
            }
        }
    }

    protected override void Reset()
    {
        _punchTimer = 0.0f;
    }
}
