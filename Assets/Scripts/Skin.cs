using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Skin : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public string spriteSheetName; //nome do spritesheet que queremos utilizar
    public string activeSpriteSheetName; //nome so spritesheet em uso

    private Dictionary<string, Sprite> spriteSheet;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.loadSpriteSheet();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(activeSpriteSheetName != spriteSheetName)
        {
            this.loadSpriteSheet();
        }
        spriteRenderer.sprite = spriteSheet[spriteRenderer.sprite.name];
    }

    private void loadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/Characters/" + spriteSheetName);
        spriteSheet = sprites.ToDictionary(s => s.name, s => s);
        activeSpriteSheetName = spriteSheetName;
    }
}
