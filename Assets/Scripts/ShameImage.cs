using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShameImage : MonoBehaviour
{
    ShameManager sManager;

    // Start is called before the first frame update
    void Awake()
    {
        sManager = FindObjectOfType<ShameManager>();   
    }
    
    public void ClearThisShame()
    {
        int myIndex = transform.GetSiblingIndex();
        sManager.RemoveShame(myIndex);
        Destroy(gameObject);

    }
}
