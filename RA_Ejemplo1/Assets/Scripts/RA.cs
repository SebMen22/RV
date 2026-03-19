using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RA : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager arTIM;
    [SerializeField] private GameObject[] arModels2Place;

    private Dictionary<string, GameObject> spawnedModels = new();
    private Dictionary<string, bool> modelState = new();

    void Awake()
    {
        foreach (var prefab in arModels2Place)
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.name = prefab.name;
            obj.SetActive(false);

            spawnedModels.Add(prefab.name, obj);
            modelState.Add(prefab.name, false);
        }
    }

    private void OnEnable()
    {
        if (arTIM != null)
            arTIM.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        if (arTIM != null)
            arTIM.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var img in args.added)
            UpdateImage(img);

        foreach (var img in args.updated)
            UpdateImage(img);

        foreach (var img in args.removed)
            DisableModel(img);
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        if (!spawnedModels.ContainsKey(name))
            return;

        var model = spawnedModels[name];

        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            model.SetActive(true);
            model.transform.SetPositionAndRotation(
                trackedImage.transform.position,
                trackedImage.transform.rotation
            );
            modelState[name] = true;
        }
        else
        {
            model.SetActive(false);
            modelState[name] = false;
        }
    }

    private void DisableModel(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        if (!spawnedModels.ContainsKey(name))
            return;

        spawnedModels[name].SetActive(false);
        modelState[name] = false;
    }
}