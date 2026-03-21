using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
public class CambioCara : MonoBehaviour
{
    [SerializeField] private ARFaceManager faceManager;
    [SerializeField]  private List<Material> mascaras = new List<Material>();

    private int indexMascara = 0;

    private void Awake()
    {
        faceManager = FindFirstObjectByType <ARFaceManager>();
    }

    public void CambiarTextura()
    {
        foreach (var face in faceManager.trackables) 
        {
            var render = face.GetComponent<MeshRenderer>();

            GetComponent<Renderer>().sharedMaterial = mascaras[indexMascara];
        }

        indexMascara++;

        if (indexMascara == mascaras.Count) 
        {
            indexMascara = 0;
        }
    }


}
