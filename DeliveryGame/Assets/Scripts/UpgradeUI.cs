using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    private GameObject scrollViewContentPrefab;

    [SerializeField]
    private List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sprite sprite in sprites)
        {
            GameObject newSprite = Instantiate(scrollViewContentPrefab, scrollViewContent);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
