using Task2;

namespace Task2.Tests;

public class ServerTests
{
    [Fact]
    public void AddToCount_Once_CorrectlyChangesCount()
    {
        Server.ResetCount();

        Server.AddToCount(10);

        Assert.Equal(10, Server.GetCount());
    }

    [Fact]
    public void AddToCount_Twice_CorrectlyChangesCount()
    {
        Server.ResetCount();

        Server.AddToCount(2);
        Server.AddToCount(3);

        Assert.Equal(5, Server.GetCount());
    }

    [Fact]
    public async Task AddToCount_WithParallelWriters_AddsCorrectTotal()
    {
        Server.ResetCount();

        const int writers = 100;
        const int additionsPerWriter = 100000;

        List<Task> tasks = new();

        for (int i = 0; i < writers; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                for (int j = 0; j < additionsPerWriter; j++)
                    Server.AddToCount(1);
            }));
        }

        await Task.WhenAll(tasks);

        Assert.Equal(writers * additionsPerWriter, Server.GetCount());
    }

    [Fact]
    public async Task AddToCount_WithParallelWritersAndReaders_AddsCorrectTotal()
    {
        Server.ResetCount();

        const int writers = 100;
        const int readers = 1000;
        const int operationsPerActor = 100000;

        List<Task> tasks = new();

        for (int i = 0; i < writers; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                for (int j = 0; j < operationsPerActor; j++)
                    Server.AddToCount(1);
            }));
        }

        for (int i = 0; i < readers; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                int value = Server.GetCount();
                Assert.True(value >= 0);
            }));
        }

        await Task.WhenAll(tasks);

        Assert.Equal(writers * operationsPerActor, Server.GetCount());
    }
}
