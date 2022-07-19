using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Circle : MonoBehaviour, IPointerDownHandler
{
    private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    private int scoresGiven;
    public int ScoresGiven
    {
        get
        {
            return scoresGiven;
        }
        set
        {
            scoresGiven = value;
        }
    }

    private bool isDestroyed;

    private void FixedUpdate()
    {
        transform.position -= Vector3.up * speed * Time.deltaTime;
        if(transform.position.y < /*Screen.height*/ - GetComponent<RectTransform>().sizeDelta.y)
        {
            Destroy(gameObject);
            CircleFactory.Instance.listOfCircles.Remove(this);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            AddScores();
            DestroyCircle();
        }
    }

    private void AddScores()
    {
        ScoresController.Instance.AddScores(scoresGiven);
    }

    private void DestroyCircle()
    {
        CircleFactory.Instance.listOfCircles.Remove(this);

        Destroy(gameObject);
    }
}
