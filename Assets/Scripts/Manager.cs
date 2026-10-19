using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private VideoController videoController;
    [SerializeField] private UIController uIController;

    private void OnEnable()
    {
        BindEvents();
    }

    private void OnDisable()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        uIController.OnTestStartRequested += videoController.TestStart;
    }

    private void UnbindEvents()
    {
        uIController.OnTestStartRequested -= videoController.TestStart;
    }
}
