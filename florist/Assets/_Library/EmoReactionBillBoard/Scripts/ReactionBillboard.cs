using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionBillboard: MonoBehaviour
{
    [SerializeField]SpriteBin spriteBin;
    [SerializeField]Sprite[] baloonReactions;
    [SerializeField]Image baloonBase;
    [SerializeField]Image EmojiBase;
    [SerializeField] AnimationCurve popInCurve;
    [SerializeField] bool SpeechBaloon;

    Vector3 OriginalSize;
    float BaloonReactionBegins;
    float baloonReactionDuration = 1;

    // Start is called before the first frame update
    void Start()
    {
        OriginalSize = transform.localScale;
        if (spriteBin != null)
            baloonReactions = spriteBin.Sprites.ToArray();
    }
    public void SetReactionSet(SpriteBin spriteBin)
    {
        this.spriteBin = spriteBin;
        baloonReactions = this.spriteBin.Sprites.ToArray();
    }
    public void reactRandom() {

        react(Random.Range(0, baloonReactions.Length));

    }
    public void react(int ID)
    {

        if (baloonReactions.Length > 0 && !EmojiBase.enabled)
        {
            if(SpeechBaloon)
                baloonBase.enabled = true;
            EmojiBase.enabled = true;
            EmojiBase.sprite = baloonReactions[ID];
            BaloonReactionBegins = Time.time;
        }
    }
    void Update()
    {
        if (baloonBase.sprite != null)
        {
            float progress = (Time.time - BaloonReactionBegins) / baloonReactionDuration;
            if (BaloonReactionBegins + baloonReactionDuration < Time.time)
            {
                EmojiBase.enabled = false;
                if (SpeechBaloon)
                    baloonBase.enabled = false;
            }
            transform.localScale = OriginalSize * popInCurve.Evaluate(progress);
        }
    }
}
