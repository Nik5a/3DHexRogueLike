using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isEnemy;
    public bool isChosen;
    
    public Cell cell;
    public GridManager manager;
    public Vector3 gridOffset;

    public int HP;
    public int attack;
    public int movementRange;
    public int attackRange;

    private void Start()
    {
        gridOffset = new Vector3(0f, .65f, 0f);
        cell = transform.parent.GetComponent<Cell>();
        manager = cell.transform.parent.GetComponent<GridManager>();
    }

    // TODO - сделать плавный переход между позициями
    private void Update()
    {
        Move();
    }
    
    public void MoveTo(Cell toCell)
    {
        manager.currentState = GridManager.State.inAction;
        
        cell.character = null;
        toCell.character = this;
        
        cell.UnHighlight(true);
        cell.AddNeighboursInRange(cell.movementRangeCells, 1);
        cell.attackRangeCells = new List<Cell>();

        cell = toCell;
        cell.AddNeighboursInRange(cell.movementRangeCells, movementRange);
        cell.AddNeighboursInRange(cell.attackRangeCells, attackRange);
        
        transform.position = toCell.transform.position + gridOffset;
        transform.parent = toCell.gameObject.transform;
        isChosen = false;

        manager.currentState = GridManager.State.nothingChosen;
    }

    void Move()
    {
        //transform.position = Vector3.LerpUnclamped();
    }

    private void OnMouseDown()
    {
        if (!isEnemy)
        {
            manager.ChooseCharacter(this);
        }
    }
    
    private void OnMouseEnter()
    {
        // TODO: изменять курсор на курсор атаки, если на ячейке враг
        if (manager.currentState == GridManager.State.characterChosen)
            return;

        cell.OnMouseEnter();
    }

    private void OnMouseExit()
    {
        if (manager.currentState == GridManager.State.characterChosen)
            return;
        
        cell.OnMouseExit();
    }

    // TODO: Запустить атаку при нажатии ПКМ
    private void OnMouseOver()
    {
        //if (manager.currentState == GridManager.State.characterChosen)
            //return;
        
        cell.OnMouseOver();
    }
}
