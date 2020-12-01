using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour {
    public GameObject throwPlaceholder;
    public Transform target;

    public GameObject[] objectsToThrow = new GameObject[6];
    public GameObject objectToSmash;

    Transform ball;



	void Update () {

        // is ball is instantiated and boss doesn't have a ball instantiated in his hand
		if(ball == null && throwPlaceholder.transform.childCount != 0)
        {
            ball = throwPlaceholder.transform.GetChild(0);
        }
	}

    void ThrowBall()
    {
        BallScript ballScript = ball.GetComponent<BallScript>();
        ballScript.ReleaseMe(target.position);
        ball = null;
    }

    public void InstantiateObjectToThrow()
    {
        int random = UnityEngine.Random.Range(0, 5);
        Instantiate(objectsToThrow[random], throwPlaceholder.transform.position, throwPlaceholder.transform.rotation, throwPlaceholder.transform);
    }

    public void InstantiateObjectToSmash()
    {
        Instantiate(objectToSmash, throwPlaceholder.transform.position, throwPlaceholder.transform.rotation, throwPlaceholder.transform);
    }

}
