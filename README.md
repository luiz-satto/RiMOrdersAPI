## About this solution
Clean Architecture With .NET 6 And CQRS | Project Setup

## This API solution implements the follow actions:
# Create a new order
# Update the order delivery address
# Update the order items
# Cancel an order
# Retrieve a single order
# Retrieve a paginated list of orders

## How to run
The application needs a database. Run the following command in the `Infrastructure` directory:

````bash
dotnet run --migrate-database
````