using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : BaseManager
{
    public float ScrollSpeed;
    public Transform[] Backgrounds;

    private float _bottomPos;
    private float _imageHeight;

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);

        _imageHeight = Backgrounds[0].GetChild(0).GetComponent<SpriteRenderer>().size.y;
        _bottomPos = Backgrounds[0].position.y - _imageHeight;
    }

    void Update()
    {
        ScrollingBackground();
    }

    void ScrollingBackground()
    {
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            if (null == Backgrounds[i])
                continue;

            Backgrounds[i].position += new Vector3(0, -ScrollSpeed, 0) * Time.deltaTime;

            if (Backgrounds[i].position.y < _bottomPos)
            {
                int index = i - 1;
                if (index < 0)
                {
                    index = Backgrounds.Length - 1;
                }

                Backgrounds[i].position = new Vector3(0, Backgrounds[index].position.y + _imageHeight - 1f, 0);
            }
        }
    }
}
