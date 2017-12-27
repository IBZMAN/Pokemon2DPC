using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {

    public Vector2 offset = new Vector2(-3, -3);

    private SpriteRenderer sprCaster;
    private SpriteRenderer sprShadow;

    private Transform transCaster;
    private Transform transShadow;

    public Material shadowMaterial;
    public Color shadowColor;

    // Use this for initialization
    void Start () {
        transCaster = transform;
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "shadow";
        transShadow.localRotation = Quaternion.identity;

        sprCaster = GetComponent<SpriteRenderer>();
        sprShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        sprShadow.material = shadowMaterial;
        sprShadow.color = shadowColor;

        sprShadow.sortingLayerName = sprCaster.sortingLayerName;
        sprShadow.sortingOrder = sprCaster.sortingOrder - 1;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transShadow.position = new Vector2(transCaster.position.x + offset.x, transCaster.position.y + offset.y);

        sprShadow.sprite = sprCaster.sprite;
	}
}
