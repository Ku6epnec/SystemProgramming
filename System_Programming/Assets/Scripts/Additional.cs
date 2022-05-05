using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

/*������� 3*. ��������������.
����������� ������ WhatTaskFasterAsync, ������� ����� ��������� � ��������
���������� CancellationToken, � ����� ��� ������ � ���� ���������� ���� Task.
������ ������ ������� ���������� ���� �� ����� �� �����, ������������� ������
� ���������� ���������. ���� ������ ������ ��������� ������, ������� true, ����
������ � false. ���� �������� CancellationToken, ����� ������� false. ���������
����������������� � ������� ����� �� ������� 2.

public static Task<bool> WhatTaskFasterAsync(CancellationToken ct, Task task1, Task
task2)
{
// �����-�� ��������
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

        Debug.Log("���������: " + result);
    }

    async Task<bool> SecondTimer(CancellationToken cancelToken)
    {
        await Task.Delay(_maxDelay);
        if (cancelToken.IsCancellationRequested)
        {
            Debug.Log("�������� � Additional �������� �������. ������� �� ������!");
            return false;
        }
        Debug.Log("� Additional ������� ������");
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
            Debug.Log("�������� � Additional �������� �������. �������� �������: " + _frames);
            return false;
        }

        Debug.Log("� Additional ������ 60 �������");
        return true;
    }
}
