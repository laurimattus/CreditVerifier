using CreditVerifier.Models;

namespace CreditVerifier.Services
{
    public interface ICreditService
    {
        CreditResponse Verify(int amount, double existingAmount, int months);
        double CalculateEndAmount(double amount, int months);
    }
}