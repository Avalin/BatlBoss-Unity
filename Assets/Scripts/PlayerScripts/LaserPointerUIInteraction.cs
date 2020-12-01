using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class LaserPointerUIInteraction : MonoBehaviour {

    LineRenderer laserPointer;
    Transform currentlyHighlightedButton;

	// Use this for initialization
	void Start ()
    {
        laserPointer = GetComponent<LineRenderer>();
	}
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        if (Physics.Raycast(laserPointer.transform.position, laserPointer.transform.TransformDirection(Vector3.forward), out hit))
        {
            CheckForHighlight(hit);
            CheckForButtonPress(hit);
        }
    }


    void HighlightHoveredButton(string buttonName, string buttonNameHovered, RaycastHit hit)
    {
        if (hit.transform.name.Equals(buttonName))
        {
            if (currentlyHighlightedButton != null && currentlyHighlightedButton.name != buttonNameHovered)
            {
                currentlyHighlightedButton.gameObject.SetActive(false);
            }

            foreach (Transform child in hit.transform.parent)
            {
                if (child.name == buttonNameHovered)
                {
                    currentlyHighlightedButton = child.transform;
                    currentlyHighlightedButton.gameObject.SetActive(true);

                }
            }
        }
    }

    void CheckForHighlight(RaycastHit hit)
    {
        if (hit.transform.tag.Equals("UIButton"))
        {
            HighlightHoveredButton("BtnPlayGame", "PlayGameHovered", hit);
            HighlightHoveredButton("BtnExitGame", "ExitGameHovered", hit);
        }
        else
        {
            if (currentlyHighlightedButton != null)
            {
                currentlyHighlightedButton.gameObject.SetActive(false);
            }
        }
    }

    void CheckForButtonPress(RaycastHit hit)
    {
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
        {
            GameObject pressedGO = hit.transform.gameObject;
            if (pressedGO.tag.Equals("UIButton"))
            {
                if (pressedGO.name.Equals("BtnPlayGame"))
                {
                    SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
                }

                if (pressedGO.name.Equals("BtnExitGame"))
                {
                    Application.Quit();
                    //Just for Unity exit
                    //UnityEditor.EditorApplication.isPlaying = false;
                }
            }
        }
    }
}
