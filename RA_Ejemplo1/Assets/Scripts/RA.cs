using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RA : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager arTIM;
    [SerializeField] private GameObject[] arModels2Place;

    private Dictionary<string, GameObject> arModels = new Dictionary<string, GameObject>();
    private Dictionary<string, bool> modelState = new Dictionary<string, bool>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var arModel in arModels2Place)
        {
            GameObject newARModel = Instantiate(arModel, Vector3.zero, Quaternion.identity);
            newARModel.name = arModel.name;

            arModels.Add(arModel.name, newARModel);
            newARModel.SetActive(false);
            modelState.Add(arModel.name, false);
        }
    }

    private void OnEnable()
    {
        arTIM.trackedImagesChanged += ImageFound;
    }

    private void OnDisable()
    {
        arTIM.trackedImagesChanged -= ImageFound;
    }

    private void ImageFound(ARTrackedImagesChangedEventArgs evenData)
    {
        foreach (var trackedImage in evenData.added)
        {
            showARModel(trackedImage);
        }

        foreach (var trackedImage in evenData.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
                showARModel(trackedImage);
            else
                hideARModel(trackedImage);

        }
    }

    private void showARModel(ARTrackedImage trackedImage)
    {
        bool isModelActive = modelState[trackedImage.referenceImage.name];

        if (!isModelActive)
        {
            GameObject arModel = arModels[trackedImage.referenceImage.name];
            arModel.transform.position = trackedImage.transform.position;

            arModel.SetActive(true);
            modelState[trackedImage.referenceImage.name] = true;
        }
        else
        {
            GameObject arModel = arModels[trackedImage.referenceImage.name];
            arModel.transform.position = trackedImage.transform.position;
        }
    }

    private void hideARModel(ARTrackedImage trackedImage)
    {
        bool isModelActive = modelState[trackedImage.referenceImage.name];

        if (isModelActive)
        {
            GameObject arModel = arModels[trackedImage.referenceImage.name];
            arModel.SetActive(false);
            modelState[trackedImage.referenceImage.name] = false;
        }

    }

}