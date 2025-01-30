using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast should the texture scroll?")]
    public float scrolleSpeed;

    [Header("References")]
    public MeshRenderer meshRenderer;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrolleSpeed * Time.deltaTime, 0);
    }
}
