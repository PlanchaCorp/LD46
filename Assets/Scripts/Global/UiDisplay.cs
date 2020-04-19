using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiDisplay : MonoBehaviour
{
    [SerializeField]
    private Image oxygenBar;
    [SerializeField]
    private TextMeshProUGUI dodoniumAmount;
    private SpaceStationManager spaceStationManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spaceStation = GameObject.FindWithTag("SpaceStation");
        if (spaceStation == null)
        {
            Debug.LogError("Unable to find SpaceStation gameObject to initialize UI!");
        } else {
            spaceStationManager = spaceStation.GetComponent<SpaceStationManager>();
        }
        InvokeRepeating("ScarceUpdate", 0, 0.15f);
    }

    // Update is called once per frame
    void ScarceUpdate()
    {
        oxygenBar.fillAmount = spaceStationManager.oxygenAmount / spaceStationManager.OXYGEN_MAX_AMOUNT;
        dodoniumAmount.text = spaceStationManager.dodoniumAmount.ToString();
    }
}
