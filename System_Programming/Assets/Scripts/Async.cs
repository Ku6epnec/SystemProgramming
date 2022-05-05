using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

/*Задание 2. Применить async/await.
  Реализовать две задачи: Task1 и Task2. В качестве параметров задачи должны принимать CancellationToken.
  Первая задача должна ожидать одну секунду, а после выводить в консоль сообщение о своём завершении.
  Вторая задача должна ожидать 60 кадров, а после — выводить сообщение в консоль.*/

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
            Debug.Log("Операция прервана токеном. Секунда не прошла!");
            return;
        }
        await Task.Delay(_maxDelay);
        Debug.Log("Секунда прошла");
    }

    async void FramesTimer(CancellationToken cancelToken, int _frames)
    {
        while (_frames > 0)
        {
            if (cancelToken.IsCancellationRequested)
            {
                Debug.Log("Операция прервана токеном. Осталось фреймов: " + _frames);
                return;
            }

            _frames--;
            await Task.Yield();
        }
        Debug.Log("Прошло 60 фреймов");
    }
}
