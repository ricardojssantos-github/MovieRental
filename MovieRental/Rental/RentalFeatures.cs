using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.PaymentProviders;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
        private readonly IEnumerable<IPaymentProvider> _paymentProviders;

        public RentalFeatures(MovieRentalDbContext movieRentalDb, IEnumerable<IPaymentProvider> paymentProviders)
		{
			_movieRentalDb = movieRentalDb;
			_paymentProviders = paymentProviders;
		}

		//TODO: make me async :(
		public async Task<Rental> SaveAsync(Rental rental)
		{
            // Added payment processing functionality with validation .
            var paymentProvider = _paymentProviders
			   .FirstOrDefault(p =>
				   p.PaymentMethod.Equals(rental.PaymentMethod, StringComparison.OrdinalIgnoreCase));

            if (paymentProvider == null)
                throw new InvalidOperationException("Unsupported payment method.");

            var dummyPriceprice = 10;

            var paymentSucceeded = await paymentProvider.Pay(dummyPriceprice);

            if (!paymentSucceeded)
                throw new InvalidOperationException("Payment failed. Rental was not created.");

            await _movieRentalDb.Rentals.AddAsync(rental);
			await _movieRentalDb.SaveChangesAsync();
			return rental;
		}

        //TODO: finish this method and create an endpoint for it
        public async Task<IEnumerable<Rental>> GetRentalsByCustomerNameAsync(string customerName)
        {
            return await _movieRentalDb.Rentals
				.Include(r => r.Customer)
				.Where(r => r.Customer.Name.Contains(customerName))
				.ToListAsync();
        }
    }
}
