using UnityEngine;

public class NPCSpeak : MonoBehaviour
{
    public GameObject canvasInteraccion;

    void Start()
    {
        
        canvasInteraccion.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasInteraccion.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasInteraccion.SetActive(false);
        }
    }
}