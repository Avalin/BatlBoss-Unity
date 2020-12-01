using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerJoystickHandler : MonoBehaviour {

    SteamVR_Action_Boolean turnLeft;
    SteamVR_Action_Boolean turnRight;

    float smooth = 3f;
    float tiltAngle = 45f;
    float endY = 0;

    // Use this for initialization
    void Start() {
        turnLeft = SteamVR_Input._default.inActions.TurnLeft;
        turnRight = SteamVR_Input._default.inActions.TurnRight;
    }

    // Update is called once per frame
    void Update()
    {
        TiltPlayer();
    }

    void TiltPlayer()
    {
        // Smoothly tilts a transform towards a target rotation.
        if (turnLeft.GetStateDown(SteamVR_Input_Sources.Any))
            endY -= tiltAngle;

        if (turnRight.GetStateDown(SteamVR_Input_Sources.Any))
            endY += tiltAngle;

        Quaternion target = Quaternion.Euler(0, endY, 0);
        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
