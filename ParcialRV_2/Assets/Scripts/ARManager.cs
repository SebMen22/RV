using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

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
    private ARAnchor currentAnchor;

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

    void DetectPlanes()
    {
        if (planeDetected) return;

        foreach (var plane in planeManager.trackables)
        {
            if (plane.trackingState == TrackingState.Tracking)
            {
                planeDetected = true;
                startButton.interactable = true;

                Debug.Log("PLANO DETECTADO");
                return;
            }
        }
    }

    void PlaceObject()
    {
        if (placed) return;

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

        if (!raycastManager.Raycast(center, hits, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("NO HAY PLANO EN CENTRO");
            return;
        }

        Pose pose = hits[0].pose;

        StartCoroutine(SpawnStable(pose));
    }

    IEnumerator SpawnStable(Pose pose)
    {
        // ⏳ esperar estabilidad de tracking AR
        yield return new WaitForSeconds(0.3f);

        // 🔵 CREAR ANCLA REAL
        GameObject anchorGO = new GameObject("ARAnchor");
        anchorGO.transform.SetPositionAndRotation(pose.position, pose.rotation);

        currentAnchor = anchorGO.AddComponent<ARAnchor>();

        if (currentAnchor == null)
        {
            Debug.LogError("ERROR CREANDO ANCLA");
            yield break;
        }

        // 🚀 INSTANCIAR DEATH STAR
        spawnedObject = Instantiate(deathStarPrefab, currentAnchor.transform);
        spawnedObject.transform.localPosition = Vector3.zero;
        spawnedObject.transform.localRotation = Quaternion.identity;
        spawnedObject.transform.localScale = Vector3.one * scaleAR;

        // ❌ DETENER ESCANEO DE PLANOS
        planeManager.enabled = false;

        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

        placed = true;

        Debug.Log("DEATH STAR FIJA Y ESTABLE");
    }
}