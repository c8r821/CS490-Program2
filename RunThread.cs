using System;
using System.Threading;
using System.Globalization;

/**
 * The RunThread class acts as the consumer for threaded tasks
 */
public class RunThread {
  /**
    * A reference to the synchronized task heap object
    */
  private readonly MinHeap heap;

  /**
    * The consumer's name, used for identification
    */
  private readonly String name;

  /**
    * The duration the consumer should wait between checks for work
    */
  private readonly int sleepInterval;

  /**
    * A shared flag that indicates the producers completion status
    */
  private volatile bool hasNewWork = true;

  /**
    * Parameterized constructor that allows for setting all configurations
    */
  public RunThread(String name, MinHeap heap, int sleepInterval) {
    this.name = name;
    this.heap = heap;
    this.sleepInterval = sleepInterval;
  }

  /**
    * Method that allows the main thread to indicate the producer has finished its work
    */
  public void noNewWork() {
    hasNewWork = false;
    Console.WriteLine(" -- {0} has been notified of producer completion", name);
  }

  /**
    * The thread entry-point, will execute tasks added to the heap by order of priority, waiting if no new work is present
    */
  public void run() {
      while (hasNewWork || !heap.isEmpty()) {
        while (heap.isEmpty()) {
          if (!hasNewWork) return;
          Thread.Sleep(sleepInterval);
        }

        Node process = heap.pop();
        Thread.Sleep(process.getTimeslice());

        Console.WriteLine(" - {0} completed process {1} at time {2}", name, process.toString(), DateTime.Now.ToString("T", CultureInfo.CreateSpecificCulture("en-us")));
    }
    Console.WriteLine(" -- {0} has completed its tasks", name);
  }
}