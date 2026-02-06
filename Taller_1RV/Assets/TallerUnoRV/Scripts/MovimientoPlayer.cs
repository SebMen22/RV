using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPlayer : MonoBehaviour
{
    public float velocidad = 6f;
    public float fuerzaSalto = 8f;

    private Rigidbody2D rb;
    private bool enSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movimiento = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            movimiento = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            movimiento = 1f;

        rb.linearVelocity = new Vector2(movimiento * velocidad, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
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
