using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Combate : MonoBehaviour
{
    public int playerHealth = 10;
    public int enemyHealth = 10;
    public int damage = 2;
    private bool playerTurn = true;
    private bool isDodging = false;

    private int dodgeUsesRemaining = 2;

    public Slider playerHealthBar;
    public Slider enemyHealthBar;
    public Text turnCounterText;
    public TMP_Text statusText;
    public GameObject startScreen;
    public GameObject endScreen; // Canvas de fin
    public TMP_Text endText; // Texto de fin (Victoria o Derrota)

    public AudioClip playerAttackSound;
    public AudioClip enemyAttackSound;
    public AudioClip playerHurtSound;
    public AudioClip enemyHurtSound;
    public AudioClip playerDefeatSound;
    public AudioClip enemyDefeatSound;
    public AudioSource audioSource;

    public float attackDistance = 2f;
    public float dodgeDistance = 1.5f;

    private Vector3 playerOriginalPosition;
    private Vector3 enemyOriginalPosition;

    private int turnCounter = 0;

    // Variables para los materiales del jugador
    public Material defaultPlayerMaterial;
    public Material attackPlayerMaterial;
    public Material dodgePlayerMaterial;

    // Variables para los materiales del enemigo
    public Material defaultEnemyMaterial;
    public Material attackEnemyMaterial;
    public Material dodgeEnemyMaterial;

    private SkinnedMeshRenderer playerSkinnedMeshRenderer;
    private SkinnedMeshRenderer enemySkinnedMeshRenderer;

    // Referencia al Animator del jugador y enemigo
    private Animator playerAnimator;
    private Animator enemyAnimator;

    // Botones de ataque y esquivar
    public Button attackButton; // Botón de ataque
    public Button dodgeButton;  // Botón de esquivar

    void Start()
    {
        // Validación de referencias en el Inspector
        if (startScreen == null)
        {
            Debug.LogError("Start Screen no está asignado en el Inspector.");
        }

        if (endScreen == null)
        {
            Debug.LogError("End Screen no está asignado en el Inspector.");
        }

        if (endText == null)
        {
            Debug.LogError("End Text no está asignado en el Inspector.");
        }

        playerOriginalPosition = transform.position;
        enemyOriginalPosition = GameObject.FindGameObjectWithTag("Enemy").transform.position;

        playerHealthBar.maxValue = playerHealth;
        playerHealthBar.value = playerHealth;
        enemyHealthBar.maxValue = enemyHealth;
        enemyHealthBar.value = enemyHealth;

        if (turnCounterText != null)
        {
            turnCounterText.text = "Turno: " + turnCounter;
        }

        // Obtener el SkinnedMeshRenderer del jugador y del enemigo
        playerSkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        enemySkinnedMeshRenderer = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<SkinnedMeshRenderer>();

        // Obtener el Animator del jugador y del enemigo
        playerAnimator = GetComponentInChildren<Animator>();
        enemyAnimator = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<Animator>();

        // Asignar los materiales por defecto al inicio
        if (playerSkinnedMeshRenderer != null && defaultPlayerMaterial != null)
        {
            playerSkinnedMeshRenderer.material = defaultPlayerMaterial;
        }

        if (enemySkinnedMeshRenderer != null && defaultEnemyMaterial != null)
        {
            enemySkinnedMeshRenderer.material = defaultEnemyMaterial;
        }

        // Desactivar la pantalla de fin al inicio
        if (endScreen != null)
        {
            endScreen.SetActive(false);
        }

        // Asignar las funciones de los botones en el evento OnClick
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(PlayerAttack);
        }

        if (dodgeButton != null)
        {
            dodgeButton.onClick.AddListener(PlayerDodge);
        }

        StartCoroutine(FadeOutStartScreen());
    }

    void Update()
    {
        if (playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                PlayerAttack();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                PlayerDodge();
            }
        }

        if (turnCounterText != null)
        {
            turnCounterText.text = "Turno: " + turnCounter;
        }
    }

    void PlayerAttack()
    {
        if (!playerTurn) return;

        Debug.Log("Jugador ataca!");

        audioSource.PlayOneShot(playerAttackSound);

        transform.Translate(Vector3.forward * attackDistance);

        enemyHealth -= damage;
        enemyHealthBar.value = enemyHealth;
        Debug.Log("Vida del enemigo: " + enemyHealth);

        if (enemySkinnedMeshRenderer != null)
        {
            // Cambiar el material del enemigo cuando recibe daño
            enemySkinnedMeshRenderer.material = attackEnemyMaterial;
            StartCoroutine(ResetEnemyMaterial(0.5f));
        }

        turnCounter++;
        Debug.Log("Turno número: " + turnCounter);

        playerTurn = false;

        Invoke("ReturnPlayerToOriginalPosition", 0.5f);

        if (enemyHealth > 0)
        {
            Invoke("EnemyTurn", 1.5f);
        }
        else
        {
            Debug.Log("¡El enemigo ha sido derrotado!");
            audioSource.PlayOneShot(enemyDefeatSound);
            StartCoroutine(MakeEnemyFall());
            ShowEndMessage("¡Victoria!");
        }
    }

    void PlayerDodge()
    {
        if (!playerTurn) return;

        if (dodgeUsesRemaining <= 0)
        {
            Debug.Log("¡Ya no puedes esquivar más!");
            return;
        }

        Debug.Log("Jugador esquiva...");

        transform.Translate(Vector3.back * dodgeDistance);

        isDodging = true;
        dodgeUsesRemaining--;

        if (playerSkinnedMeshRenderer != null)
        {
            playerSkinnedMeshRenderer.material = dodgePlayerMaterial;
        }

        playerTurn = false;

        Invoke("ReturnPlayerToOriginalPosition", 0.5f);

        Invoke("EnemyTurn", 1.5f);
    }

    void EnemyTurn()
    {
        if (enemyHealth > 0)
        {
            Debug.Log("El enemigo ataca!");

            audioSource.PlayOneShot(enemyAttackSound);

            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            enemy.transform.Translate(Vector3.forward * attackDistance);

            if (!isDodging)
            {
                playerHealth -= damage;
                playerHealthBar.value = playerHealth;
                Debug.Log("Vida del jugador: " + playerHealth);

                audioSource.PlayOneShot(playerHurtSound);

                if (playerSkinnedMeshRenderer != null)
                {
                    playerSkinnedMeshRenderer.material = attackPlayerMaterial;
                    StartCoroutine(ResetPlayerMaterial(0.5f));
                }
            }
            else
            {
                Debug.Log("¡El ataque del enemigo ha sido esquivado!");
                isDodging = false;

                if (playerSkinnedMeshRenderer != null)
                {
                    playerSkinnedMeshRenderer.material = defaultPlayerMaterial;
                }
            }

            if (playerHealth > 0)
            {
                turnCounter++;
                Debug.Log("Turno número: " + turnCounter);
                playerTurn = true;
            }
            else
            {
                Debug.Log("¡El jugador ha sido derrotado!");
                audioSource.PlayOneShot(playerDefeatSound);
                StartCoroutine(MakePlayerFall());
                ShowEndMessage("¡Derrota!");
            }

            Invoke("ReturnEnemyToOriginalPosition", 0.5f);
        }
    }

    // Coroutine para hacer que el enemigo caiga al suelo
    IEnumerator MakeEnemyFall()
    {
        float fallSpeed = 1f;
        float fallDuration = 2f;
        float timer = 0f;

        while (timer < fallDuration)
        {
            timer += Time.deltaTime;
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            enemy.transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            yield return null;
        }

        GameObject.FindGameObjectWithTag("Enemy").transform.position = new Vector3(enemyOriginalPosition.x, 0f, enemyOriginalPosition.z);
    }

    // Coroutine para hacer que el jugador caiga al suelo
    IEnumerator MakePlayerFall()
    {
        float fallSpeed = 1f;
        float fallDuration = 2f;
        float timer = 0f;

        while (timer < fallDuration)
        {
            timer += Time.deltaTime;
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    IEnumerator ResetPlayerMaterial(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (playerSkinnedMeshRenderer != null && defaultPlayerMaterial != null)
        {
            playerSkinnedMeshRenderer.material = defaultPlayerMaterial;
        }
    }

    IEnumerator ResetEnemyMaterial(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (enemySkinnedMeshRenderer != null && defaultEnemyMaterial != null)
        {
            enemySkinnedMeshRenderer.material = defaultEnemyMaterial;
        }
    }

    void ReturnPlayerToOriginalPosition()
    {
        transform.position = playerOriginalPosition;
    }

    void ReturnEnemyToOriginalPosition()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemy.transform.position = enemyOriginalPosition;
    }

    // Corutina para desvanecer la pantalla de inicio después de 2 segundos
    IEnumerator FadeOutStartScreen()
    {
        // Esperar 1 segundos antes de empezar a desvanecer
        yield return new WaitForSeconds(1f);

        CanvasGroup canvasGroup = startScreen.GetComponent<CanvasGroup>();
        float duration = 3f;
        float startTime = Time.time;

        // Iniciar el desvanecimiento
        while (Time.time - startTime < duration)
        {
            canvasGroup.alpha = 1 - ((Time.time - startTime) / duration);
            yield return null;
        }

        startScreen.SetActive(false);
    }

    void ShowEndMessage(string message)
    {
        // Muestra el Canvas final y el mensaje
        endText.text = message;
        endText.gameObject.SetActive(true);
        endScreen.SetActive(true);

        StartCoroutine(CambiarEscenaConRetraso());
    }

    private IEnumerator CambiarEscenaConRetraso()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("JuegoPostCombate");
    }
}
