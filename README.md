# MovieRental Exercise

This is a dummy representation of a movie rental system.
Can you help us fix some issues and implement missing features?

 * The app is throwing an error when we start, please help us. Also, tell us what caused the issue.
   => The error that occurs when the application starts happens because the IRentalFeatures service was registered as a Singleton.
		However, it depends on MovieRentalDbContext, which has a scoped lifetime, and this is not allowed in ASP.NET Core.
		To fix this, I changed IRentalFeatures to Scoped, ensuring it shares the same lifecycle as the DbContext and avoiding potential concurrency issues.

 * The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
   => First I changed the return type. Now the method returns Task<Rental>, then changed the name into SaveAsync for good practices. 
		Then I added the await and changed the methods into async methods from entity framework core.
		The synchronous version blocks the thread while the database operation is being executed.
		The asynchronous version frees the thread while waiting for the database to respond, allowing the server to handle other requests. This improves scalability and performance, especially under high load.

 * Please finish the method to filter rentals by customer name, and add the new endpoint.
   => Added in code.
   
 * We noticed we do not have a table for customers, it is not good to have just the customer name in the rental.
   Can you help us add a new entity for this? Don't forget to change the customer name field to a foreign key, and fix your previous method!
   => Added in code

 * In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
   => About this and skipping the async:
		Getting all records this way may represent some issues depending of the amount of data.
		ToList() will bring all the records from the table and if the table grows, that may cause severe issues of performance or even memory being full. 
		One way to avoid this is using pagination, where we can still count the total records but only take a specific amout of data. 
		Another thing is exposing the entity directly. We could use a DTO with mapping to expose only the necessary data we want. 

 * No exceptions are being caught in this api, how would you deal with these exceptions?
   => Currently, no exceptions are being handled in the API, which means that any unexpected error results in unhandled 500 response.
		I would use global exception handling middleware to catch and log unhandled exceptions and return consistent error responses. 
		For expected errors, I would handle them explicitly and return the appropriate HTTP status codes.



	## Challenge (Nice to have)
We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
    * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
* RentalFeatures Class:
    * Within the RentalFeatures class, you are required to implement the payment processing functionality.
* Payment Provider Designation:
    * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
* Extensibility:
    * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
* Payment Failure Handling:
    * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.

