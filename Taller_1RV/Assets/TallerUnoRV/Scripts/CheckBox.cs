using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject PanelFin;
    private bool terminado = false;

    void Start()
    {
        PanelFin.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (terminado) return;

        if (collision.gameObject.CompareTag("Meta"))
        {
            Debug.Log("GGs ni tan mal");
            terminado = true;
            PanelFin.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
