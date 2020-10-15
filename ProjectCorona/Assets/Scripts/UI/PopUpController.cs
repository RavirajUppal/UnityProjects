using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    private Image itemSprite;
    public Animation anim;
    public Animator animator;
    bool isAnimationStarted = false;

    float timer;

    private void Awake()
    {
        itemSprite = GetComponentInChildren<Image>();
    }

    public void ShowPopUp(Sprite itemSprite)
    {
        this.itemSprite.sprite = itemSprite;

        anim.Play();

        anim.playAutomatically = true;
        isAnimationStarted = true;
    }

    private void Update()
    {
        if (anim != null && isAnimationStarted == true)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= anim.clip.length)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
