using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureShowHide : MonoBehaviour {
    public string textureName = "";

    private int[] texturePixels;
    private bool isDisapearing = false;
    private Texture2D texture;

	// Use this for initialization
	void Start () {
        texture = Instantiate(Resources.Load("Textures/" + textureName) as Texture2D) as Texture2D;
        GetComponent<Renderer>().material.mainTexture = texture;
        //runDisapearing();
    }
	
	// Update is called once per frame
	void Update () {
        if (isDisapearing)
        {
                removePixel(20);
        }
	}

    void deleteObject()
    {
        GameObject.Destroy(transform.parent.gameObject);
        GameObject.Destroy(gameObject);
    }

    void removePixel(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (texturePixels.Length < 1)
            {
                deleteObject();
                return;
            }
            int index = Random.Range(0, texturePixels.Length);
            int xyOfPixel = texturePixels[index];
            int x = (int)Mathf.Floor(xyOfPixel / 1000);
            int y = xyOfPixel - (int)Mathf.Floor(xyOfPixel / 1000) * 1000;
            texture.SetPixel(x, y, new Color(0F, 0F, 0F, 0F));
            texturePixels = texturePixels.RemoveFromArray(xyOfPixel);
        }
        texture.Apply();
    }

    public void runDisapearing()
    {
        preparePixelsArray();
        isDisapearing = true;
    }

    /// <summary>
    /// put all indexes (x, y) of texture in the array
    /// </summary>
    void preparePixelsArray()
    {
        texturePixels = new int[texture.height * texture.width];
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                texturePixels[y * texture.height + x] = (y * 1000) + x;
            }
        }
    }
}
