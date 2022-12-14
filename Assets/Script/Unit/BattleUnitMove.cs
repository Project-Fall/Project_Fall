using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitMove : MonoBehaviour
{
    BattleUnit _BattleUnit;
    BattleDataManager _BattleDataMNG;

    #region Loc X, Y
    [SerializeField] int _LocX, _LocY;
    public int LocX => _LocX;
    public int LocY => _LocY;
    #endregion


    private void Awake()
    {
        _BattleUnit = GetComponent<BattleUnit>();
    }

    private void Start()
    {
        _BattleDataMNG = GameManager.Instance.BattleMNG.BattleDataMNG;
    }


    //오브젝트 생성 이전, 최초 위치 설정
    public void setLocate(int x, int y)
    {
        _LocX = x;
        _LocY = y;
    }


    // 이동 경로를 받아와 이동시킨다
    public void MoveLotate(int x, int y)
    {
        _BattleDataMNG.FieldMNG.EnterTile(_BattleUnit, LocX, LocY);

        int dumpX = _LocX;
        int dumpY = _LocY;

        // 타일 범위를 벗어난 이동이면 이동하지 않음
        if (0 <= _LocX + x && _LocX + x < 8)
            dumpX += x;
        if (0 <= _LocY + y && _LocY + y < 3)
            dumpY += y;

        // 이동할 곳이 비어있지 않다면 이동하지 않음
        if (!GameManager.Instance.BattleMNG.BattleDataMNG.FieldMNG.GetIsOnTile(dumpX, dumpY))
        {
            _LocX = dumpX;
            _LocY = dumpY;
        }


        SetLotate();
    }

    // 타일 위로 이동
    public void SetLotate()
    {
        Vector3 vec = _BattleDataMNG.FieldMNG.GetTileLocate(LocX, LocY);
        transform.position = vec;

        // 현재 타일에 내가 들어왔다고 알려줌 
        _BattleDataMNG.FieldMNG.EnterTile(_BattleUnit, LocX, LocY);
    }
}
