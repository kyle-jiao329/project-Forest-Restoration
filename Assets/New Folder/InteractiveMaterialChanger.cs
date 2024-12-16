using UnityEngine;

public class InteractiveMaterialChanger  : MonoBehaviour
{
    public Material normalMaterials;
    public Material highlightMaterials;
    public AudioClip hoverSound;
    // public AudioClip clickSound;
    public bool playerSoundOnHover = true;
    public bool playerSoundOnClick = false;
    public float hoverDuration = 1f;

    private Material currentMaterial;
    private bool isHovered = false;
    // Start is called before the first frame update
    void Start()
    {
        currentMaterial = normalMaterials;
        GetComponent<Renderer>().material = currentMaterial;
        
    }
    void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
        if (playerSoundOnHover)
        {
            AudioSource.PlayClipAtPoint(hoverSound, transform.position);
        }
        
        changeMaterial(highlightMaterials);
        isHovered = true;
        // if (playerSoundOnHover)
        // {
        //     GetComponent<AudioSource>().PlayOneShot(hoverSound);
        // }
        // currentMaterial = highlightMaterials;
        // GetComponent<Renderer>().material = currentMaterial;
        // isHovered = true;
    }
    void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
        changeMaterial(normalMaterials);
        isHovered = false;
        // currentMaterial = normalMaterials;
        // GetComponent<Renderer>().material = currentMaterial;
        // isHovered = false;
    }
    void OnMouseDown()
    {
        if (playerSoundOnClick)
        {
            AudioSource.PlayClipAtPoint(hoverSound, transform.position);
            // GetComponent<AudioSource>().PlayOneShot(hoverSound);
        }
    }
    void changeMaterial(Material mat)
    {
        GetComponent<Renderer>().material = currentMaterial;
        currentMaterial = mat;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
