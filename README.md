# Travel Agency Backend

This project is a backend API for a travel agency, providing authentication features such as user registration, login, and logout. It also supports booking flights and hotels, similar to popular travel booking platforms like Booking.com. It is built using ASP.NET Core with Entity Framework Core for data access and ASP.NET Core Identity for user management.

## Getting Started

To get started with the project, follow these steps:

1. Clone the repository to your local machine.
2. Update the database connection string in the `appsettings.json` file with your actual database connection.
3. Run the database migrations to create the database schema:

    ```
    dotnet ef database update
    ```

4. Run the application:

    ```
    dotnet run
    ```

The application will start listening on the specified port (typically port 5000) and you can now access the API endpoints.

## API Endpoints

### Authentication

- `POST /api/auth/register`: Register a new user.
- `POST /api/auth/login`: Login with existing credentials.
- `POST /api/auth/logout`: Logout the current user.

### Flight Booking

- `GET /api/flights`: Get a list of available flights.
- `GET /api/flights/{id}`: Get details of a specific flight by ID.
- `POST /api/flights/book`: Book a flight.

### Hotel Booking

- `GET /api/hotels`: Get a list of available hotels.
- `GET /api/hotels/{id}`: Get details of a specific hotel by ID.
- `POST /api/hotels/book`: Book a hotel.

### Example Request

Book a flight:
```
POST /api/flights/book
Content-Type: application/json
Authorization: Bearer {access_token}

{
"flightId": 123,
"passengerName": "John Doe",
"passengerEmail": "john@example.com",
"numberOfSeats": 2
}
```


### Example Response

```
HTTP/1.1 200 OK
Content-Type: application/json

{
"message": "Flight booked successfully"
}
```


## Dependencies

- ASP.NET Core 8.0
- Entity Framework Core 6.0
- ASP.NET Core Identity 8.0
- Swashbuckle.AspNetCore 6.4.0 (for Swagger documentation)

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
