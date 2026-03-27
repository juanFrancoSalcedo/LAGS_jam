using System.Collections;
using UnityEngine;

public class MinerYPCHandler : MonoBehaviour, IEmployeeDrivable
{
    [SerializeField] private RockHandler rockHandler;
    [SerializeField] private float timeMiniforWagon = 6f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Wagon wagon;
    [SerializeField] private ResourceSheet[] sheets;

    private IEnumerator Start()
    {
        rockHandler.gameObject.SetActive(false);
        spriteRenderer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        if (employee != null)
        {
            spriteRenderer.gameObject.SetActive(true);
            rockHandler.gameObject.SetActive(true);
            StartCoroutine(Hit());
            StartCoroutine(Mining());
        }
    }

    IEnumerator Mining() 
    {
        while (true)
        {
            yield return new WaitForSeconds(timeMiniforWagon);
            var randomReso = sheets[Random.Range(0, sheets.Length)];
            wagon.AddCollect(randomReso);
        }
    }

    IEnumerator Hit()
    {
        while (true) 
        {
            yield return new WaitForSeconds(1);
            rockHandler.Hit();
        }
    }
    EmployeeSheet employee;

    public void AssignEmployee(EmployeeSheet employee)
    {
        this.employee = employee;
    }
}
