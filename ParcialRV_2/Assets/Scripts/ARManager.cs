using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ARManager : MonoBehaviour
{
    [Header("AR SYSTEM")]
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    [Header("UI")]
    public Button startButton;

    [Header("PREFAB")]
    public GameObject deathStarPrefab;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool planeDetected = false;
    private bool objectPlaced = false;

    private GameObject spawnedObject;

    private float scaleAR = 0.02f;

    void Start()
    {
        startButton.interactable = false;
        startButton.onClick.AddListener(PlaceObject);
    }

    void Update()
    {
        DetectPlanes();
    }

    // 🔵 Detecta si ya hay planos en el mundo
    void DetectPlanes()
    {
        if (planeDetected) return;

        foreach (var plane in planeManager.trackables)
        {
            if (plane.trackingState == TrackingState.Tracking)
            {
                planeDetected = true;
                startButton.interactable = true;

                Debug.Log("PLANO DETECTADO → BOTÓN ACTIVADO");
                return;
            }
        }
    }

    // 🔴 Colocar objeto y fijarlo al mundo
    void PlaceObject()
    {
        if (objectPlaced) return;

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            // 🧠 CREAR ANCLA ESTABLE
            GameObject anchorObject = new GameObject("ARAnchor");
            anchorObject.transform.position = hitPose.position;
            anchorObject.transform.rotation = hitPose.rotation;

            ARAnchor anchor = anchorObject.AddComponent<ARAnchor>();

            if (anchor == null)
            {
                Debug.LogError("ERROR CREANDO ANCLA");
                return;
            }

            // 🚀 INSTANCIAR DEATH STAR COMO HIJA DEL ANCLA
            spawnedObject = Instantiate(deathStarPrefab, anchor.transform);
            spawnedObject.transform.localPosition = Vector3.zero;
            spawnedObject.transform.localRotation = Quaternion.identity;

            // 📏 ESCALA FINAL
            spawnedObject.transform.localScale = Vector3.one * scaleAR;

            // ❌ DETENER ESCANEO DE PLANOS
            planeManager.enabled = false;

            // ❌ OCULTAR PLANOS EXISTENTES
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

            objectPlaced = true;

            Debug.Log("DEATH STAR FIJA COLOCADA EN EL MUNDO");
        }
        else
        {
            Debug.Log("NO SE ENCONTRÓ PLANO PARA COLOCAR");
        }
    }
}