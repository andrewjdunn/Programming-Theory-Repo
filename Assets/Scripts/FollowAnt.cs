using UnityEngine;
using UnityEngine.UIElements;

public class FollowAnt : MonoBehaviour
{

    [SerializeField]
    private GameObject antParent;

    [SerializeField]
    private int antTargetIndex;
    
    void Start()
    {
        antTargetIndex = 0;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            ++antTargetIndex;
            Debug.Log($"Setting index to {antTargetIndex}");
            if(antTargetIndex > antParent.transform.childCount)
            {
                antTargetIndex = 0;
                Debug.Log($"Wrapped to 0");
            }
            
        } 
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            --antTargetIndex;
            Debug.Log($"Setting index to {antTargetIndex}");
            if (antTargetIndex < 0)
            {
                antTargetIndex = antParent.transform.childCount - 1;
                Debug.Log($"Wrapped to {antTargetIndex}");
            }
        }

        if (antParent.transform.childCount > antTargetIndex)
        {
            var targetTramsform = antParent.transform.GetChild(antTargetIndex);
            var targetPositon = targetTramsform.transform.position;
            targetPositon.y += 15;
            targetPositon.z -= 10;
            transform.SetPositionAndRotation(targetPositon, transform.rotation);
            
        }
    }
}
