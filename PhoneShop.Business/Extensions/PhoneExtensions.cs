using Phoneshop.Domain.Entities;

namespace Phoneshop.Business.Extensions
{
    public static class PhoneExtensions
    {
        public static double PriceWithoutVat(this Phone value)
        {
            return value.Price - value.Price / 100 * 21;
        }
    }
}
