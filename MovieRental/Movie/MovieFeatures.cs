using MovieRental.Data;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

		// TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
		/*
			About this and skipping the async:
		
			Getting all records this way may represent some issues depending of the amount of data
			ToList() will bring all the records from the table and if the table grows, that may cause severe issues of performance or even memory being full. 
			One way to avoid this is using pagination, where we can still count the total records but only take a specific amout of data. 

			Another thing is exposing the entity directly. We could use a DTO with mapping to expose only the necessary data we want. 
		 */
		public List<Movie> GetAll()
		{
			return _movieRentalDb.Movies.ToList();
		}


	}
}
