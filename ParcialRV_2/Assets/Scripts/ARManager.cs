using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ARManager : MonoBehaviour
{
    [Header("AR")]
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    [Header("UI")]
    public Button startButton;

    [Header("Prefab")]
    public GameObject deathStarPrefab;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool planeDetected = false;
    private bool placed = false;

    private GameObject spawnedObject;

    public float scaleAR = 0.003f;

    void Start()
    {
        startButton.interactable = false;
        startButton.onClick.AddListener(PlaceObject);
    }

    void Update()
    {
        DetectPlanes();
    }

    void DetectPlanes()
    {
        if (planeDetected) return;

        foreach (var plane in planeManager.trackables)
        {
            // 🔥 SOLO PLANOS HORIZONTALES (SUELO)
            if (plane.trackingState == TrackingState.Tracking &&
                plane.alignment == PlaneAlignment.HorizontalUp)
            {
                planeDetected = true;
                startButton.interactable = true;

                Debug.Log("SUELO DETECTADO");
                return;
            }
        }
    }

    void PlaceObject()
    {
        if (placed) return;

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

        // 🔥 SOLO RAYCAST A PLANOS HORIZONTALES
        if (!raycastManager.Raycast(center, hits, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("NO HAY SUELO EN EL CENTRO");
            return;
        }

        Pose pose = hits[0].pose;

        StartCoroutine(SpawnStable(pose));
    }

    IEnumerator SpawnStable(Pose pose)
    {
        yield return new WaitForSeconds(0.2f);

        // 📏 Separar un poco del usuario
        pose.position += Camera.main.transform.forward * 0.3f;

        // 🔒 Rotación fija (sin inclinaciones)
        Quaternion fixedRotation = Quaternion.Euler(0, 0, 0);

        // 🚀 Instanciar en el mundo (NO como hijo)
        spawnedObject = Instantiate(deathStarPrefab);
        spawnedObject.transform.position = pose.position;
        spawnedObject.transform.rotation = fixedRotation;
        spawnedObject.transform.localScale = Vector3.one * scaleAR;

        // 👁️ Ocultar planos
        foreach (var p in planeManager.trackables)
        {
            p.gameObject.SetActive(false);
        }

        // 🛑 DETENER COMPLETAMENTE EL AR SCAN
        planeManager.enabled = false;
        raycastManager.enabled = false;

        placed = true;

        Debug.Log("OBJETO FIJO EN SUELO");
    }
}