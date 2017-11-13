using UnityEngine;
using System.Collections;

public class RightPunchRecognizer : GestureRecognizer {
    public float MinPunchDuration = 0.2f;
    public float MinPunchSpeed = 1.0f;
    
    protected float _prevZ = 0.0f;
    protected float _punchTimer = 0.0f;
	
	// Update is called once per frame
	protected override void UpdateRecognizer () {        
        float currentZ = PointController.Hand_Right.transform.position.z;
        float punchSpeed = (currentZ - _prevZ) / Time.deltaTime;
        
        if (punchSpeed >= MinPunchSpeed) {
            _punchTimer += Time.deltaTime;
            
            if (_punchTimer >= MinPunchDuration) {
                GestureRecognized();
                    
                Debug.Log("Right Punch Recognized.");
            }
        } else {
            _punchTimer -= Time.deltaTime;
            
            if (_punchTimer <= 0.0f) {
                Reset();
            }
        }
        
        _prevZ = currentZ;
	}
    
    protected override void Reset () {
        _prevZ = PointController.Hand_Right.transform.position.z;
        _punchTimer = 0.0f;
    }
}
