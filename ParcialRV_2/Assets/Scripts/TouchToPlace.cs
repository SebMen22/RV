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
        // Detectar toque
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // Solo en el primer toque
        if (touch.phase != TouchPhase.Began) return;

        // Raycast contra planos detectados
        if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose pose = hits[0].pose;

            // Si ya hay objeto, lo mueve
            if (objetoInstanciado == null)
            {
                objetoInstanciado = Instantiate(prefabObjeto, pose.position, pose.rotation);
            }
            else
            {
                objetoInstanciado.transform.SetPositionAndRotation(pose.position, pose.rotation);
            }
        }
    }
}