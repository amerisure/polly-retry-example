using System;
using Polly;

namespace PollyRetryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a policy
            var policy = Policy.Handle<Exception>()
                .WaitAndRetry(
                    retryCount: 10,
                    sleepDurationProvider: attemptNumber => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber)), // 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024 seconds
                    onRetry: (exception, _) => Console.WriteLine($"Encountered the following exception on retry.{Environment.NewLine}{Environment.NewLine}{exception}") // Log or take some other action
                );

            // Step 2: Run the code that may throw an exception. Polly will handle the exception and retry according to the rule above
            policy.Execute(() => throw new Exception("An error has occurred!"));
        }
    }
}
