using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    public int column;
    public int row;
    public int TargetX;
    public int TargetY;
    private Vector2 firstPosition;
    private Vector2 finalPosition;
    private float DragAngle;
    private float swipeLimit = 0.5f;
    private board bd;
    private GameObject otherdot;
    private Vector2 tempPosition;

    

    // Start is called before the first frame update
    void Start()
    {
        TargetX = (int)transform.position.x;
        TargetY = (int)transform.position.y;
        //Debug.Log("tagetx = " + TargetX);
        column = TargetX;
        row = TargetY;
        bd = FindObjectOfType<board>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetX = column;
        TargetY = row;
        moveDots();
        if (findMatches)
        {

        }
    }

    private void OnMouseDown()
    {
        firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        finalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
        
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalPosition.y - firstPosition.y) > swipeLimit || Mathf.Abs(finalPosition.x - firstPosition.x) > swipeLimit)
        {
            DragAngle = Mathf.Atan2(finalPosition.y - firstPosition.y, finalPosition.x - firstPosition.x) * Mathf.Rad2Deg;
            //Debug.Log(DragAngle);
            SwipeDots();
        }
    }

    void SwipeDots()
    {
        //right swipe
        if(DragAngle > -45  && DragAngle <= 45 && column < bd.Width -1)
        {
            otherdot = bd.AllDots[column + 1, row];
            otherdot.GetComponent<DotScript>().column -= 1;
            column += 1;
        }

        //left swipe
        if (DragAngle > 135 || DragAngle <= -135 && column > 0)
        {
            otherdot = bd.AllDots[column -1 , row];
            otherdot.GetComponent<DotScript>().column += 1;
            column -= 1;
        }

        //up swipe
        if (DragAngle > 45  && DragAngle <= 135 && row < bd.Height -1)
        {
            otherdot = bd.AllDots[column, row + 1];
            otherdot.GetComponent<DotScript>().row -= 1;
            row += 1;
        }

        //down swipe
        if (DragAngle > -135  && DragAngle <= -45 && row > 0)
        {
            otherdot = bd.AllDots[column , row - 1];
            otherdot.GetComponent<DotScript>().row += 1;
            row -= 1;
        }

    }

    void moveDots()
    {
        if( Mathf.Abs(TargetX - transform.position.x) > 0.1)
        {
            tempPosition = new Vector2(TargetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.1f);
            
        }
        else
        {
            tempPosition = new Vector2(TargetX, transform.position.y);
            transform.position = tempPosition;
            //Debug.Log("gameobject = "+ this.gameObject);
            bd.AllDots[column, row] = this.gameObject;

        }

        if (Mathf.Abs(TargetY - transform.position.y) > 0.1)
        {
            tempPosition = new Vector2(transform.position.x, TargetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.1f);
           
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, TargetY);
            transform.position = tempPosition;
            bd.AllDots[column, row] = this.gameObject;
        }
    }

    bool findMatches()
    {
        GameObject leftDot1 = bd.AllDots[column - 1, row];
        GameObject rightDot1 = bd.AllDots[column + 1, row];
        if(leftDot1.tag == gameObject.tag && rightDot1.tag == gameObject.tag)
        {
            return true;
        }

        GameObject UpDot1 = bd.AllDots[column , row + 1];
        GameObject DownDot1 = bd.AllDots[column , row - 1];
        if (UpDot1.tag == gameObject.tag && DownDot1.tag == gameObject.tag)
        {
            return true;
        }

        else
        return false;
    }
    

}
