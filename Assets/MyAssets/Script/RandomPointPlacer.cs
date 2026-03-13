using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomPointPlacer : MonoBehaviour
{
    [Header("Area Settings")]
    [Tooltip("Width of the rectangular placement area in metres")]
    public float areaWidth = 40f;

    [Tooltip("Height (depth) of the rectangular placement area in metres")]
    public float areaHeight = 40f;

    [Tooltip("World-space origin (bottom-left corner) of the area")]
    public Vector3 areaOrigin = Vector3.zero;

    [Header("Point Settings")]
    [Tooltip("Total number of points to place")]
    public int targetPoint = 110;

    [Tooltip("Minimum distance (metres) between any two points")]
    public float minDistance = 3f;

    [Header("Sampling Settings")]
    [Tooltip("Candidate attempts per active point before giving up on it (30 is standard)")]
    public int maxSamplesbeforeReject = 30;

    private List<Vector3> placePoint = new List<Vector3>();

    public void SetUpWorldPoint(Vector3 origin, float width, float height)
    {
        areaOrigin = origin;
        areaWidth = width;
        areaHeight = height;
    }

    public List<Vector3> CalculatePlacePoint()
    {
        placePoint.Clear();
        List<Vector2> PDSamplePoints = PoissonDiskSample(areaWidth,areaHeight,minDistance,maxSamplesbeforeReject,targetPoint);

        if (PDSamplePoints.Count < targetPoint)
        {
            Debug.Log("Error cannot place all point in area");
        }
        else
        {
            Debug.Log("Successfully place all point");
        }

        foreach (Vector2 p in PDSamplePoints)
        {
            placePoint.Add(areaOrigin + new Vector3(p.x,0f,p.y));
        }
        return placePoint;
    }

    /// <summary>
    /// Generates up to <paramref name="maxPoints"/> 2-D samples inside a
    /// [0, width] x [0, height] rectangle with a guaranteed minimum separation
    /// of <paramref name="radius"/> between any two samples.
    /// </summary>
    /// 

    private List<Vector2> points;
    private List<Vector2> active;
    private int[] grid;
    private List<Vector2> PoissonDiskSample(float width, float height,float radius,int samplesBeforeRejection,int maxPoints)
    {
        // Cell size guarantees at most one sample per grid cell
        float cellSize = radius / Mathf.Sqrt(2f);
        int cols = Mathf.CeilToInt(width / cellSize);
        int rows = Mathf.CeilToInt(height / cellSize);

        // Background grid: stores index+1 into 'points' (0 = empty)
        grid = new int[cols * rows];

        points = new List<Vector2>();
        active = new List<Vector2>();

        // Seed with one random point
        Vector2 seed = new Vector2(Random.Range(0f, width),Random.Range(0f, height));
        AddSample(seed,cols,cellSize);



        while (active.Count > 0 && points.Count < maxPoints)
        {
            // Pick a random active point
            int idx = Random.Range(0, active.Count);
            Vector2 origin = active[idx];
            bool found = false;

            for (int s = 0; s < samplesBeforeRejection; s++)
            {
                // Sample in the annulus [radius, 2*radius]
                float angle = Random.value * Mathf.PI * 2f;
                float dist = Random.Range(radius, radius * 2f);
                Vector2 candidate = origin + new Vector2(
                    Mathf.Cos(angle) * dist,
                    Mathf.Sin(angle) * dist);

                if (IsValid(candidate, width, height, radius, cols, rows, cellSize))
                {
                    AddSample(candidate, cols, cellSize);
                    found = true;

                    if (points.Count >= maxPoints) break;
                }
            }

            if (!found)
                active.RemoveAt(idx); // Exhausted — retire this point
        }

        return points;

    }

    private void AddSample(Vector2 point, int cols, float cellSize)
    {
        points.Add(point);
        active.Add(point);

        int gx = (int)(point.x / cellSize);
        int gy = (int)(point.y / cellSize);
        grid[gy * cols + gx] = points.Count; // store 1-based index
    }

    private bool IsValid(Vector2 candidate,float width, float height,float radius, int cols, int rows, float cellSize)
    {
        // Reject if outside the area
        if (candidate.x < 0 || candidate.x >= width ||
            candidate.y < 0 || candidate.y >= height)
            return false;

        int gx = (int)(candidate.x / cellSize);
        int gy = (int)(candidate.y / cellSize);

        // Check the 5x5 neighbourhood of grid cells
        int xMin = Mathf.Max(0, gx - 2);
        int xMax = Mathf.Min(cols - 1, gx + 2);
        int yMin = Mathf.Max(0, gy - 2);
        int yMax = Mathf.Min(rows - 1, gy + 2);

        for (int y = yMin; y <= yMax; y++)
        {
            for (int x = xMin; x <= xMax; x++)
            {
                int storedIdx = grid[y * cols + x];
                if (storedIdx > 0)
                {
                    Vector2 neighbour = points[storedIdx - 1];
                    if ((candidate - neighbour).sqrMagnitude < radius * radius)
                        return false;
                }
            }
        }

        return true;
    }

}
