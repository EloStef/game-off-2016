using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class ElementsController : MonoBehaviour
{
    public int elementsInLine = 7;
    public int elementsInColumn = 10;
    public int elementsAmount = 7;

    private Transform[,] mapArray;

    private TimerUtil timeToCreate;
    private float widthOfElements = 1f;

    private Score score;
    // Use this for initialization
    void Start()
    {
        mapArray = new Transform[elementsInLine, elementsInColumn];
        timeToCreate = new TimerUtil(1f);
        widthOfElements = GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().bounds.size.x;
        score = new Score(0);
    }

    // Update is called once per frame
    void Update()
    {
        timeToCreate.Update();
        if (timeToCreate.IsLimitReached())
        {
            createNewElement();
        }
    }

    void FixedUpdate()
    {
        UpdateArrayMap();
        deleteGroupOfElements();
    }

    void UpdateArrayMap()
    {
        resetArray();
        GameObject[] elements = GameObject.FindGameObjectsWithTag("Elements");
        foreach (GameObject element in elements)
        {
            Element tempElement = element.GetComponent<Element>();
            if(tempElement != null && tempElement.transform.name != "none")
            if (element.GetComponent<Element>().isGrounded())
            {
                int y = getYInArray(element.transform.position.y);
                if(y >= elementsInColumn)
                    {
                        LooseGame();
                        return;
                    }
                int x = element.GetComponent<VerticalLines>().getCurrentLine();
                if (y > -1 && y < elementsInColumn && x > -1 && x <elementsInLine)
                    mapArray[(int)x, (int)y] = element.transform;
            }
        }
    }

    void createNewElement()
    {
        string element = "Elements/elements" + Random.Range(0, elementsAmount);
        GameObject elementPrefab = Resources.Load(element) as GameObject;
        Instantiate(elementPrefab, 
            new Vector3(
                getRandomLineX(), 8f, 0f
                ), elementPrefab.transform.localRotation);
    }

    void resetArray()
    {
        for (int k = 0; k < mapArray.GetLength(0); k++)
            for (int l = 0; l < mapArray.GetLength(1); l++)
                mapArray[k, l] = null;
    }

    void deleteGroupOfElements()
    {
        for (int k = 0; k < mapArray.GetLength(0); k++)
            for (int l = 0; l < mapArray.GetLength(1); l++)
            {
                if(mapArray[k, l] != null)
                {
                    List<int> list = findAllTheSameElements(k, l, mapArray[k, l].name, new List<int> {});
                    IEnumerable<int> iEnumerable = list.Union(list);
                    if (iEnumerable.Count() > 2)
                    {
                        score.addPoints(iEnumerable.Count());
                        iEnumerable.ToList().ForEach(
                            item => {
                                //GameObject.Destroy(mapArray[(int)Mathf.Floor(item / 1000), item - (int)Mathf.Floor(item / 1000) * 1000].gameObject);
                                mapArray[(int)Mathf.Floor(item / 1000), item - (int)Mathf.Floor(item / 1000) * 1000].gameObject.GetComponent<Element>().DestroyElement();
                                mapArray[(int)Mathf.Floor(item / 1000), item - (int)Mathf.Floor(item / 1000) * 1000] = null;
                            });
                    }
                }
            }
        //Ustawianie AKTULANEJ wartośći punktów w obiekcie GUI TEXT POINTS
        GameObject.FindGameObjectWithTag("Points").GetComponent<Text>().text = score.getCurrentPoints();
    }

    List<int> findAllTheSameElements(int x, int y, string elementKind, List<int> currentList)
    {
        List<int> list = new List<int> { };
        if ( x < 0 || x >= elementsInLine || y < 0 || y >= elementsInColumn)
            return list;
        if (mapArray[x, y] == null)
            return list;
        if (mapArray[x, y].name == "none")
            return list;
        if (mapArray[x,y].name != elementKind)
            return list;
        int valueXY = x * 1000 + y;
        if(currentList.Contains(valueXY))
            return list;
        list.AddRange(currentList);
        list.Add(valueXY);
        list.AddRange(findAllTheSameElements(x + 1, y, elementKind, list));
        list.AddRange(findAllTheSameElements(x, y + 1, elementKind, list));
        list.AddRange(findAllTheSameElements(x - 1, y, elementKind, list));
        list.AddRange(findAllTheSameElements(x, y - 1, elementKind, list));
        return list;
    }

    float getRandomLineX()
    {
        int boundaryLine = (int)Mathf.Floor((elementsInLine - 2) / 2);
        int lineNumber = Random.Range(-boundaryLine, boundaryLine + 1);
        return lineNumber * widthOfElements;
    }

    int getXInArray(float xposition)
    {
        return (int)System.Math.Round(xposition / widthOfElements, 1) + 3;
    }

    int getYInArray(float yposition)
    {
        return (int)Mathf.Floor(yposition / widthOfElements) + 4;
    }

    void LooseGame()
    {
        GameObject[] elements = GameObject.FindGameObjectsWithTag("Elements");
        foreach (GameObject element in elements)
        {
            element.GetComponent<Element>().DestroyElement();
        }
        GameObject.FindGameObjectWithTag("MenuController").GetComponent<Menu>().setActiveCanvas("MainCanvas");
        Destroy(gameObject);
        }
}
