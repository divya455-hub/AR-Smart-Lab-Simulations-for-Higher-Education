using UnityEngine;
using System.Collections.Generic;

public class DropAllBalls : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> startPositions = new Dictionary<GameObject, Vector3>();

    void Start()
    {
        // Store initial positions of all balls
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            startPositions[ball] = ball.transform.position;
        }
    }

    public void DropBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }

    public void ResetBalls()
    {
        foreach (var pair in startPositions)
        {
            GameObject ball = pair.Key;
            Vector3 startPos = pair.Value;

            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            ball.transform.position = startPos;

            // Reset timer if ball has one
            BallTimer bt = ball.GetComponent<BallTimer>();
            if (bt != null)
            {
                bt.ResetTimer();
            }
        }
    }
}
