using System;
using UnityEngine;
using Zenject;

public class Wagon:MonoBehaviour,IEmployeeDrivable
{
    [SerializeField] private float distanceCollect = 4f;
    [SerializeField] private float maxFollowDistance = 10f;
    [SerializeField] private float followSpeed = 3f;
    [SerializeField] TriggerDetector triggerDetector;
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

    private void OnEnable() => triggerDetector.OnTriggerEntered += Collect;
    private void OnDisable() => triggerDetector.OnTriggerEntered -= Collect;
    
    private void Collect(Transform _transform)
    {
        if(_transform.TryGetComponent<ResourceHandler>(out var compo))
        {
            print("TODO Inject this perrito, shutese esto mi socio");
            _transform.gameObject.SetActive(false);
            MiningMediator.Publish(TypeMiningEvent.CollectResource);
            InventoryDataService.AddItem(compo.Sheet.GetModelCopy());
        }
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
        //spriteRenderer.sprite = employee.Sprite;
        spriteRenderer.gameObject.SetActive(true);
    }
}


public interface IEmployeeDrivable
{
    public void AssignEmployee(EmployeeSheet employee);
}
