using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float paddingWidth;
    public float paddingHeight;
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate = 0.2f;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    // Use this for initialization
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Vector3 screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 screenBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));

        xMin = screenLeft.x + paddingWidth;
        xMax = screenRight.x - paddingWidth;
        yMin = screenBottom.y + paddingHeight;
        yMax = screenTop.y - paddingHeight;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            //transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            //transform.position += new Vector3(0f, -speed * Time.deltaTime, 0f);
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            //transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            //transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        float xNew = Mathf.Clamp(transform.position.x, xMin, xMax);
        float yNew = Mathf.Clamp(transform.position.y, yMin, yMax);

        transform.position = new Vector3(xNew, yNew, 0f);

        if (Input.GetKeyDown("space"))
        {
            InvokeRepeating("Fire", 0.000001f, fireRate);

        }

        if (Input.GetKeyUp("space"))
        {
            CancelInvoke("Fire");
        }

    }

    void Fire()
    {
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, projectileSpeed, 0f);
    }
}