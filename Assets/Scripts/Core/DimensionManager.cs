using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public GameObject whiteworld;
    public GameObject blackworld;
    public int maxShifts = 99;
    public int remainingShifts;
    private bool isWhiteActive;

    void Start()
    {
        remainingShifts = maxShifts;
        UpdateWorldState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&& remainingShifts > 0)
        {
            ShiftDemension();    
        }
    }

    void ShiftDemension()
    {
        isWhiteActive = !isWhiteActive;
        UpdateWorldState();
        remainingShifts--;

        Debug.Log(remainingShifts);
    }

    void UpdateWorldState()
    {
        whiteworld.SetActive(!isWhiteActive);
        blackworld.SetActive(isWhiteActive);
    }
}
