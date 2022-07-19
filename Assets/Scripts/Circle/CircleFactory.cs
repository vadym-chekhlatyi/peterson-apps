using System.Collections.Generic;
using UnityEngine;

public class CircleFactory : MonoBehaviour
{
    public static CircleFactory Instance;

    [SerializeField] private Circle circle;

    [HideInInspector] public bool started;
    [HideInInspector] public List<Circle> listOfCircles = new List<Circle>();
    [HideInInspector] public List<Texture2D> textureList = new List<Texture2D>();

    [Space]
    [Header("Game settings")]
    public int maxCircleCount = 5;

    public float minSpawnTime = 1f;

    public float maxSpawnTime = 3f;

    [Space]
    [Header("Circle variables")]
    public float speed = 50f;

    public float speedIncreaseOverCount = 2f;

    public int scoresGiven = 50;

    private float timer;

    private void Start()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (!started)
            return;

        if (listOfCircles.Count < maxCircleCount)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = Random.Range(minSpawnTime, maxSpawnTime);
                CreateNewCircle();
            }
        }
    }

    private void CreateNewCircle()
    {
        int randomSize = Random.Range(50, Screen.width);

        float textureSize = randomSize / Screen.width;  // Getting a part of max size

        Texture2D newTexture;

        if(textureSize < 0.25f)
        {
            textureSize = 32;
            newTexture = textureList[0];
        }
        else if(textureSize < 0.5f)
        {
            textureSize = 64;
            newTexture = textureList[1];
        }
        else if(textureSize < 0.75f)
        {
            textureSize = 128;
            newTexture = textureList[2];
        }
        else
        {
            textureSize = 256;
            newTexture = textureList[3];
        }

        Vector3 positionToInstantiate = new Vector3(Random.Range(randomSize / 2, Screen.width - randomSize / 2), Screen.height + randomSize / 2);
        
        Circle newCircle = Instantiate(circle, positionToInstantiate, Quaternion.identity, transform);
        
        newCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(randomSize, randomSize);
        //newCircle.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(newTexture, new Rect(Vector2.zero, new Vector2(textureSize, textureSize)), new Vector2(0f,0f));

        newCircle.Speed = speed * (Screen.width / randomSize);
        newCircle.ScoresGiven = scoresGiven * (Screen.width / randomSize);

        listOfCircles.Add(newCircle);
    }

    private Texture2D CreateTextureSet(int size)
    {
        Texture2D newTexture = new Texture2D(size, size);
        
        return CreateCircle(newTexture, 0, 0, size/2, Color.white);
    }

    public Texture2D CreateCircle(Texture2D tex, int x, int y, int r, Color color)
    {
        float rSquared = r * r;

        for (int u = 0; u < tex.width; u++)
        {
            for (int v = 0; v < tex.height; v++)
            {
                if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared) tex.SetPixel(u, v, color);
            }
        }

        return tex;
    }

}
