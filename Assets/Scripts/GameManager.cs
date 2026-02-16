using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    const int LIVES = 3;
    const int vIDA_EXTRA_PRECIO = 1000; // Puntos necesarios para vida extra

    [SerializeField] TextMeshProUGUI txtScore;
    [SerializeField] TextMeshProUGUI txtMaxScore;
    [SerializeField] TextMeshProUGUI txtMessage;
    //Array paara las imágenes que marcan las vidas 
    [SerializeField] GameObject[] imgLives;
    [SerializeField] GameObject vida1;
    [SerializeField] GameObject vida2;
    [SerializeField] GameObject vida3;
    
    int score;
    int maxScore;
    //Inicializamos las vidas a la constante 
    int lives = LIVES;

    static GameManager instance;

    private void OnGUI()
    {
        for (int i = 0; i < imgLives.Length; i++)
        {
            imgLives[i].SetActive(i < lives);
        }
        txtScore.text = string.Format("{0,4:D4}", score);
    }
    // Método estático para obtener la instancia del GameManager
    public static GameManager GetInstance()
    {
        return instance;
    }

    // Función Awake se ejecuta cuando se instancia el objeto
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Evitar que el objeto se destruya al cambiar de escena
        }
        else if (instance != this)
        {
            // Si ya existe una instancia, destruimos el nuevo GameManager para mantener la singularidad
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;

        // Actualizar récord
        if (score > maxScore)
        {
            maxScore = score;
            txtMaxScore.text = string.Format("{0,4:D4}", maxScore);
        }

        CheckExtraLife();
    }

    void AddLife()
    {
        if (lives < imgLives.Length)
        {
            lives++;
        }
    }

    public void LoseLife()
    {
        lives--;

        if (lives == 2)
        {
            vida3.SetActive(false);
        }
        else if (lives == 1)
        {
            vida2.SetActive(false);
        }
        else if (lives <= 0)
        {
            vida1.SetActive(false);
            lives = 0;
            txtMessage.text = "GAME OVER";
            Time.timeScale = 0f;
        }
    }

    void CheckExtraLife()
{
    if (score >= vIDA_EXTRA_PRECIO && lives < 3)
    {
        score -= vIDA_EXTRA_PRECIO;
        lives++;

        if (lives == 1)
        {
            vida1.SetActive(true);
        }
        else if (lives == 2)
        {
            vida2.SetActive(true);
        }
        else if (lives == 3)
        {
            vida3.SetActive(true);
        }
    }
}

}