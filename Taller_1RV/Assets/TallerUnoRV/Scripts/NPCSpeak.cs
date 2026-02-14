using UnityEngine;
using UnityEngine.InputSystem;

public class NPCSpeak : MonoBehaviour
{
    public GameObject panelE;
    public GameObject panelDialogo;

    public Vector3 offset = new Vector3(0.26f, 0);

    private bool playerEnRango = false;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (panelE != null)
            panelE.SetActive(false);

        if (panelDialogo != null)
            panelDialogo.SetActive(false);
    }

    void Update()
    {
        if (!playerEnRango) return;

        
        if (panelE != null)
        {
            Vector3 posicionPantalla = cam.WorldToScreenPoint(transform.position + offset);
            panelE.transform.position = posicionPantalla; //------------------------------------ mueve exactamente el panelE a la posicion del NPC, con un offset para que no quede encima del NPC.
        }

        
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (panelDialogo != null)
            {
                bool estado = panelDialogo.activeSelf;
                panelDialogo.SetActive(!estado);

                if (panelE != null)
                    panelE.SetActive(estado);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnRango = true;

            if (panelE != null)
                panelE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEnRango = false;

            if (panelE != null)
                panelE.SetActive(false);

            if (panelDialogo != null)
                panelDialogo.SetActive(false);
        }
    }
}
