using UnityEngine;
using System.Collections;

public class UpSwipeRecognizer : GestureRecognizer {
    public float MaxGestureDuration = 1.0f;
    public float MinSwipeDuration = 0.2f;
    public float MinSwipeSpeed = 1.0f;
    
    protected float _prevLeftY = 0.0f;
    protected float _prevRightY = 0.0f;
    protected float _lastDirectSwipeTimer = 0.0f;
    protected float _directSwipeTimer = 0.0f;
    protected float _reverseSwipeTimer = 0.0f;
	
	// Update is called once per frame
	protected override void Update () {
        if (_lastDirectSwipeTimer > 0.0f) {
            _lastDirectSwipeTimer -= Time.deltaTime;
            
            if (_lastDirectSwipeTimer <= 0.0f) {
                _lastDirectSwipeTimer = 0.0f;
                
                Reset();
            }
        }
        
        float currentLeftY = PointController.Hand_Left.transform.position.y;
        float currentRightY = PointController.Hand_Right.transform.position.y;
        float swipeLeftSpeed = (currentLeftY - _prevLeftY) / Time.deltaTime;
        float swipeRightSpeed = (currentRightY - _prevRightY) / Time.deltaTime;
        
        if (swipeLeftSpeed >= MinSwipeSpeed && swipeRightSpeed >= MinSwipeSpeed) {
            _directSwipeTimer += Time.deltaTime;
            
            if (_directSwipeTimer >= MinSwipeDuration) {
                _directSwipeTimer = MinSwipeDuration;
                _lastDirectSwipeTimer = MaxGestureDuration;
            }
        } else if (swipeLeftSpeed <= -MinSwipeSpeed && swipeRightSpeed <= -MinSwipeSpeed && _directSwipeTimer >= MinSwipeDuration) {
            _reverseSwipeTimer += Time.deltaTime;
            
            if (_reverseSwipeTimer >= MinSwipeDuration) {
                _reverseSwipeTimer = MinSwipeDuration;
                
                if (_lastDirectSwipeTimer > 0.0f) {
                    GestureRecognized();
                    
                    Debug.Log("Up Swipe Recognized.");
                }
            }
        } else {
            if (_lastDirectSwipeTimer <= 0.0f) {
                _directSwipeTimer -= Time.deltaTime;
                
                if (_directSwipeTimer <= 0.0f) {                
                    Reset();
                }
            }
        }
        
        _prevLeftY = currentLeftY;
        _prevRightY = currentRightY;
	}
    
    protected override void Reset () {
        _prevLeftY = PointController.Hand_Left.transform.position.y;
        _prevRightY = PointController.Hand_Right.transform.position.y;
        _lastDirectSwipeTimer = 0.0f;
        _directSwipeTimer = 0.0f;
        _reverseSwipeTimer = 0.0f;
    }
}
