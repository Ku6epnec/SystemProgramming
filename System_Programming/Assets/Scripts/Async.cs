using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

/*������� 2. ��������� async/await.
  ����������� ��� ������: Task1 � Task2. � �������� ���������� ������ ������ ��������� CancellationToken.
  ������ ������ ������ ������� ���� �������, � ����� �������� � ������� ��������� � ���� ����������.
  ������ ������ ������ ������� 60 ������, � ����� � �������� ��������� � �������.*/

public class Async : MonoBehaviour
{
    [SerializeField] private int _maxFrames = 60;
    [SerializeField] private int _maxDelay = 1000;
    void Start()
    {
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken cancelToken = cancelTokenSource.Token;
        SecondTimer(cancelToken);
        FramesTimer(cancelToken, _maxFrames);

        cancelTokenSource.Cancel();
        cancelTokenSource.Dispose();
    }

    async void SecondTimer(CancellationToken cancelToken)
    {
        if (cancelToken.IsCancellationRequested)
        {
            Debug.Log("�������� �������� �������. ������� �� ������!");
            return;
        }
        await Task.Delay(_maxDelay);
        Debug.Log("������� ������");
    }

    async void FramesTimer(CancellationToken cancelToken, int _frames)
    {
        while (_frames > 0)
        {
            if (cancelToken.IsCancellationRequested)
            {
                Debug.Log("�������� �������� �������. �������� �������: " + _frames);
                return;
            }

            _frames--;
            await Task.Yield();
        }
        Debug.Log("������ 60 �������");
    }
}
