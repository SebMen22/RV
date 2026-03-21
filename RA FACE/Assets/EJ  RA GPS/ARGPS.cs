using UnityEngine;
using System.Collections;
using UnityEngine.Android;
using Unity.XR.CoreUtils;
using System.Runtime.InteropServices;

public class ARGPS : MonoBehaviour
{
    public XROrigin sessionOrigin;
    public GameObject prefab;

    int maxWait = 20;

    private bool gpsEnabled = false;

    public double latitude;
    public double longitude;
    public double altitude;

    void Start()
    {
        StartCoroutine(UpdateGPS());
    }

    private void Awake()
    {
        if (!Application.isEditor) 
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                Permission.RequestUserPermission(Permission.FineLocation);

            if(!Permission.HasUserAuthorizedPermission(Permission.Camera))
                Permission.RequestUserPermission(Permission.Camera);
        }
    }
    IEnumerator UpdateGPS() 
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Servicio de Locacion:" + Input.location.isEnabledByUser);
            yield break;
        }
        else 
        {
            Debug.Log("Servicio de Locacion:" + Input.location.isEnabledByUser);
        }

        Input.location.Start();

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) 
        {
            yield return new WaitForSeconds(1);
            maxWait--;  
        }

        if (maxWait <= 0) 
        {
            Debug.Log("Inicializacion GPS por fuera del tiempo Limite");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Imposible determinar la posicion del dispositivo");
            yield break;
        }
        else 
        {
            gpsEnabled = true;

            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;
            
        }
        if (gpsEnabled) 
        {
            Vector3 position = sessionOrigin.transform.position;
            position.x = (float)longitude;
            position.y = 0;
            position.z = (float)latitude;

            prefab.transform.position = position;

            Vector3 forward = sessionOrigin.transform.forward;  
            forward.y = 0;
            Quaternion rotation = Quaternion.LookRotation(forward);
            prefab.transform.rotation = rotation;

            Input.location.Stop();

        }
        
    }

}
