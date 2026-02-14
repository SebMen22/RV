using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour
{
    public void CargarJuego()
    {
        SceneManager.LoadScene("Juego");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}