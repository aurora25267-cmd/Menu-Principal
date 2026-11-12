using UnityEngine;
using UnityEngine.SceneManagement; // Es obligatorio incluir esta librería para manejar escenas

public class CambiarEsena : MonoBehaviour
{
    // Método que llamaremos desde el botón de Jugar
    public void CargarEscenaBlanco()
    {
        // Reemplaza "EscenaBlanco" por el nombre exacto que le diste a tu nueva escena
        SceneManager.LoadScene("EscenaBlanco");
    }
}
