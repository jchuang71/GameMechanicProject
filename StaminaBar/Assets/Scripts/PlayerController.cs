using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public int stamina;
    public int maxStamina;
    public bool staminaExhausted;
    public Slider staminaBar;
    public float baseSpeed;
    public float speed;
    public bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        staminaBar.value = (float)stamina / maxStamina;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is colliding with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        staminaBar.value = (float)stamina / maxStamina;
        if (Input.GetKey(KeyCode.LeftShift) && !staminaExhausted)
        {
            if(speed == baseSpeed)
            {
                speed *= 2;
            }
            stamina -= 1;
            if (stamina <= 0)
            {
                staminaExhausted = true;
                speed /= 4;
                staminaBar.image.color = Color.red;
            }
        }
        else
        {
            if (stamina == maxStamina)
            {
                staminaExhausted = false;
                staminaBar.image.color = Color.yellow;
            }
            if (!staminaExhausted)
            {
                speed = baseSpeed;
            }
            if (stamina < maxStamina)
            {
                stamina += 1;
            }
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Jump using Rigidbody2D
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            }
        }
    }
}
