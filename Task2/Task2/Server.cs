using System;
using System.Threading;

namespace Task2;

/// <summary>
/// An imitation of a remote server.
/// </summary>
public static class Server
{
    private static int s_count;
    private readonly static ReaderWriterLockSlim s_countLock = new();

    /// <summary>
    /// A getter for count. If something is writing into count, the method won't return until the currently ongoing write operation completes.
    /// </summary>
    /// <returns>The current value of count.</returns>
    public static int GetCount()
    {
        s_countLock.EnterReadLock();

        try
        {
            return s_count;
        }
        finally
        {
            s_countLock.ExitReadLock();
        }
    }

    /// <summary>
    /// A setter for count. If something is already writing into count, the method won't proceed until the currently ongoing write operation completes.
    /// </summary>
    /// <param name="value">An amount to add to the current value of count.</param>
    public static void AddToCount(int value)
    {
        if (value <= 0)
            return;

        s_countLock.EnterWriteLock();

        try
        {
            s_count += value;
        }
        finally
        {
            s_countLock.ExitWriteLock();
        }
    }

    /// <summary>
    /// Resets the count. If something is already writing into count, the method won't proceed until the currently ongoing write operation completes.
    /// </summary>
    public static void ResetCount()
    {
        s_countLock.EnterWriteLock();

        try
        {
            s_count = 0;
        }
        finally
        {
            s_countLock.ExitWriteLock();
        }
    }
}
