using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class TouchToPlace : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject prefabObjeto;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject objetoInstanciado;

    void Update()
    {
        // Si no hay toque → salir
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // Solo cuando empieza el toque
        if (touch.phase != TouchPhase.Began) return;

        // Raycast SOLO en planos reales
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;

            // Mantener el objeto recto en el suelo
            Quaternion rotacion = Quaternion.Euler(0, pose.rotation.eulerAngles.y, 0);

            // Instanciar o mover
            if (objetoInstanciado == null)
            {
                objetoInstanciado = Instantiate(prefabObjeto, pose.position, rotacion);
            }
            else
            {
                objetoInstanciado.transform.SetPositionAndRotation(pose.position, rotacion);
            }
        }
    }
}