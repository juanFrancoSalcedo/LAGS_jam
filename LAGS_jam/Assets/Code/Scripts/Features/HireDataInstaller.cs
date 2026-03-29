using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HireDataInstaller: MonoBehaviour 
{
    [SerializeField] [RequireBadInterface(typeof(IEmployeeDrivable))]
    private MonoBehaviour mDrivableWagon;
    private IEmployeeDrivable Wagon;

    [SerializeField]
    [RequireBadInterface(typeof(IEmployeeDrivable))]
    private MonoBehaviour mDrivableMiner;
    private IEmployeeDrivable Miner;

    [SerializeField] private List<EmployeeSheet> employees = new List<EmployeeSheet>();


    private void Awake()
    {
        HireDataService.ReadData();
        if (HireDataService.runTimeData == null || HireDataService.runTimeData.employees.Count == 0)
            return;
        var wagonStorageData = HireDataService.runTimeData.employees.FirstOrDefault(e => e.typeTask == TypeEmployeeTask.DriveWagon);
        var wagonData = employees.First(t =>t.Model.typeTask == TypeEmployeeTask.DriveWagon);
        if (wagonData != null && wagonStorageData != null)
        {
            Wagon = mDrivableWagon.GetComponent<IEmployeeDrivable>();
            Wagon.AssignEmployee(wagonData);
        }

        var minerStorageData = HireDataService.runTimeData.employees.FirstOrDefault(e => e.typeTask == TypeEmployeeTask.PickRock);
        var minerData = employees.First(t => t.Model.typeTask == TypeEmployeeTask.PickRock);
        if (minerData != null && minerStorageData != null)
        {
            Miner = mDrivableMiner.GetComponent<IEmployeeDrivable>();
            Miner.AssignEmployee(minerData);
        }
    }
}
