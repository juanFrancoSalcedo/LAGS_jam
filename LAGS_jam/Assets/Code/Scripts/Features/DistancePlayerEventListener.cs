using System;
using UnityEngine;
using UnityEngine.Events;

public class DistancePlayerEventListener : MonoBehaviour 
{
    [Header("Distance Settings")]
    [SerializeField] private float nearDistance = 2f;
    [Tooltip("Si es true, buscará al jugador cada frame. Si es false, solo al inicio.")]
    [SerializeField] private bool continuousSearch = false;

    [Header("Events")]
    [SerializeField] private UnityEvent onNear;
    [SerializeField] private UnityEvent onFarAway;

    public event Action OnNear;
    public event Action OnFarAway;

    private PlayerHandler playerHandler;
    private Transform playerTransform;
    
    // Estados para evitar invocar múltiples veces el mismo evento
    private bool isNear = false;
    private bool initialized = false;

    private void Start()
    {
        FindPlayer();
    }

    private void Update()
    {
        // Si no hay jugador, intentar buscarlo continuamente si está habilitado
        if (playerHandler == null && continuousSearch)
        {
            FindPlayer();
            return;
        }

        // Si ya encontramos al jugador, verificar la distancia
        if (playerHandler != null && playerTransform != null)
        {
            CheckDistance();
        }
    }

    /// <summary>
    /// Busca al jugador en la escena
    /// </summary>
    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        
        if (playerObject != null)
        {
            playerHandler = playerObject.GetComponent<PlayerHandler>();
            
            if (playerHandler != null)
            {
                playerTransform = playerHandler.transform;
                initialized = true;
            }
            else
            {
                Debug.LogWarning($"[DistancePlayerEventListener] El GameObject con tag 'Player' no tiene componente PlayerHandler en {gameObject.name}");
            }
        }
    }

    /// <summary>
    /// Verifica la distancia y dispara los eventos correspondientes
    /// </summary>
    private void CheckDistance()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // Si está cerca y no estaba cerca antes
        if (distance < nearDistance && !isNear)
        {
            isNear = true;
            InvokeNearEvents();
        }
        // Si está lejos y estaba cerca antes
        else if (distance >= nearDistance && isNear)
        {
            isNear = false;
            InvokeFarAwayEvents();
        }
    }

    /// <summary>
    /// Invoca los eventos cuando el jugador está cerca
    /// </summary>
    private void InvokeNearEvents()
    {
        onNear?.Invoke();
        OnNear?.Invoke();
    }

    /// <summary>
    /// Invoca los eventos cuando el jugador está lejos
    /// </summary>
    private void InvokeFarAwayEvents()
    {
        onFarAway?.Invoke();
        OnFarAway?.Invoke();
    }

    /// <summary>
    /// Obtiene la distancia actual al jugador
    /// </summary>
    public float GetCurrentDistance()
    {
        if (playerTransform != null)
        {
            return Vector3.Distance(playerTransform.position, transform.position);
        }
        return float.MaxValue;
    }

    /// <summary>
    /// Verifica si el jugador está cerca actualmente
    /// </summary>
    public bool IsPlayerNear()
    {
        return isNear;
    }

    /// <summary>
    /// Cambia la distancia de detección en runtime
    /// </summary>
    public void SetNearDistance(float newDistance)
    {
        nearDistance = Mathf.Max(0, newDistance);
    }

    /// <summary>
    /// Fuerza una verificación de distancia inmediata
    /// </summary>
    public void ForceCheck()
    {
        if (playerHandler != null && playerTransform != null)
        {
            CheckDistance();
        }
    }

    // Visualización en el editor
    private void OnDrawGizmosSelected()
    {
        // Dibujar el radio de detección
        Gizmos.color = isNear ? Color.green : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, nearDistance);
        
        // Dibujar línea al jugador si existe
        if (playerTransform != null)
        {
            Gizmos.color = isNear ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
    }
}