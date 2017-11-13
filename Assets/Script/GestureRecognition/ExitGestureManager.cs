using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGestureManager : GestureRecognizedDisplayer {

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    protected override void HandleGestureRecognized(object sender, EventArgs e)
    {
        Exit();
    }
}
