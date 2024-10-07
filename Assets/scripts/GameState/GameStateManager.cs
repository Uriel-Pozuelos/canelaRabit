using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    // Atributos del estado del jugador
    public int health;
    public float playerSpeed;
    public int experience;
    public int level;
    public int damage;
    public int jumpForce;

    // Dificultad del juego
    public enum Difficulty { Normal, Hard, Expert }
    public Difficulty gameDifficulty = Difficulty.Normal;

    // Atributos de la cámara
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;


    //gravity
    public float gravity = 9.8f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);  // Persistir entre escenas
        }
    }

    // Métodos para modificar el estado del jugador
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Jugador muerto");
        }
    }

    public void AddExperience(int xp)
    {
        experience += xp;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (experience >= 100)
        {
            level++;
            experience = 0;  // Reiniciar experiencia
            Debug.Log("Subiste de nivel: " + level);
        }
    }

    // Método para obtener el multiplicador de dificultad
    public float GetDifficultyMultiplier()
    {
        switch (gameDifficulty)
        {
            case Difficulty.Hard:
                return 1.5f;  // Aumenta la dificultad en 50%
            case Difficulty.Expert:
                return 2.0f;  // Aumenta la dificultad en 100%
            default:
                return 1.0f;  // Normal, sin multiplicador adicional
        }
    }

    public float GetPlayerSpeed()
    {
        float baseSpeed = 5f; // Velocidad base
        float difficultyMultiplier = GetDifficultyMultiplier(); // Método que devuelve el multiplicador de dificultad
        int playerLevel = level; // Asegúrate de que 'level' sea la variable de nivel del jugador

        return baseSpeed + (playerLevel * 0.2f * difficultyMultiplier);
    }

}
