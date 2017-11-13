using UnityEngine;
using System.Collections;

public class LeftSwipeRecognizer : GestureRecognizer {
    public float MaxGestureDuration = 1.0f;
    public float MinSwipeDuration = 0.2f;
    public float MinSwipeSpeed = 1.0f;
    
    protected float _prevX = 0.0f;
    protected float _lastDirectSwipeTimer = 0.0f;
    protected float _directSwipeTimer = 0.0f;
    protected float _reverseSwipeTimer = 0.0f;
	
	// Update is called once per frame
	protected override void UpdateRecognizer () {
        if (_lastDirectSwipeTimer > 0.0f) {
            _lastDirectSwipeTimer -= Time.deltaTime;
            
            if (_lastDirectSwipeTimer <= 0.0f) {
                _lastDirectSwipeTimer = 0.0f;
                
                Reset();
            }
        }
        
        float currentX = PointController.Hand_Right.transform.position.x;
        float swipeSpeed = (currentX - _prevX) / Time.deltaTime;
        
        if (swipeSpeed <= -MinSwipeSpeed) {
            _directSwipeTimer += Time.deltaTime;
            
            if (_directSwipeTimer >= MinSwipeDuration) {
                _directSwipeTimer = MinSwipeDuration;
                _lastDirectSwipeTimer = MaxGestureDuration;
            }
        } else if (swipeSpeed >= MinSwipeSpeed && _directSwipeTimer >= MinSwipeDuration) {
            _reverseSwipeTimer += Time.deltaTime;
            
            if (_reverseSwipeTimer >= MinSwipeDuration) {
                _reverseSwipeTimer = MinSwipeDuration;
                
                if (_lastDirectSwipeTimer > 0.0f) {
                    GestureRecognized();
                    
                    Debug.Log("Left Swipe Recognized.");
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
        
        _prevX = currentX;
	}
    
    protected override void Reset () {
        _prevX = PointController.Hand_Right.transform.position.x;
        _lastDirectSwipeTimer = 0.0f;
        _directSwipeTimer = 0.0f;
        _reverseSwipeTimer = 0.0f;
    }
}
