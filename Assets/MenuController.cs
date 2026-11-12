using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Paneles de la UI")]
    // mainPanel = 0, settingsPanel = 1, scoresPanel = 2, creditsPanel = 3
    public GameObject[] panels;

    [Header("Botones")]
    public Button[] menuButtons; // Botones principales: Jugar, Ajustes, Puntuaciones, Créditos, Salir
    public Button[] backButtons; // Botones para regresar al menú principal

    [Header("Componentes de Configuración")]
    public TMP_InputField playerNameField;
    public TMP_Dropdown qualityDropdown;
    public Slider brightnessSlider;
    public Toggle fullscreenToggle;

    [Header("Configuración de Escena")]
    public string gameSceneName;

    private void Start()
    {
        // Inicializar el estado de los componentes visuales con los valores actuales del sistema
        if (qualityDropdown != null)
        {
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.onValueChanged.AddListener(SetQuality);
        }

        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }

        // Mostrar por defecto solo el panel principal al arrancar
        ShowPanel(0);
    }

    // --- NAVEGACIÓN ENTRE PANELES ---
    public void ShowPanel(int panelIndex)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] != null)
            {
                panels[i].SetActive(i == panelIndex);
            }
        }
        PlayClickSound();
    }

    // --- ACCIONES DE BOTONES PRINCIPALES ---
    public void PlayGame()
    {
        PlayClickSound();
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            // Guarda el nombre del jugador si se necesita en la siguiente escena
            if (playerNameField != null && !string.IsNullOrEmpty(playerNameField.text))
            {
                PlayerPrefs.SetString("PlayerName", playerNameField.text);
            }
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogWarning("El nombre de la escena de juego no está asignado en el Inspector.");
        }
    }

    public void QuitGame()
    {
        PlayClickSound();
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    // --- AJUSTES Y CONFIGURACIÓN ---
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex, true);
        Debug.Log("Calidad cambiada a: " + QualitySettings.names[qualityIndex]);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Pantalla completa: " + isFullscreen);
    }

    // Método opcional para el Slider de brillo (si necesitas implementar lógica visual)
    public void OnBrightnessChanged(float value)
    {
        // Aquí puedes alterar la exposición de la cámara o un panel translúcido negro
        Debug.Log("Brillo ajustado a: " + value);
    }

    private void PlayClickSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}