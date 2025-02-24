using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    public DimensionManager dimensionManager;
    [SerializeField] private TextMeshProUGUI shiftTxt;

    public void shifts()
    {
        shiftTxt.text = "Shifs: " + dimensionManager.remainingShifts.ToString();
    }
    private void Update()
    {
        shifts();
    }
}
