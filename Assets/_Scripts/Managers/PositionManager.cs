using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    [SerializeField] public List<float> obstaclePositions;
    [SerializeField] public List<Vector3> muzzlePositions;
    [SerializeField] public List<Vector3> movingObstaclePositions;

    [SerializeField] private Vector3 startX;
    [SerializeField] private Vector3 startZ;

    public static PositionManager instance;
    private void Awake()
    {
        instance = this;

        muzzlePositions = new List<Vector3>();

        muzzlePositions = InitializeMuzzlePositions();

        obstaclePositions = InitializeObstaclePositions();

        movingObstaclePositions = InitializeMovingObstaclePositions();
    }

    private List<Vector3> InitializeMuzzlePositions()
    {
        Vector3 currentPos = new Vector3(-2.75f, 1, -345);
        Vector3 posX = new Vector3(2.75f, 0, 0);
        Vector3 posZ = new Vector3(0, 0, 2);
        Vector3 newArea = new Vector3(0, 0, 10);
        List<Vector3> blocks = new List<Vector3>();
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    blocks.Add(currentPos);
                    currentPos = currentPos + posX;
                }
                posX = startX;
                currentPos = new Vector3(-2.75f, 1, currentPos.z) + posZ;
            }
            posZ = startZ;
            currentPos = currentPos + newArea;
        }
        return blocks;
    }

    private List<float> InitializeObstaclePositions()
    {
        List<float> result = new List<float>();
        float newPos = 0;

        for (int i = -9; i <= 9; i++)
        {
            newPos = i * 50;
            result.Add(newPos);
        }

        return result;
    }

    private List<Vector3> InitializeMovingObstaclePositions()
    {
        List<Vector3> result = new List<Vector3>();

        Vector3 firstPosition = new Vector3(-4.25f, 3.75f, -350f);
        Vector3 increasedPosition = new Vector3(0, 0, 10);

        for (int i = 0; i < 95; i++)
        {
            firstPosition += increasedPosition;
            result.Add(firstPosition);
        }

        return result;
    }

    public Vector3 GiveBlockPosition()
    {
        Vector3 newPos = muzzlePositions[0];

        muzzlePositions.RemoveAt(0);

        return newPos;
    }

    public float GiveObstaclePosition(int order)
    {
        if (order < 0 || order >= obstaclePositions.Count)
        {
            return 0;
        }
        else
        {
            float newPos = obstaclePositions[order];
            return newPos;
        }
    }

    public Vector3 GiveMovingObstaclePosition()
    {
        Debug.Log("give mov obs pos çalýþtý");
        Vector3 newPos = movingObstaclePositions[0];

        if (newPos.z % 50  == 0)
        {
            movingObstaclePositions.RemoveAt(0);

            newPos = movingObstaclePositions[0];
        }

        movingObstaclePositions.RemoveAt(0);

        return newPos;
    }
}