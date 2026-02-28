using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPlayer : MonoBehaviour
{
    public float velocidad = 6f;
    public float salto = 8f;

    private Rigidbody2D rb;
    private bool enSuelo;
    private float movimiento;

   
    private AudioSource audioPasos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

 
        audioPasos = GetComponent<AudioSource>();
    }

    void Update()
    {
        movimiento = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            movimiento = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            movimiento = 1f;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, salto);
        }

        // 🔊 NUEVO — CONTROLAR SONIDO DE PASOS
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f && enSuelo)
        {
            if (!audioPasos.isPlaying)
                audioPasos.Play();
        }
        else
        {
            if (audioPasos.isPlaying)
                audioPasos.Stop();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimiento * velocidad, rb.linearVelocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            enSuelo = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            enSuelo = false;
        }
    }
}