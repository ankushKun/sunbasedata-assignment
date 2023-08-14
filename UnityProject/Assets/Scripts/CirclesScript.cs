using System.Collections.Generic;
using UnityEngine;

public class CirclesScript : MonoBehaviour
{
    // Minimum and maximum circles to spawn
    int minP = 5, maxP = 10;

    // X and Z position range to spawn the circles in, 
    // so that they dont spawn outside the camera range
    int Xrange = 8, Zrange = 4;

    // Fixed Y position for every circle
    int fixedY = 5;

    // Parent gameobejct that contains all Circles
    public GameObject CirclesContainer;

    public GameObject CirclePrefab;
    public LineRenderer line;

    Vector3 previousMousePosition;
    List<Vector3> linePoints = new List<Vector3>();

    bool drawn = false;
    bool reset = false;

    // This layer will keep track of which circles are colliding with the drawn line
    public LayerMask circleLayer;

    public void spawnRandomCircles()
    {
        previousMousePosition = new Vector3(0, 0, 0);
        linePoints.Clear();
        line.positionCount = 0;
        reset = true;

        // Clear out all previous circles
        foreach (Transform child in CirclesContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Generate a random quantity to spawn
        int numPoints = Random.Range(minP, maxP + 1);
        Vector3[] points = new Vector3[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            // Random position to spawn the circle
            Vector3 p = new Vector3(Random.Range(-Xrange, Xrange), fixedY, Random.Range(-Zrange, Zrange));
            GameObject spawnedCircle = Instantiate(CirclePrefab);
            // Change parent to the Circle container, for easy finding in future
            spawnedCircle.transform.parent = CirclesContainer.transform;
            spawnedCircle.transform.position = p;
            // spawnedCircle.layer = 6; // No longer needed here as we are assigning layer during drawing of line
        }

    }

    void Start()
    {
        spawnRandomCircles();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))// As long as mouse is being clicked draw line and check if it collides with a circle using Raycasts
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((int)previousMousePosition.x != 0 && (int)previousMousePosition.y != 0)
            {
                float distance = Vector3.Distance(previousMousePosition, currentMousePosition);
                if (!drawn && distance > 0) // Draw lien only if mouse position on screen changes
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.name.Contains("Cylinder")) // If raycast hits a circle out it in the circle layer
                        {
                            Debug.Log(hit.transform.name);
                            hit.transform.gameObject.layer = 6;
                        }
                    }

                    linePoints.Add(currentMousePosition);
                    // Debug.Log(distance);
                    line.positionCount = linePoints.Count;
                    line.SetPosition(linePoints.Count - 1, currentMousePosition);
                }
                if (reset)
                {
                    drawn = false;
                    reset = false;
                }
            }
            previousMousePosition = currentMousePosition;
        }
        if (Input.GetMouseButtonUp(0)) // When player has finished drawing line
        {
            drawn = true;
            foreach (Transform child in CirclesContainer.transform)
            {
                if (child.gameObject.layer == 6)
                {
                    Destroy(child.gameObject);
                }
            }

        }
    }
}
