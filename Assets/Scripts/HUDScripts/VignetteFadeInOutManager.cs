using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteFadeInOutManager : MonoBehaviour {


    public VignetteFader vignetteFader;
    public GameObject player;
    float previous = 0;
    float velocity;
 
    void Update()
    {
        velocity = CalculateRotationVelocity();
        previous = Mathf.Abs(player.transform.rotation.y);
        FadeVignette();
    }

    float CalculateRotationVelocity()
    {
        float currentY = Mathf.Abs(player.transform.rotation.y);
        float vel = Mathf.Abs((currentY - previous) / Time.deltaTime);
        return vel;
    }
    void FadeVignette()
    {
        if (velocity > 0.05)
        {
            vignetteFader.FadeIn(0.02f);
        }
        else if (velocity < 0.02)
        {
            vignetteFader.FadeOut(0.3f);
        }
    }
}
