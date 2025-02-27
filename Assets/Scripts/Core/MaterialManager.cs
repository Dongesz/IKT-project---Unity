using System.Collections.Generic;
using UnityEngine;

public class ShiftMaterialManager : MonoBehaviour
{
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material whiteMaterial;

    [SerializeField] private Material blackSkybox;
    [SerializeField] private Material whiteSkybox;

    [SerializeField] private List<MeshRenderer> targetRenderers = new List<MeshRenderer>(); // 3D modellek
    [SerializeField] private List<SpriteRenderer> targetSprites = new List<SpriteRenderer>(); // 2D sprite-ok

    private bool isShiftActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isShiftActive)
        {
            SetMaterial(blackMaterial, blackSkybox);
            isShiftActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && isShiftActive)
        {
            SetMaterial(whiteMaterial, whiteSkybox);
            isShiftActive = false;
        }
    }

    private void SetMaterial(Material newMaterial, Material newSkybox)
    {
        foreach (var renderer in targetRenderers)
        {
            renderer.material = newMaterial;
        }

        foreach (var sprite in targetSprites)
        {
            sprite.material = newMaterial;
        }

        
    }
}
