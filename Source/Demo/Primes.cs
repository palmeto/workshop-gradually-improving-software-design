namespace Prime;
public static class Primes
{
    public static IEnumerable<int> GetPrimes() =>
        GetPrimeCandidates().Where(IsPrime);

    private static bool IsPrime(int candidate)
    {
        return !GetDivisors(candidate).Any();
    }

    private static IEnumerable<int> GetDivisors(int candidate)
    {
        return Enumerable.Range(0, (int)Math.Sqrt(candidate) -1 )
            .Where(divisor => candidate%divisor == 0);
    }

    private static IEnumerable<int> GetPrimeCandidates()
    {
        return Enumerable.Range(2, int.MaxValue -1);
    }
}