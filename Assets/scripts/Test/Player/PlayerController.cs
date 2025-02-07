using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CameraFollow cameraFollow; 
    public float moveSpeed = 25f; 
    public float jumpForce = 23f; 
    private bool isGrounded = true; 
    private bool isOnWall = false; 
    private Collision2D lastCollision; 


    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject); 

    }


    void Update()
    {

        if(true)
        {
            float moveInput = Input.GetAxis("Horizontal"); 
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            if (moveInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); 
            }
            else if (moveInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); 
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false; 
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y < transform.position.y)
                {
                    isGrounded = true;
                    isOnWall = false;
                    Debug.Log("角色接触到: " + collision.gameObject.name);
                    break; 
                }
            }
        }
        if(collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = false;
            Debug.Log("角色离开了墙: " + collision.gameObject.name);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {

            Debug.Log("角色离开了地面: " + collision.gameObject.name);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawnPoint != null)
        {

            transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0);
            Debug.Log($"在场景 '{scene.name}' 中找到了 SpawnPoint，并设置了角色位置。");
            Debug.Log($"角色位置设置为: {transform.position}");
        }
        else
        {
            Debug.Log($"在场景 '{scene.name}' 中未找到 SpawnPoint。");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("角色触发到: " + other.gameObject.name); 
    }


} 