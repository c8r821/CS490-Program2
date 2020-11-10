using System;
using System.Threading;
using System.Globalization;

/**
 * The AddThread class acts as the producer for threaded tasks
 */
public class AddThread {
    /**
     * A reference to the synchronized task heap object
     */
    private readonly MinHeap heap;

    /**
     * The number of batch adds that will be performed
     */
    private readonly int batchNum;

    /**
     * The number of tasks to add with each batch
     */
    private readonly int batchSize;

    /**
     * The minimum value the random priority can be set to
     */
    private readonly int priorityMin;

    /**
     * The maximum value the random priority can be set to
     */
    private readonly int priorityMax;

    /**
     * The duration the producer should wait between batches
     */
    private readonly int sleepInterval;

    /**
     * The minimum "runtime" for produced tasks
     */
    private readonly int sleepMin;

    /**
     * The maximum "runtime" for produced tasks
     */
    private readonly int sleepMax;

    int pid = 0;

    /**
     * Parameterized constructor that allows for setting all configurations
     */
    public AddThread(MinHeap heap, int batchNum, int batchSize, int priorityMin, int priorityMax, int sleepInterval, int sleepMin, int sleepMax) {
        this.heap = heap;
        this.batchNum = batchNum;
        this.batchSize = batchSize;
        this.priorityMin = priorityMin;
        this.priorityMax = priorityMax;
        this.sleepInterval = sleepInterval;
        this.sleepMin = sleepMin;
        this.sleepMax = sleepMax;
    }

    /**
     * The thread entry-point, will perform batch adds as specified by the parameters passed in
     */
    public void run() {
        Random rand = new Random();
        for (int batch = 0; batch < batchNum; batch++) {
            for (int i = 0; i < batchSize; i++) {
                Node process = new Node(
                    pid++,
                    rand.Next(priorityMin, priorityMax),
                    rand.Next(sleepMin, sleepMax)
                );

                heap.push(process);

                Console.WriteLine(" + Producer added process {0} at time {1}", process.toString(), DateTime.Now.ToString("T", CultureInfo.CreateSpecificCulture("en-us")));
            }

            Thread.Sleep(sleepInterval);
        }
        Console.WriteLine(" ++ Producer has completed its tasks.");
    }
}