using System;
using System.Threading;

class MainClass {
  public static readonly int RUN_SLEEP_INTERVAL = 1000;

  public static readonly int ADD_BATCH_NUM = 5;
  public static readonly int ADD_BATCH_SIZE = 20;

  public static readonly int ADD_PRIORITY_MIN = 1;
  public static readonly int ADD_PRIORITY_MAX = 100;

  public static readonly int ADD_SLEEP_INTERVAL = 10000;

  public static readonly int ADD_SLEEP_MIN = 250;
  public static readonly int ADD_SLEEP_MAX = 1000;

  public static void Main (string[] args) {
    MinHeap processes = new MinHeap();

    RunThread r1 = new RunThread("Consumer-1", processes, RUN_SLEEP_INTERVAL);
    RunThread r2 = new RunThread("Consumer-2", processes, RUN_SLEEP_INTERVAL);

    Thread rt1 = new Thread(new ThreadStart(r1.run));
    Thread rt2 = new Thread(new ThreadStart(r2.run));

    rt1.Start();
    rt2.Start();

    AddThread a1 = new AddThread(processes, ADD_BATCH_NUM, ADD_BATCH_SIZE, ADD_PRIORITY_MIN, ADD_PRIORITY_MAX, ADD_SLEEP_INTERVAL, ADD_SLEEP_MIN, ADD_SLEEP_MAX);

    Thread at1 = new Thread(new ThreadStart(a1.run));

    at1.Start();

    at1.Join();
    r1.noNewWork();
    r2.noNewWork();

    rt1.Join();
    rt2.Join();
  }
}