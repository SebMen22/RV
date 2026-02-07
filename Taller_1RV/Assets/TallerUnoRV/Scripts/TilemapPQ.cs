using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class TilemapPQ : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null) return;

                           
        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());  //lee la pos del mouse.---------------------------------
        Vector3Int cellPos = tilemap.WorldToCell(mousePos); 

        
        if (Mouse.current.leftButton.wasPressedThisFrame)  //con el click izq se colocan bloques del tilemapTerreno unicamente.---------------------------------
        {
            tilemap.SetTile(cellPos, tile);
        }

        
        if (Mouse.current.rightButton.wasPressedThisFrame)    //con el click derch  se eliminan los bloques del tilemapTerreno unicamente.--------------------------
        {
            tilemap.SetTile(cellPos, null);
        }
    }
}
