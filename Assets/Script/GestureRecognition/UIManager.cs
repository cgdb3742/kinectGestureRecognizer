using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public void StartKinect()
    {
        SceneManager.LoadScene("KinectSample");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
