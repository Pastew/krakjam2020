using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public List<string> _happyBossSprites;
    public List<string> _angryBossSprites;

    // Start is called before the first frame update
    void Start()
    {
        _happyBossSprites = new List<string> {
            "spritename1",
            "spritename2",
            "spritename3",
            "spritename3",
        };

        _angryBossSprites = new List<string> {
            "spritename1",
            "spritename2",
            "spritename3",
            "spritename3",
        };
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetHappy()
    {
        string sprite = GetRandomItem(_happyBossSprites);
        SetSprite(sprite);
        Dance();
    }

    private string GetRandomItem(List<string> spritesToChoose)
    {
        var random = new System.Random();
        int index = random.Next(spritesToChoose.Count);
        return spritesToChoose[index];
    }

    private void Dance()
    {
        throw new NotImplementedException();
    }

    private void SetSprite(string sprite)
    {
        throw new NotImplementedException();
    }

    public void SetAngry()
    {
        string sprite = GetRandomItem(_angryBossSprites);
        SetSprite(sprite);
        Dance();
    }
}
