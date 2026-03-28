using System;
using UnityEngine;
using Zenject;

public class Wagon:MonoBehaviour,IEmployeeDrivable
{
    [SerializeField] private float distanceCollect = 4f;
    [SerializeField] private float maxFollowDistance = 10f;
    [SerializeField] private float followSpeed = 3f;
    [SerializeField] TriggerDetector triggerDetector;
    [SerializeField] CollisionDetector[] collisionDetectors;
    [SerializeField] private SpriteRenderer spriteRenderer;
    PlayerHandler playerHandler;
    Rigidbody rb;
    
    private void Start()
    {
        playerHandler = GameObject.FindWithTag("Player").GetComponent<PlayerHandler>();
        rb = GetComponent<Rigidbody>();
    }

    public bool IsNearPlayer() 
    {
        return Vector3.Distance(playerHandler.transform.position,transform.position) < distanceCollect;
    }

    private void OnEnable()
    {
        triggerDetector.OnTriggerEntered += Collect;
        
        // Suscribir a cada detector de colisión en el array
        foreach (var detector in collisionDetectors)
        {
            if (detector != null)
            {
                detector.OnCollisionStayed += CollisionStayed;
            }
        }
    }

    private void OnDisable()
    {
        triggerDetector.OnTriggerEntered -= Collect;
        
        // Desuscribir de cada detector de colisión en el array
        foreach (var detector in collisionDetectors)
        {
            if (detector != null)
            {
                detector.OnCollisionStayed -= CollisionStayed;
            }
        }
    }


    private void CollisionStayed(Collision collision)
    {
        if (collision.transform.TryGetComponent<PlayerHandler>(out var compo))
        {
            compo.DebtStamina(0.02f);
        }
    }

    private void Collect(Transform _transform)
    {
        if(_transform.TryGetComponent<ResourceHandler>(out var compo))
        {
            _transform.gameObject.SetActive(false);
            AddCollect(compo.Sheet);
        }
    }

    public void AddCollect(ResourceSheet Sheet) 
    {
        MiningMediator.Publish(TypeMiningEvent.CollectResource);
        InventoryDataService.AddItem(Sheet.GetModelCopy());
        AudioManager.Instance.PlayCollectResource();
    }

    private void Update()
    {
        if (employee)
        {
            var playerPosBuffer = playerHandler.transform.position;
            float distanceToPlayer = Vector3.Distance(playerPosBuffer, transform.position);
            
            // Si el jugador está muy lejos, acercar el vagón
            if (distanceToPlayer > maxFollowDistance)
            {
                Vector3 directionToPlayer = (playerPosBuffer - transform.position).normalized;
                Vector3 targetPosition = Vector3.MoveTowards(transform.position, playerPosBuffer, followSpeed * Time.deltaTime);
                
                // Mantener la Y fija (asumiendo movimiento 2D o en plano horizontal)
                targetPosition.y = transform.position.y;
                
                rb.MovePosition(targetPosition);
            }
        }
    }

    EmployeeSheet employee;

    public void AssignEmployee(EmployeeSheet employee)
    {
        this.employee = employee;
        spriteRenderer.gameObject.SetActive(true);
    }
}


public interface IEmployeeDrivable
{
    public void AssignEmployee(EmployeeSheet employee);
}
