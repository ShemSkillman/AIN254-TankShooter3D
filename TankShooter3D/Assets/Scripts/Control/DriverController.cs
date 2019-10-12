using UnityEngine;
using TankShooter.Movement;

public class DriverController : MonoBehaviour
{
    TankMove tankMove;

    private void Start()
    {
        tankMove = GetComponent<TankMove>();
    }

    private void Update()
    {
        Accelerate();
        Steer();
    }    

    private void Accelerate()
    {
        tankMove.MoveTank(Input.GetAxis("Vertical"));
    }

    private void Steer()
    {
        tankMove.TurnTank(Input.GetAxis("Horizontal"));
    }
}
