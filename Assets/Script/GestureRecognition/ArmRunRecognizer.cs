using UnityEngine;
using System.Collections;

public class ArmRunRecognizer : GestureRecognizer {
    public float MaxGestureDuration = 1.0f;
    public float MinRunDuration = 0.2f;
    public float MinRunSpeed = 15.0f;
    
    protected float _prevLeftArmRot = 0.0f;
    protected float _prevRightArmRot = 0.0f;
    protected float _prevLeftForearmRot = 0.0f;
    protected float _prevRightForearmRot = 0.0f;
    protected float _lastDirectRunTimer = 0.0f;
    protected float _directRunTimer = 0.0f;
    protected float _reverseRunTimer = 0.0f;
	
	// Update is called once per frame
	protected override void UpdateRecognizer () {
        if (_lastDirectRunTimer > 0.0f) {
            _lastDirectRunTimer -= Time.deltaTime;
            
            if (_lastDirectRunTimer <= 0.0f) {
                _lastDirectRunTimer = 0.0f;
                
                Reset();
            }
        }
        
        float currentLeftArmRot = Mathf.Repeat(ModelController.Shoulder_Left.transform.localEulerAngles.y + 180.0f, 360.0f) - 180.0f;
        float currentRightArmRot = Mathf.Repeat(ModelController.Shoulder_Right.transform.localEulerAngles.y + 180.0f, 360.0f) - 180.0f;
        float currentLeftForearmRot = Mathf.Repeat(ModelController.Elbow_Left.transform.localEulerAngles.y + 180.0f, 360.0f) - 180.0f;
        float currentRightForearmRot = Mathf.Repeat(ModelController.Elbow_Right.transform.localEulerAngles.y + 180.0f, 360.0f) - 180.0f;
        float runLeftArmSpeed = (currentLeftArmRot - _prevLeftArmRot) / Time.deltaTime;
        float runRightArmSpeed = (currentRightArmRot - _prevRightArmRot) / Time.deltaTime;
        float runLeftForearmSpeed = (currentLeftForearmRot - _prevLeftForearmRot) / Time.deltaTime;
        float runRightForearmSpeed = (currentRightForearmRot - _prevRightForearmRot) / Time.deltaTime;
        
        float runLeftMinSpeed = Mathf.Min(runLeftArmSpeed, runLeftForearmSpeed);
        float runLeftMaxSpeed = Mathf.Max(runLeftArmSpeed, runLeftForearmSpeed);
        float runRightMinSpeed = Mathf.Min(runRightArmSpeed, runRightForearmSpeed);
        float runRightMaxSpeed = Mathf.Max(runRightArmSpeed, runRightForearmSpeed);
        
        if (runLeftMaxSpeed >= MinRunSpeed && runRightMinSpeed <= -MinRunSpeed) {
            //Debug.Log("Direct");
            
            _directRunTimer += Time.deltaTime;
            
            if (_directRunTimer >= MinRunDuration) {
                _directRunTimer = MinRunDuration;
                _lastDirectRunTimer = MaxGestureDuration;
            }
        } else if (runLeftMinSpeed <= -MinRunSpeed && runRightMaxSpeed >= MinRunSpeed && _directRunTimer >= MinRunDuration) {
            //Debug.Log("Reverse");
            
            _reverseRunTimer += Time.deltaTime;
            
            if (_reverseRunTimer >= MinRunDuration) {
                _reverseRunTimer = MinRunDuration;
                
                if (_lastDirectRunTimer > 0.0f) {
                    GestureRecognized();
                    
                    Debug.Log("Arm Run Recognized.");
                }
            }
        } else {
            //Debug.Log("Reset");
            
            if (_lastDirectRunTimer <= 0.0f) {
                _directRunTimer -= Time.deltaTime;
                
                if (_directRunTimer <= 0.0f) {                
                    Reset();
                }
            }
        }
        
        _prevLeftArmRot = currentLeftArmRot;
        _prevRightArmRot = currentRightArmRot;
        _prevLeftForearmRot = currentLeftForearmRot;
        _prevRightForearmRot = currentRightForearmRot;
	}
    
    protected override void Reset () {
        _prevLeftArmRot = Mathf.Repeat(ModelController.Shoulder_Left.transform.localEulerAngles.y + 180.0f, 360.0f) - 180.0f;
        _prevRightArmRot = Mathf.Repeat(ModelController.Shoulder_Right.transform.localEulerAngles.y + 180.0f, 360.0f) - 180.0f;
        _prevLeftForearmRot = Mathf.Repeat(ModelController.Elbow_Left.transform.localEulerAngles.y + 180.0f, 360.0f) - 180.0f;
        _prevRightForearmRot = Mathf.Repeat(ModelController.Elbow_Right.transform.localEulerAngles.y + 180.0f, 360.0f) - 180.0f;
        _lastDirectRunTimer = 0.0f;
        _directRunTimer = 0.0f;
        _reverseRunTimer = 0.0f;
    }
}
