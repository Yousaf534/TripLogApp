Overview
  This project provides a simple interface for creating and managing trips. Each trip consists of:
    -A destination
    -Accommodation
    -Start and end dates
    -One or more selected activities

In addition to trip creation, users can manage the supporting data sets: destinations, accommodations, and activities.
The application uses Entity Framework Core with the InMemory provider, making it easy to run without database setup.

Features
  -Trip Management
    -View all trips in a structured table
    -Delete existing trips
    -See destination, dates, accommodation, and selected activities

Two-Step Trip Creation
  -Step 1: Select destination, accommodation, and dates
  -Step 2: Select one or more activities

Destinations
  -Add new destinations
  -Delete destinations that are not currently used by trips

Accommodations
  -Add new accommodations
  -Delete accommodations not referenced by trips

Activities
  -Add new activities
  -Delete unused activities
  
Data Layer
  -EF Core InMemory database for easy setup
  -One-to-many relationships (Trip → Destination, Trip → Accommodation)
  -Many-to-many relationship (Trip ↔ Activity) implemented via a join entity TripActivity
  -Delete restrictions to maintain referential integrity

Technology Stack
  -ASP.NET Core MVC (.NET 8)
  -Entity Framework Core (InMemory provider)
  -Bootstrap 5
  -C#
  -Razor Views
