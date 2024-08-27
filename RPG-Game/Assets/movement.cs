using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 100f; // player speed
    public float sprint = 200f; // player sprint speed
    public float stamina = 100f; // Current stamina
    public float maxStamina = 100f; // Maximum stamina
    public float staminaRegen = 5f; // Stamina regeneration rate per second
    public float staminaCostPerSecond = 15f; // Stamina cost per second while sprinting

    private Coroutine regenCoroutine;
    public Slider staminaBar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        
        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = stamina;
        }
    }

    void Update()
    {
     
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            if (movement != Vector2.zero)
            {
                if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
                {
                    rb.velocity = movement * sprint;
                    UseStamina(staminaCostPerSecond * Time.deltaTime);
                }
                else
                {
                    rb.velocity = movement * speed;

                   
                    if (stamina < maxStamina && regenCoroutine == null)
                    {
                        regenCoroutine = StartCoroutine(RegenerateStamina());
                    }
                }
            }
            else
            {
                rb.velocity = Vector2.zero;

           
                if (stamina < maxStamina && regenCoroutine == null)
                {
                    regenCoroutine = StartCoroutine(RegenerateStamina());
                }
            }
        
    }

    private void UseStamina(float amount)
    {
        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
            regenCoroutine = null;
        }

        stamina -= amount;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        if (staminaBar != null)
        {
            staminaBar.value = stamina;
        }
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(1f); 

        while (stamina < maxStamina)
        {
            stamina += staminaRegen * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);

            if (staminaBar != null)
            {
                staminaBar.value = stamina;
            }

            yield return null;
        }

        regenCoroutine = null;
    }
}
