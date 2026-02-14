using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using TMPro;

public class TilemapPQ : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase bloque;

    public int limiteB = 10;
    private int bloquesD;

    public TextMeshProUGUI textoBloques; 

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        bloquesD = limiteB;
        ActualizarUI();
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        mouseScreen.z = Mathf.Abs(cam.transform.position.z);
        Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorld);

        //------------------------------------------------------------------- COLOCAR BLOQUE con click izq segun la posicion en la q se encuentre el mouse.
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (bloquesD > 0 && !tilemap.HasTile(cellPos)) //----------------------------------------------Verificar que haya bloques disponibles y que no haya un bloque ya en esa celda.
            {
                tilemap.SetTile(cellPos, bloque);
                bloquesD--;
                ActualizarUI();
            }
        }

        //------------------------------------------------------ BORRAR BLOQUE con click derecho, si el mouse esta sobre un bloque, se borra y se suma a los bloques disponibles.
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (tilemap.HasTile(cellPos))
            {
                tilemap.SetTile(cellPos, null);

                if (bloquesD < limiteB)
                {
                    bloquesD++;
                    ActualizarUI();
                }
            }
        }
    }

    void ActualizarUI()
    {
        textoBloques.text = "Bloques: " + bloquesD;
    }
}
