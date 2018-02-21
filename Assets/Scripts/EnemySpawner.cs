using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float formationSpeed;
    public float gizmoWidth;
    public float gizmoHeight;
    public float paddingWidth;

    private bool movingRight = true;
    private float xMin;
    private float xMax;


    // Use this for initialization
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xMin = screenLeft.x + paddingWidth;
        xMax = screenRight.x - paddingWidth;

        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, position: child.position, rotation: Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gizmoWidth, gizmoHeight, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * formationSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * formationSpeed * Time.deltaTime;
        }

        if (transform.position.x <= xMin || transform.position.x >= xMax)
        {
            movingRight = !movingRight;
        }

        float xNew = Mathf.Clamp(transform.position.x, xMin, xMax);

        transform.position = new Vector3(xNew, transform.position.y, transform.position.z);

    }
}
