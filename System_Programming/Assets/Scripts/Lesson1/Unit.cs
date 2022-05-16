using System.Collections;
using UnityEngine;

/* ������� 1. ��������� ��������.
 * ����:
1. ����� Unit, � �������� ���� ���������� health, ���������� �� ������� ���������� ������.
2. ����� RecieveHealing().

������: ����������� ��������, ������� ����� ���������� �� ������ RecieveHealing, ����� ���� ������� 
��������� 5 ������ ������ ���������� � ������� 3 ������ ��� �� ��� ���, ���� ���������� ������ �� ������ ������ 100. 
�� ���� �� ����� ����������� ����� ������ ������� ��������� ������������.

public class Unit : MonoBehaviour
{
int health;
public void ReceiveHealing()
{
// �����-�� ��������
}
}*/

public class Unit: MonoBehaviour
{
    [SerializeField] private int _health = 25;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _heal = 5;

    [SerializeField] private float _maxTimeHeal = 3.0f;
    [SerializeField] private float _healInterval = 0.5f;
    [SerializeField] private float _pastTime = 0.0f;

    private float _nullTime = 0.0f;
    private bool _healAvailable = true;

    private void Update()
    {
        //������� �� ������ H, ����� ��������� ���� ���(������� ��� �������� ��������, �.�. ��� ��������� �������������� ����� Unity)
        if (Input.GetKeyUp(KeyCode.H) && _healAvailable)
        {
            _healAvailable = false;
            Debug.Log("���� ��: " + _health);
            ReceiveHealing();
        }
    }

    public void ReceiveHealing()
    {
        StartCoroutine(HealUp(_maxTimeHeal));
    }

    IEnumerator HealUp(float _maxTime)
    {
        while (_health < _maxHealth && _pastTime < _maxTime) 
        {
            yield return new WaitForSeconds(_healInterval);
            _health += _heal;
            _pastTime += _healInterval;
            Debug.Log(_health);
        }
        _pastTime = _nullTime;
        Debug.Log("����� ��: " + _health);
        _healAvailable = true;
    }
}