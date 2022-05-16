using System.Collections;
using UnityEngine;

/* Задание 1. Применить корутины.
 * Дано:
1. Класс Unit, у которого есть переменная health, отвечающая за текущее количество жизней.
2. Метод RecieveHealing().

Задача: реализовать корутину, которая будет вызываться из метода RecieveHealing, чтобы юнит получал 
исцеление 5 жизней каждые полсекунды в течение 3 секунд или до тех пор, пока количество жизней не станет равным 100. 
На юнит не может действовать более одного эффекта исцеления одновременно.

public class Unit : MonoBehaviour
{
int health;
public void ReceiveHealing()
{
// какие-то действия
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
        //Нажмите на кнопку H, чтобы запустить этот код(сделано для удобства проверки, т.к. все программы отрабатываются через Unity)
        if (Input.GetKeyUp(KeyCode.H) && _healAvailable)
        {
            _healAvailable = false;
            Debug.Log("Было ХП: " + _health);
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
        Debug.Log("Стало ХП: " + _health);
        _healAvailable = true;
    }
}