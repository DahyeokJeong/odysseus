using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private GameObject currentMapContents;

    public void ChangeMap(GameObject nextMapContents)
    {
        if(currentMapContents != null)
            currentMapContents.SetActive(false);

        nextMapContents.SetActive(true);

        currentMapContents = nextMapContents;
    }
}
