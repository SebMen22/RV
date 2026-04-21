using UnityEngine;
using UnityEngine.Android;

public class CameraPermission : MonoBehaviour
{
    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Debug.Log("Permiso concedido");
            }
            else
            {
                Debug.Log("Permiso denegado");
            }
        }
    }
}