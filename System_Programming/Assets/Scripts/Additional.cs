using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

/*Задание 3*. Дополнительное.
Реализовать задачу WhatTaskFasterAsync, которая будет принимать в качестве
параметров CancellationToken, а также две задачи в виде переменных типа Task.
Задача должна ожидать выполнения хотя бы одной из задач, останавливать другую
и возвращать результат. Если первая задача выполнена первой, вернуть true, если
вторая — false. Если сработал CancellationToken, также вернуть false. Проверить
работоспособность с помощью задач из Задания 2.

public static Task<bool> WhatTaskFasterAsync(CancellationToken ct, Task task1, Task
task2)
{
// какие-то действия
}
*/

public class Additional : MonoBehaviour
{
    [SerializeField] private int _maxFrames = 60;
    [SerializeField] private int _maxDelay = 1000;
    async void Start()
    {
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken cancelToken = cancelTokenSource.Token;

        Task<bool> Task1 = SecondTimer(cancelToken);
        Task<bool> Task2 = FramesTimer(cancelToken, _maxFrames);
        Task<bool> finishedTask = await Task.WhenAny(Task1, Task2);
        bool result = (finishedTask == Task1 && finishedTask.Result == true);

        cancelTokenSource.Cancel();
        cancelTokenSource.Dispose();

        Debug.Log("Результат: " + result);
    }

    async Task<bool> SecondTimer(CancellationToken cancelToken)
    {
        await Task.Delay(_maxDelay);
        if (cancelToken.IsCancellationRequested)
        {
            Debug.Log("Операция в Additional прервана токеном. Секунда не прошла!");
            return false;
        }
        Debug.Log("В Additional секунда прошла");
        return true;
    }

    async Task<bool> FramesTimer(CancellationToken cancelToken, int _frames)
    {
        while (_frames > 0)
        {
            _frames--;
            await Task.Yield();
        }
        if (cancelToken.IsCancellationRequested)
        {
            Debug.Log("Операция в Additional прервана токеном. Осталось фреймов: " + _frames);
            return false;
        }

        Debug.Log("В Additional прошло 60 фреймов");
        return true;
    }
}
