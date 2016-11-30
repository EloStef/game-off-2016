using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureShowHide : MonoBehaviour {
    public string textureName = "";

    private int[] texturePixels;
    private int isDisapearing = 0;
    private Texture2D texture;
    private Texture2D creatingTexture;
    private int speed = 800;

    private bool cascadeDistapering = false;

    // Use this for initialization
    void Awake ()
    {
        texture = Instantiate(Resources.Load("Textures/" + textureName) as Texture2D) as Texture2D;
        GetComponent<Renderer>().material.mainTexture = texture;
    }

    void Start () {
        //texture = Instantiate(Resources.Load("Textures/" + textureName) as Texture2D) as Texture2D;
        
        //runShowingObject();
        //runDisapearing();
    }
	
	// Update is called once per frame
	void Update () {
        if (isDisapearing == 1)
        {
                removePixel(speed);
        }
        if (isDisapearing == -1)
        {
                putPixel(speed);
        }
    }

    void deleteObject()
    {
        GameObject.Destroy(transform.parent.gameObject);
        GameObject.Destroy(gameObject);
    }

    public void runShowingObject(int speed)
    {
        this.speed = speed;
        preparePixelsArray();
        creatingTexture = Instantiate(Resources.Load("Textures/empty") as Texture2D) as Texture2D;
        GetComponent<Renderer>().material.mainTexture = creatingTexture;
        isDisapearing = -1;
    }

    void putPixel(int amount)
    {
        for (int i = 0; i < amount * Time.deltaTime; i++)
        {
            if (texturePixels.Length < 1)
            {
                GetComponent<Renderer>().material.mainTexture = texture;
                isDisapearing = 0;
                return;
            }
            int index = Random.Range(0, texturePixels.Length);
            int xyOfPixel = texturePixels[index];
            int x = (int)Mathf.Floor(xyOfPixel / 1000);
            int y = xyOfPixel - (int)Mathf.Floor(xyOfPixel / 1000) * 1000;
            creatingTexture.SetPixel(x, y, texture.GetPixel(x, y));
            texturePixels = texturePixels.RemoveFromArray(xyOfPixel);
        }
        creatingTexture.Apply();
    }

    void removePixel(int amount)
    {
        for (int i = 0; i < amount * Time.deltaTime; i++)
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
        if(texturePixels.Length < 500)
        {
            if (cascadeDistapering)
            {
                GameObject[] elements = GameObject.FindGameObjectsWithTag("Elements");
                for (int i = 0; i < 2; i++)
                {

                    elements[Random.Range(0, elements.Length)].GetComponentInChildren<TextureShowHide>().cascadeDisapearing();
                }
            }
            cascadeDistapering = false;
        }
        texture.Apply();
    }

    public void runDisapearing(int speed)
    {
        if (isDisapearing == 1)
            return;
        GetComponent<Renderer>().material.mainTexture = texture;
        this.speed = speed;
        preparePixelsArray();
        isDisapearing = 1;
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

    /// <summary>
    /// start disapering this object and turn cascede to true 
    /// then when object will be destroyed it will destroy next 3 elements with tag Element
    /// </summary>
    public void cascadeDisapearing()
    {
        runDisapearing(2000);
        cascadeDistapering = true;
    }
}
