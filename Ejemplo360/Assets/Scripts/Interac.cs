using UnityEngine;
using UnityEngine.InputSystem;

public class Interac : MonoBehaviour
{
    private NIS inputActions;

    public float distancia = 5f;

    void Awake()
    {
        inputActions = new NIS();
    }

    void OnEnable()
    {
        inputActions.View.Enable();
    }

    void OnDisable()
    {
        inputActions.View.Disable();
    }

    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * distancia, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, distancia))
        {
            if (inputActions.View.Click.triggered)
            {
                // Buscar el script correcto
                InteracPunto interact = hit.collider.GetComponent<InteracPunto>();

                if (interact != null)
                {
                    // Toggle abrir / cerrar
                    bool estaActivo = interact.Panel.activeSelf;
                    interact.Panel.SetActive(!estaActivo);
                }
            }
        }
    }
}