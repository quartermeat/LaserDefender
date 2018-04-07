using System.Collections;
using System.Collections.Generic;
using Assets.Entities.Enemy;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool _movingRight = true;
    private float _xMin;
    private float _xMax;

    public float SpawnsPerSecond = 0.75f;
    public GameObject Enemy;
    public float Width = 10f;
    public float Height = 5f;
    public float Speed = 5f;

    // Use this for initialization
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        _xMin = leftMost.x;
        _xMax = rightMost.x;

        //SpawnEnemies();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Width,Height));
    }

    // Update is called once per frame
    void Update()
    {
        if (_movingRight)
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
        }

        float rightEdge = transform.position.x + (0.5f * Width);
        float leftEdge = transform.position.x - (0.5f * Width);

        if (_movingRight && rightEdge > _xMax)
        {
            _movingRight = !_movingRight;
        }
        else if (!_movingRight && leftEdge < _xMin)
        {
            _movingRight = !_movingRight;
        }

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        Transform newFreePosition = NextFreePosition();
        if (newFreePosition)
        {
            float probability = Time.deltaTime * SpawnsPerSecond;
            if (Random.value < probability)
            {
                GameObject enemy = Instantiate(Enemy, newFreePosition.position, Quaternion.identity);
                enemy.transform.parent = newFreePosition;
            }
        }
    }

    private bool AllMembersDead()
    {
        foreach (Transform currentTransform in transform)
        {
            if (currentTransform.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    private Transform NextFreePosition()
    {
        foreach (Transform currentTransform in transform)
        {
            if (currentTransform.childCount <= 0)
            {
                return currentTransform;
            }
        }
        return null;
    }
}
